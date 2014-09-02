using System;
using System.Linq.Expressions;
using System.Reflection;
using AutoFilter.Helpers;

namespace AutoFilter.Expressions
{
    /// <summary>
    /// DateTime expressions extensions.
    /// </summary>
    public static class DateTimeExpressionExtensions
    {
        #region Fields
        /// <summary>
        /// The ToUpper method.
        /// </summary>
        private readonly static MethodInfo s_trimSeconds = typeof(DateTimeHelper).GetMethod("TrimSeconds", new Type[] { typeof(DateTime) });
        #endregion

        #region Methods
        /// <summary>
        /// DateTimeExtensions.TrimSeconds expression.
        /// </summary>
        /// <param name="dateTimeExpression">The DateTime expression.</param>
        /// <returns>The expressions.</returns>
        public static MethodCallExpression TrimSeconds(this Expression dateTimeExpression)
        {
            return Expression.Call(s_trimSeconds, dateTimeExpression);
        }

        #endregion
    }
}
