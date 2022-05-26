using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Slide>  Slides { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemb = typeof(ProductCategory).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemb);

            base.OnModelCreating(modelBuilder);
        }
    }
}
