using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.API.DTOs;
using SpecificationPattern.Data;
using SpecificationPattern.Data.Specification;

namespace SpecificationPattern.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            using (var context = new ProductContext())
            {
                return context.Products.Select(ProductDetails.FromDb).ToList();
            }
        }

        [HttpGet]
        [Route("get-active-sad")]
        public ActionResult<IEnumerable<ProductDetails>> GetActiveSad()
        {
            using (var context = new ProductContext())
            {
                return context.Products
                    .Where(p => p.IsActive && p.Variants.All(v => v.StockLevel >= 5))
                    .Select(p => new ProductDetails
                    {
                        IsActive = p.IsActive,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description
                    }).ToList();
            }
        }

        [HttpGet]
        [Route("get-active")]
        public ActionResult<IEnumerable<ProductDetails>> GetActive()
        {
            using (var context = new ProductContext())
            {
                // In EF for .Net Framework logging can be turned on by:
                // context.Database.Log = s => Console.WriteLine(s);
                var specification = new ProductIsAvailableSpecification().And(new ProductHasEnoughStockSpecification());
                return context.Products
                    .Where(specification)
                    .Select(ProductDetails.FromDb)
                    .ToList();
            }
        }
    }
}
