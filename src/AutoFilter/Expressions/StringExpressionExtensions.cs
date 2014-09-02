using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoFilter.Expressions
{
    /// <summary>
    /// String expression extensions.
    /// </summary>
    public static class StringExpressionExtensions
    {
        #region Fields
        /// <summary>
        /// The ToUpper method.
        /// </summary>
        private readonly static MethodInfo s_toUpperMethod = typeof(string).GetMethod("ToUpper", new Type[0]);

        /// <summary>
        /// The Contains method.
        /// </summary>
        private readonly static MethodInfo s_containsMethod = typeof(string).GetMethod("Contains");

        /// <summary>
        /// The IsNullOrEmpty static method.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "sis")]
        private readonly static MethodInfo s_isNullOrEmptyMethod = typeof(string).GetMethod("IsNullOrEmpty");
        #endregion

        #region Methods
        /// <summary>
        /// String.ToUpper expression.
        /// </summary>
        /// <param name="stringExpression">The string expression.</param>
        /// <returns>The Expression.</returns>
        public static MethodCallExpression ToUpper(this Expression stringExpression)
        {
            return Expression.Call(stringExpression, s_toUpperMethod);
        }

        /// <summary>
        /// String.Contains epxression.
        /// </summary>
        /// <param name="stringExpression">The string expression.</param>
        /// <param name="substringExpression">The substring expression.</param>
        /// <returns>The Expression.</returns>
        public static MethodCallExpression Contains(this Expression stringExpression, Expression substringExpression)
        {
            return Expression.Call(stringExpression, s_containsMethod, substringExpression);
        }

        /// <summary>
        /// String.IsNullOrEmpty expression.
        /// </summary>
        /// <param name="stringExpression">The string expression.</param>
        /// <returns>The Expression.</returns>
        public static MethodCallExpression IsNullOrEmpty(this Expression stringExpression)
        {
            return Expression.Call(s_isNullOrEmptyMethod, stringExpression);
        }
        #endregion
    }
}
