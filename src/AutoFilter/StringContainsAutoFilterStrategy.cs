using System;
using System.Linq.Expressions;
using AutoFilter.Expressions;

namespace AutoFilter
{
    /// <summary>
    /// IAutoFilterStrategy's implementation that filter using string contains.
    /// </summary>
    public class StringContainsAutoFilterStrategy : SimpleValueAutoFilterStrategyBase<String>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringContainsAutoFilterStrategy"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public StringContainsAutoFilterStrategy(string propertyName = null)
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
                            left.Contains(SimpleValueExpression));
        }

        /// <summary>
        /// Try to perform the filter parse to the simple value supported by the strategy.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="simpleValue">The simple value.</param>
        /// <returns>True if parse was succeeded.</returns>
        protected override bool TryParse(string filter, out string simpleValue)
        {
            simpleValue = filter;

            return true;
        }
        #endregion
    }
}
