using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using System;

namespace ShopManagement.Configuration
{
    public static class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection serviceCollection,string constring)
        {
            serviceCollection.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            serviceCollection.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            serviceCollection.AddDbContext<ShopContext>(options => options.UseSqlServer(constring));

        }
    }
}
