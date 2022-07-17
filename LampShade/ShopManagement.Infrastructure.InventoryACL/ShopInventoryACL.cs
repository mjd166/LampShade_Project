using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.InventoryACL
{
    public class ShopInventoryACL : IShopInventoryACL
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryACL(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> orderItems)
        {
            var command = (from OrderItem item in orderItems
                           let reduceitem = new ReduceInventory(item.ProductId, item.Count, "خرید مشتری", item.OrderId)
                           select reduceitem).ToList();
            return _inventoryApplication.Reduce(command).IsSuccedded;
        }
    }
}
