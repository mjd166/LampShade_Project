using _0_Framework.Domain;
using System;
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

        public Inventory(long productId, int count, double unitPrice)
        {
            ProductId = productId;
            Count = count;
            UnitPrice = unitPrice;
            InStock = false;
        }
       
        private long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }
        public void Encrease(long count,long operatorId,string description)
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
    }

    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long OperatorId { get; private set; }
        public DateTime CreationDate{ get; private set; }
        public long CurrentDiscount { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }

        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, long count, long operatorId, long currentDiscount, string description, long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperatorId = operatorId;
            CurrentDiscount = currentDiscount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
        }
    }
}
