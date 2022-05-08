using _0_Framework.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);

        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);

    }
}
