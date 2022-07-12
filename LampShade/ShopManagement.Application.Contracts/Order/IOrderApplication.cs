using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart command);
        string OrderSucceeded(long orderId,long refId);

        double GetAmountBy(long id);
    }
}
