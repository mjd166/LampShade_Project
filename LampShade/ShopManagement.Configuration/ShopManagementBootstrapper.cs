using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Slide;
using _01_LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Configuration
{
    public static class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string constring)
        {
           

            

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<Application.Contracts.Slide.ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();
            services.AddTransient<CartCalculator, CartCalculator>();


            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddTransient<ICartCalculatorService, CartCalculator>();


            services.AddSingleton<ICartService, CartService>();


            services.AddDbContext<ShopContext>(options => options.UseSqlServer(constring));



        }
    }
}
