using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.Id, x.PayAmount }).FirstOrDefault(x => x.Id == id);
            return order != null ? order.PayAmount : 0;


        }

        public List<OrderViewModel> Search(OrderSearchModel orderSearchModel)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();

            var query = _context.Orders.Select(x => new OrderViewModel
            {
                AccountId = x.AccountId,
                Id = x.Id,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNumber = x.IssueTrackingNumber,
                PayAmount = x.PayAmount,
                PaymentMethodId = x.PaymentMethod,
                RefId = x.RefId,
                DiscountAmount = x.DiscountAmount,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi()

            });

            query = query.Where(x => x.IsCanceled == orderSearchModel.IsCanceled);

            if (orderSearchModel.AccountId > 0)
                query = query.Where(x => x.AccountId == orderSearchModel.AccountId);



            var order = query.OrderByDescending(x => x.Id).ToList();

            foreach (var item in order)
            {
                item.AccountFullName = accounts.FirstOrDefault(x => x.Id == item.AccountId)?.Fullname;
                item.PaymentMethod = PaymentMethod.GetBy(item.PaymentMethodId)?.Name;
            }

            return order;

        }
    }
}
