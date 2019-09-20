using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using SpecificationPattern.Data;

namespace SpecificationPattern.API.DTOs
{
    public class ProductDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public static Expression<Func<Product, ProductDetails>> FromDb = source => new ProductDetails
        {
            Name = source.Name,
            IsActive = source.IsActive,
            Price = source.Price,
            Description = source.Description
        };
    }
}