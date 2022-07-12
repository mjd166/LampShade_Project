using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using System;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;


        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
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
            _orderRepository.Savechanges();
            return order.IssueTrackingNumber;
        }

        public long PlaceOrder(Cart command)
        {
            var currentAccount = _authHelper.CurrentAccountId();
            var order = new Order(currentAccount, command.TotalAmount, command.DiscountAmount, command.PayAmount);
            foreach (var item in command.Items)
            {
                var orderitem = new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate);
                order.Add(orderitem);
            }
            _orderRepository.Create(order);
            _orderRepository.Savechanges();
            return order.Id;
        }
    }
}
