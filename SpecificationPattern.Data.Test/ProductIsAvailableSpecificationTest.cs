using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SpecificationPattern.Data.Specification.Test
{
    [TestFixture]
    public class ProductIsAvailableSpecificationTest
    {
        [Test]
        public void ShouldReturnCorrectResults()
        {
            var isAvailable = new ProductIsAvailableSpecification();

            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    IsActive = true
                },
                new Product
                {
                    ProductId = 2,
                    IsActive = false
                }
            };

            var availableProducts = products.AsQueryable().Where(isAvailable).ToList();
            Assert.AreEqual(availableProducts.Count, 1);
            Assert.AreEqual(availableProducts.Single().ProductId, 1);
        }
    }
}