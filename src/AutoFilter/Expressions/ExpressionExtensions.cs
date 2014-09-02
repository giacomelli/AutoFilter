using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace AutoFilter.Expressions
{
    /// <summary>
    /// Extensões para Expressions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Traduz o tipo do primeiro parâmetro de uma expression para outro tipo.
        /// </summary>
        /// <typeparam name="TFrom">O parâmetro origem.</typeparam>
        /// <typeparam name="TTo">Ô parâmetro destino.</typeparam>
        /// <typeparam name="TResult">O tipo da expression destino.</typeparam>
        /// <param name="expression">A expression original.</param>
        /// <returns>A expression resultante</returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Expression<Func<TTo, TResult>> Translate<TFrom, TTo, TResult>(this Expression<Func<TFrom, TResult>> expression)
        {
            var param = Expression.Parameter(typeof(TTo), expression.Parameters[0].Name);
            var subst = new Dictionary<Expression, Expression> { { expression.Parameters[0], param } };
            var visitor = new TypeVisitor(typeof(TFrom), typeof(TTo), subst);

            return Expression.Lambda<Func<TTo, TResult>>(visitor.Visit(expression.Body), param);
        }
    }
}
