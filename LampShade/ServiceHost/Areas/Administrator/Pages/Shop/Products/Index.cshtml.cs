using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly IProductCategoryApplication _productcategoryApplication;
        public SelectList ProductCategories;

        public ProductSearchModel SearchModel { get; set; }
        public List<ProductViewModel> Products { get; set; }
        private readonly IProductApplication _ProductApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productcategoryApplication)
        {
            _ProductApplication = productApplication;
            _productcategoryApplication = productcategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {

            ProductCategories = new SelectList(_productcategoryApplication.GetProductCategories(), "Id", "Name");
            Products = _ProductApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _productcategoryApplication.GetProductCategories()
            };


            return Partial("Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            return new JsonResult(_ProductApplication.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _ProductApplication.GetDetails(id);
            product.Categories = _productcategoryApplication.GetProductCategories();
            return Partial("Edit", product);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            return new JsonResult(_ProductApplication.Edit(command));
        }


    
    }
}
