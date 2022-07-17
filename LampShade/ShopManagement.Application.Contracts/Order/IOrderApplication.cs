using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart command);
        string OrderSucceeded(long orderId,long refId);
        void Cancel(long id);
        double GetAmountBy(long id);
        List<OrderItemViewModel> GetItems(long id);
        List<OrderViewModel> Search(OrderSearchModel orderSearchModel);

    }
}
