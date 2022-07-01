using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
