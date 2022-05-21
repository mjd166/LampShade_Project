using System.Collections.Generic;
namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryViewModel GetProductCategoryWithProductBy(string slug);
        List<ProductCategoryQueryViewModel> GetProductCategories();
        List<ProductCategoryQueryViewModel> GetProductCategoriesWithProducts();
    }
}
