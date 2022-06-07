using BlogManagement.Application;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using BolgManagement.Application.Contracts.ArticleCategory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BolgManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Query;
using _01_LampshadeQuery.Contracts.ArticleCategory;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionstring)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionstring));
        }
    }
}
