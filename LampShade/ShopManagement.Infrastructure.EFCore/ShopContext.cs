using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {

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
