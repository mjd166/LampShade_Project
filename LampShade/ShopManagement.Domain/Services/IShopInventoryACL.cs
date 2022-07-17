using ShopManagement.Domain.OrderAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.Services
{
    public interface IShopInventoryACL
    {
        bool ReduceFromInventory(List<OrderItem> orderItems);
    }
}
