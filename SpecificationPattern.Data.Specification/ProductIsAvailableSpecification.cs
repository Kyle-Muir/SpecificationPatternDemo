using System;
using System.Linq.Expressions;
using SpecificationPattern.Data.Specification.Definition;

namespace SpecificationPattern.Data.Specification
{
    public class ProductIsAvailableSpecification : Specification<Product>
    {
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return source => source.IsActive;
        }
    }
}