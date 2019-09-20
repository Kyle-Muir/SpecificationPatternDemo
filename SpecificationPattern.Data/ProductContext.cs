using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace SpecificationPattern.Data
{
    public class ProductContext : DbContext
    {
        public static readonly LoggerFactory LoggerFactory =
            new LoggerFactory(new[] {
                new DebugLoggerProvider()
            });

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SpecPattern;User Id=dev;Password=dev;");
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(b =>
            {
                b.HasData(new Product
                    {
                        ProductId = 1,
                        Name = "Green shirt",
                        Description = "A cool shirt",
                        IsActive = true,
                        Price = 29.99m,
                    },
                    new Product
                    {
                        ProductId = 2,
                        Name = "Casio Watch",
                        Description = "Keep track of time in style",
                        IsActive = true,
                        Price = 42,
                    });
                b.OwnsMany(p => p.Variants).HasData(new ProductVariant
                    {
                        ProductId = 1,
                        ProductVariantId = 1,
                        Name = "Extra Small",
                        IsActive = false,
                        StockLevel = 5
                    },
                    new ProductVariant
                    {
                        ProductId = 1,
                        ProductVariantId = 2,
                        Name = "Small",
                        IsActive = true,
                        StockLevel = 6
                    },
                    new ProductVariant
                    {
                        ProductId = 1,
                        ProductVariantId = 3,
                        Name = "Medium",
                        IsActive = true,
                        StockLevel = 2
                    },
                    new ProductVariant
                    {
                        ProductId = 1,
                        ProductVariantId = 4,
                        Name = "Large",
                        IsActive = true,
                        StockLevel = 9
                    },
                    new ProductVariant
                    {
                        ProductId = 1,
                        ProductVariantId = 5,
                        Name = "Extra Large",
                        IsActive = false,
                        StockLevel = 0
                    }, new ProductVariant
                    {
                        ProductId = 2,
                        ProductVariantId = 6,
                        Name = "Green",
                        IsActive = false,
                        StockLevel = 5
                    },
                    new ProductVariant
                    {
                        ProductId = 2,
                        ProductVariantId = 7,
                        Name = "Gold",
                        IsActive = true,
                        StockLevel = 5
                    },
                    new ProductVariant
                    {
                        ProductId = 2,
                        ProductVariantId = 8,
                        Name = "Black",
                        IsActive = true,
                        StockLevel = 12
                    });
            });
        }
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ProductVariant> Variants { get; set; }
    }

    public class ProductVariant
    {
        [Key]
        public int ProductVariantId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int StockLevel { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}