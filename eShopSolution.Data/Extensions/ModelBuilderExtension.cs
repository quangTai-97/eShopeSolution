using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitile", Value = "This is home page of Solution" },
                new AppConfig() { Key = "HomeKeyWord", Value = "This is HomeKeyWord page of Solution" },
                new AppConfig() { Key = "Homedepcription", Value = "This is Homedepcription page of Solution" }
                );
            modelBuilder.Entity<Language>().HasData(
               new Language() { Id = "vi-VN", Name = "Tieng Viet", IsDefault = true },
               new Language() { Id = "en-US", Name = "English", IsDefault = false });

            modelBuilder.Entity<Category>().HasData(
            new Category()
            {
                Id = 1,
                IsShowOnHome = true,
                ParentId = null,
                SortOrder = 1,
                Status = Status.Active
            },
            new Category()
            {
                Id = 2,
                IsShowOnHome = true,
                ParentId = null,
                SortOrder = 2,
                Status = Status.Active
            });
            modelBuilder.Entity<CategoryTranslation>().HasData(

                new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Ao Nam", LanguageId = "vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sans Pham ao thoi trang nam", SeoTitle = "Sans Pham ao thoi trang nam" },
                  new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "men-shirt", SeoDescription = "This shirt products for men", SeoTitle = "This shirt products for men" },
                new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Ao Nu", LanguageId = "vi-VN", SeoAlias = "ao-nu", SeoDescription = "Sans Pham ao thoi trang nu", SeoTitle = "Sans Pham ao thoi trang nu" },
                new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Women Shirt", LanguageId = "en-US", SeoAlias = "women-shirt", SeoDescription = "This shirt products for women", SeoTitle = "This shirt products for men" }
                );

            modelBuilder.Entity<Product>().HasData(
            new Product()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                OriginalPrice = 100000,
                Price = 200000,
                Stock = 0,
                ViewCount = 0

            });

            modelBuilder.Entity<ProductTranslation>().HasData(


                    new ProductTranslation()
                    {
                        Id = 1,
                        ProductId = 1,
                        Name = "Ao Nam viet tien",
                        LanguageId = "vi-VN",
                        SeoAlias = "ao-nam-viet-tien",
                        SeoDescription = "Ao Nam viet tien",
                        SeoTitle = "Ao Nam viet tien",
                        Details = "Ao Nam viet tien",
                        Description = "Ao Nam viet tien"
                    },
                    new ProductTranslation()
                    {
                        Id = 2,
                        ProductId = 1,
                        Name = "Viet Tien Men T-Shirt",
                        LanguageId = "en-US",
                        SeoAlias = "Viet-tien-men-t-shirt",
                        SeoDescription = "Viet Tien Men T-Shirt",
                        SeoTitle = "Viet Tien Men T-Shirt",
                        Details = "Viet Tien Men T-Shirt",
                        Description = "Viet Tien Men T-Shirt"

                    });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }
                );


            //any guid
            var roleId = new Guid("3969955C-023E-418F-8568-0546F5296145");
            var adminId = new Guid("E2C7E83D-F2AE-45F6-964B-C42E3C826A1B");

            modelBuilder.Entity<AppRole>().HasData(new AppRole 
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "doquangtaia3@gmail.com",
                NormalizedEmail = "doquangtaia3@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null,"ABC123456@"),
                SecurityStamp = string.Empty,
                FirstName = "Tai",
                LastName = "Do",
                Dob = new DateTime(2020,12,30)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

        }
    }
}
