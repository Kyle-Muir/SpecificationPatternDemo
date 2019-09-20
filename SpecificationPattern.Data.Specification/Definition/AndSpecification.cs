using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPattern.Data.Specification.Definition
{
    internal class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            andExpression = (BinaryExpression)new ParameterReplacer(leftExpression.Parameters.Single()).Visit(andExpression);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }
}