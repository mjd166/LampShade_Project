using _0_Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public int Count { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }

        public List<InventoryOperation>  Operations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }
       
        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }
        public void Increase(long count,long operatorId,string description)
        {
            var currentcount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, count, operatorId, currentcount, description, 0, Id);
            Operations.Add(operation);
            InStock = currentcount > 0;
        }
        public void Reduce(long count,long operatorId,string description,long orderId)
        {
            var currentcount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, count, operatorId, currentcount, description, orderId, Id);
            Operations.Add(operation);
            InStock = currentcount > 0;
        }

        public void Edit(long productId, double unitPrice)
        {
            this.ProductId = productId;
            this.UnitPrice = unitPrice;
        }
    }
}
