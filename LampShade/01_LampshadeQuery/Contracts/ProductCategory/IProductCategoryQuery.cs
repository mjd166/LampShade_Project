using System.Collections.Generic;
namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryViewModel> GetProductCategories();
        List<ProductCategoryQueryViewModel> GetProductCategoriesWithProducts();
    }
}
