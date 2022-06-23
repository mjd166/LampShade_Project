using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Products",new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProducts,"ListProducts"),
                        new PermissionDto(ShopPermissions.SearchProducts,"SearchProducts"),
                        new PermissionDto(ShopPermissions.CreateProduct,"CreateProduct"),
                        new PermissionDto(ShopPermissions.EditProduct,"EditProduct"),
                        

                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductCategories,"ListProductCategories"),
                        new PermissionDto(ShopPermissions.SearchProductCategories,"SearchProductCategories"),
                        new PermissionDto(ShopPermissions.CreateProductCategory,"CreateProductCategories"),
                        new PermissionDto(ShopPermissions.EditProductCategory,"EditProductCategories"),

                    }
                }
            };
        }
    }
}
