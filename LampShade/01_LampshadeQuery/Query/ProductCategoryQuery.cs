using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
        }

        public List<ProductCategoryQueryViewModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryViewModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Id = x.Id


            }).ToList();
        }

        public List<ProductCategoryQueryViewModel> GetProductCategoriesWithProducts()
        {
            return _context.ProductCategories.Include(x => x.Products)
                .ThenInclude(x=>x.Category).Select(x => new ProductCategoryQueryViewModel
            {
                Id = x.Id,
                Name = x.Name,

                Products = MapProducts(x.Products)
            }).ToList();
        }

        private List<ProductQueryModel> MapProducts(List<Product> products)
        {
            var result = new List<ProductQueryModel>();
            foreach (var product in products)
            {
                var item = new ProductQueryModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Category = product.Category.Name,
                    Slug = product.Slug
                };
                result.Add(item);
            }

            return result;
        }
    }
}
