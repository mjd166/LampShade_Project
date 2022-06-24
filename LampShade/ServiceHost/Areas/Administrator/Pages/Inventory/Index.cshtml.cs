using System.Collections.Generic;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Inventory
{
    [Authorize(Roles = Roles.Administrator)]
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; } = "";

        public SelectList Products;

        public InventorySearchModel SearchModel { get; set; }
        public List<InventoryViewModel> Inventory { get; set; }
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        [NeedPermission(InventoryPermissions.ListInventory)]
        public void OnGet(InventorySearchModel searchModel)
        {
            Message = "";
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {

                Products = _productApplication.GetProducts()

            };


            return Partial("Create", command);
        }

        [NeedPermission(InventoryPermissions.CreateInventory)]
        public JsonResult OnPostCreate(CreateInventory command)
        {
            return new JsonResult(_inventoryApplication.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("Edit", inventory);

        }

        [NeedPermission(InventoryPermissions.EditInventory)]
        public JsonResult OnPostEdit(EditInventory command)
        {
            return new JsonResult(_inventoryApplication.Edit(command));
        }





        public IActionResult OnGetIncrease(long id)
        {
            var inventory = new IncreaseInventory()
            {
                InventoryId = id

            };
            return Partial("Increase", inventory);

        }

        [NeedPermission(InventoryPermissions.IncreaseInventory)]
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            return new JsonResult(_inventoryApplication.Increase(command));
        }



        public IActionResult OnGetReduce(long id)
        {
            var inventory = new ReduceInventory()
            {
                InventoryId = id

            };
            return Partial("Reduce", inventory);

        }
        [NeedPermission(InventoryPermissions.ReduceInventory)]
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            return new JsonResult(_inventoryApplication.Reduce(command));
        }

        [NeedPermission(InventoryPermissions.OperationLog)]
        public IActionResult OnGetLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }

    }
}
