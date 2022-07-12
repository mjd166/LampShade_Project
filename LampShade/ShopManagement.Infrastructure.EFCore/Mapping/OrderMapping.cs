using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IssueTrackingNumber).HasMaxLength(8);

            builder.OwnsMany(x => x.Items, navigationbuiler =>
            {
                navigationbuiler.ToTable("OrderItems");
                navigationbuiler.HasKey(x => x.Id);
                navigationbuiler.WithOwner(x => x.Order).HasForeignKey(x => x.OrderId);
            });
        }
    }
}
