using _0_Framework.Infrastructure;
using ShopManagement.Domain.OrderAgg;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;

        public OrderRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.Id, x.PayAmount }).FirstOrDefault(x => x.Id == id);
            return order != null ? order.PayAmount : 0;


        }
    }
}
