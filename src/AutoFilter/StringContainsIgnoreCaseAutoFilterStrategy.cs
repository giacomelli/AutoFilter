using System.Linq.Expressions;
using AutoFilter.Expressions;

namespace AutoFilter
{
    /// <summary>
    /// IAutoFilterStrategy's implementation that filter using string contains ignore-case.
    /// </summary>
    public class StringContainsIgnoreCaseAutoFilterStrategy : StringContainsAutoFilterStrategy
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringContainsIgnoreCaseAutoFilterStrategy"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public StringContainsIgnoreCaseAutoFilterStrategy(string propertyName = null)
            : base(propertyName)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <returns>
        /// The expression.
        /// </returns>
        public override Expression CreateExpression(Expression left)
        {
            return Expression.AndAlso(
                        Expression.Not(left.IsNullOrEmpty()),
                        left.ToUpper().Contains(SimpleValueExpression.ToUpper()));
        }
        #endregion
    }
}
