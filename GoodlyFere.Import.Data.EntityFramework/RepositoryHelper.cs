#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Common.Logging;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework
{
    public static class RepositoryHelper
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(RepositoryHelper));

        #endregion

        #region Methods

        internal static Expression ConvertToConcreteExpression(
            Type concreteType, Type interfaceType, Expression interfaceExpression)
        {
            if (!interfaceType.IsAssignableFrom(concreteType))
            {
                throw new Exception("Interface type must be assignable from concrete type to convert an expression.");
            }

            return TransformVisitor.Transform(concreteType, interfaceType, interfaceExpression);
        }

        internal static Expression<Func<TConcrete, bool>> ConvertToConcreteExpression<TConcrete, TInterface>(
            Expression<Func<TInterface, bool>> interfaceExpression)
        {
            if (!typeof(TInterface).IsAssignableFrom(typeof(TConcrete)))
            {
                throw new Exception("TInterface must be assignable from TConcrete to convert an expression.");
            }

            return (Expression<Func<TConcrete, bool>>)
                   TransformVisitor.Transform(
                       typeof(TConcrete),
                       typeof(TInterface),
                       interfaceExpression);
        }

        #endregion
    }

    internal class TransformVisitor : ExpressionVisitor
    {
        #region Constants and Fields

        private readonly Type _concreteType;
        private readonly Type _interfaceType;
        private readonly ParameterExpression _param;

        #endregion

        #region Constructors and Destructors

        private TransformVisitor(Type concreteType, Type interfaceType)
        {
            _concreteType = concreteType;
            _interfaceType = interfaceType;
            _param = Expression.Parameter(_concreteType, "param_0");
        }

        #endregion

        #region Public Methods

        public static Expression Transform(Type concreteType, Type interfaceType, Expression expression)
        {
            var visitor = new TransformVisitor(concreteType, interfaceType);
            var newLambda = visitor.Visit(expression);
            return newLambda;
        }

        #endregion

        #region Methods

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (typeof(T).IsAssignableFrom(Expression.GetFuncType(_interfaceType, typeof(bool))))
            {
                return Expression.Lambda(
                    Expression.GetFuncType(_concreteType, typeof(bool)),
                    Visit(node.Body),
                    _param
                    );
            }

            return base.VisitLambda(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType.IsAssignableFrom(_interfaceType))
            {
                return Expression.MakeMemberAccess(
                    Visit(node.Expression),
                    _concreteType.GetProperty(node.Member.Name));
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type.IsAssignableFrom(_interfaceType))
            {
                return _param;
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.Type.IsAssignableFrom(_interfaceType))
            {
                return Expression.MakeUnary(
                    node.NodeType, node.Operand, _concreteType);
            }

            return base.VisitUnary(node);
        }

        #endregion
    }
}