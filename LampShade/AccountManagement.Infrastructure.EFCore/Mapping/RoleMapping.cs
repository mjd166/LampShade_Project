using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
           builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Accounts).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
            builder.OwnsMany<Permission>(x => x.Permissions, navigationbuiler =>
            {
                navigationbuiler.HasKey(x => x.Id);
                navigationbuiler.ToTable("RolePermissions");
                navigationbuiler.Ignore(x => x.Name);
                navigationbuiler.WithOwner(x => x.Role);
            });
        }
    }
}
