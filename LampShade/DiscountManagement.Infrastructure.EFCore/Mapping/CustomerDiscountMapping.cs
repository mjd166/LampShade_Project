using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DiscountManagement.Infrastructure.EFCore.Mapping
{
    class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.ToTable("CustomerDiscounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired
                ();
            builder.Property(x => x.Reason).HasMaxLength(255).IsRequired();

            builder.Property(x => x.DiscountRate).IsRequired();
           
        }
    }
}
