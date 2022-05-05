using System.Collections.Generic;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Discounts.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public SelectList Products;

        public CustomerDiscountSearchModel SearchModel { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        private readonly ICustomerDiscountApplication _CustomerDiscountApplication;
        private readonly  IProductApplication _productApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _CustomerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products =new SelectList( _productApplication.GetProducts(),"Id","Name");
            CustomerDiscounts = _CustomerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };


            return Partial("Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            return new JsonResult(_CustomerDiscountApplication.Define(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerdiscount = _CustomerDiscountApplication.GetDetails(id);
            customerdiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", customerdiscount);
           
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            return new JsonResult(_CustomerDiscountApplication.Edit(command));
        }


    }
}
