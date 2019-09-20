using System;
using System.Linq;
using System.Linq.Expressions;
using SpecificationPattern.Data.Specification.Definition;

namespace SpecificationPattern.Data.Specification
{
    public class ProductHasEnoughStockSpecification : Specification<Product>
    {
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return source => source.Variants.All(v => v.StockLevel >= 5);
        }
    }
}