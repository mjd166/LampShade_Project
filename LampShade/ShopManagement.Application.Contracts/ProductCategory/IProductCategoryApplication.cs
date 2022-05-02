
using _0_Framework.Application;
using System.Collections.Generic;
namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory Command);
        OperationResult Edit(EditProductCategory command);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

       EditProductCategory GetDetails(long id);

        List<ProductCategoryViewModel> GetProductCategories();


    }
}
