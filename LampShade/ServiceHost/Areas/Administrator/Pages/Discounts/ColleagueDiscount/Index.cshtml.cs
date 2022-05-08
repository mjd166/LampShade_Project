using System.Collections.Generic;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Discounts.ColleagueDiscount

{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; } = "";

        public SelectList Products;

        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }
        private readonly IColleagueDiscountApplication _ColleagueDiscountApplication;
        private readonly  IProductApplication _productApplication;

        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _ColleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Message = "";
            Products =new SelectList( _productApplication.GetProducts(),"Id","Name");
            ColleagueDiscounts = _ColleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts()
            };


            return Partial("Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            return new JsonResult(_ColleagueDiscountApplication.Define(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var colleaguediscount = _ColleagueDiscountApplication.GetDetails(id);
            colleaguediscount.Products = _productApplication.GetProducts();
            return Partial("Edit", colleaguediscount);
           
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            return new JsonResult(_ColleagueDiscountApplication.Edit(command));
        }


        public IActionResult OnGetRemove(long id)
        {
            var result = _ColleagueDiscountApplication.Remove(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                //ViewBag.
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _ColleagueDiscountApplication.Restore(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }


    }
}
