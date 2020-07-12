using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class AppUserConfiguaration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser"); //Tạo tên bảng trong database

            builder.Property(a => a.FirstName).HasMaxLength(200).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(200).IsRequired();
            builder.Property(a => a.Dob).IsRequired();

        }
    }
}
