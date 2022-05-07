using DiscountManagement.Domain.ColleageuDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace DiscountManagement.Infrastructure.EFCore
{
    public class DiscountContext:DbContext
    {

        public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
        {

        }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder); 

        }

    }
}
