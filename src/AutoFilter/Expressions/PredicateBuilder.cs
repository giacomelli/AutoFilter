using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "LinqKit")]

namespace LinqKit
{
    /// <summary>
    /// See http://www.albahari.com/expressions for information and examples.
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// True expression.
        /// </summary>
        /// <typeparam name="T">The parameter type.</typeparam>
        /// <returns>The expression.</returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// False expression.
        /// </summary>
        /// <typeparam name="T">The parameter type.</typeparam>
        /// <returns>The expression.</returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// Or expression.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <param name="right">The right expression.</param>
        /// <returns>The expression.</returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left,
                                                  Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                 (Expression.OrElse(left.Body, invokedExpr), left.Parameters);
        }

        /// <summary>
        /// And expression.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <param name="right">The right expression.</param>
        /// <returns>The expression.</returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
                                                   Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                 (Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }
    }

}
