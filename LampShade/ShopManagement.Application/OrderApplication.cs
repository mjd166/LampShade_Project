using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;
using System;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInventoryACL _shopInventoryACL;
        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration, IShopInventoryACL shopInventoryACL)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
            _shopInventoryACL = shopInventoryACL;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public string OrderSucceeded(long orderId,long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            var symbol = _configuration.GetSection("Symbol").Value;
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNumber(issueTrackingNo);
            //Reduce Order Item from Inventory .... 
            if (!_shopInventoryACL.ReduceFromInventory(order.Items))
            {
                return "";
            }
            else
            {
                _orderRepository.Savechanges();
                return order.IssueTrackingNumber;
            }

        }

        public long PlaceOrder(Cart command)
        {
            var currentAccount = _authHelper.CurrentAccountId();
            var order = new Order(currentAccount,command.PaymentMethod, command.TotalAmount, command.DiscountAmount, command.PayAmount);
            foreach (var item in command.Items)
            {
                var orderitem = new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate);
                order.Add(orderitem);
            }
            _orderRepository.Create(order);
            _orderRepository.Savechanges();
            return order.Id;
        }

        public List<OrderViewModel> Search(OrderSearchModel orderSearchModel)
        {
            return _orderRepository.Search(orderSearchModel);
        }
    }
}
