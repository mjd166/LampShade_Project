using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {

        private readonly IInventoryRepository
        
        public OperationResult Create(CreateInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditInventory command)
        {
            throw new NotImplementedException();
        }

        public EditInventory GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            throw new NotImplementedException();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
