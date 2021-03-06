using System.Collections.Generic;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductCategories
{
    //[Authorize(Roles ="1,2")]
    public class IndexModel : PageModel
    {

        private readonly IProductCategoryApplication _productCategoryApplication;

        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> producctCategories;
        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        [NeedPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            producctCategories = _productCategoryApplication.Search(searchModel);
        }
        [NeedPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CreateProductCategory());
        }
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        
        public IActionResult OnGetEdit(long id)
        {
            var productcategory = _productCategoryApplication.GetDetails(id);
            return Partial("./Edit", productcategory);
        }

        [NeedPermission(ShopPermissions.EditProductCategory)]
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);

        }
    }
}
