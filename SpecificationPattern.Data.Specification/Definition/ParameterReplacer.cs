using System.Linq.Expressions;

namespace SpecificationPattern.Data.Specification.Definition
{
    /// <summary>
    /// This is required since EF gets really grumpy when you combine the specifications together
    /// This fixes the parameters so you don't get the dreaded 
    /// "The parameter was not bound in the specified LINQ to Entities query expression"
    /// Interestingly this is _not_ required by Fluent NHibernate
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type == _parameter.Type)
            {
                return base.VisitParameter(_parameter);
            }

            return base.VisitParameter(node);
        }

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}