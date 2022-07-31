using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Inventory;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        public InventoryQuery(InventoryContext context, ShopContext shopContext)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            
            var inventory = _context.Inventory.First(x => x.ProductId == command.ProductId);
            if(inventory ==null || inventory.CalculateCurrentCount()<command.Count)
            {
                var product = _shopContext.Products.Select(x=>new {x.Id,x.Name }).FirstOrDefault(x => x.Id == command.ProductId);
                return new StockStatus
                {
                    IsStock = false,
                    ProductName = product?.Name
                };
            }

            return new StockStatus
            {
                IsStock = true
            };
        }
    }
}
