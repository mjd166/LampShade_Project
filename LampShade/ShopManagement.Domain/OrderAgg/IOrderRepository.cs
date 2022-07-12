using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<long, Order>
    {
        double GetAmountBy(long id);
    }
}
