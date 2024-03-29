﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPattern.Data.Specification.Definition
{
    internal class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            orExpression = (BinaryExpression)new ParameterReplacer(leftExpression.Parameters.Single()).Visit(orExpression);
            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }
}