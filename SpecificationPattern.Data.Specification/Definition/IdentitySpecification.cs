using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Data.Specification.Definition
{
    public class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return value => true;
        }
    }
}