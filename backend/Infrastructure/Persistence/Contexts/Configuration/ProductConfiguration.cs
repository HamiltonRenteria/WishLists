using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");

            builder.ToTable("Product");

            builder.Property(e => e.Category).HasMaxLength(200);
            builder.Property(e => e.Description).HasMaxLength(250);
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.CategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");
        }
    }
}
