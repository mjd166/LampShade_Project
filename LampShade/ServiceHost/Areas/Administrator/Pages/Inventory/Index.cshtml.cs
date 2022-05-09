using System.Collections.Generic;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Inventory

{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; } = "";

        public SelectList Products;

        public InventorySearchModel SearchModel { get; set; }
        public List<InventoryViewModel> Inventory { get; set; }
        private readonly IInventoryApplication _inventoryApplication;
        private readonly  IProductApplication _productApplication;

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Message = "";
            Products =new SelectList( _productApplication.GetProducts(),"Id","Name");
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

        public JsonResult OnPostReduce(ReduceInventory command)
        {
            return new JsonResult(_inventoryApplication.Reduce(command));
        }

    }
}
