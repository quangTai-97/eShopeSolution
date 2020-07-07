using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            //Co hai khoa chinh
            builder.HasKey(t =>
            new
            {
                t.CategoryId,
                t.ProductId
            });
            builder.ToTable("ProductIncategories");

            //Khoa ngoai doi voi 2 bang
            builder.HasOne(t => t.Product).WithMany(p => p.ProductInCategories)
                .HasForeignKey(p=>p.ProductId);

            builder.HasOne(t => t.Category).WithMany(p => p.ProductInCategories)
               .HasForeignKey(p => p.CategoryId);
        }
    }
}
