using System;
using System.Globalization;
using System.Linq.Expressions;
using AutoFilter.Expressions;
using AutoFilter.Helpers;

namespace AutoFilter
{
    /// <summary>
    /// IAutoFilterStrategy's implementation that compare DateTime values.
    /// </summary>
    public class DateTimeEqualAutoFilterStrategy : SimpleValueAutoFilterStrategyBase<DateTime>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeEqualAutoFilterStrategy"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public DateTimeEqualAutoFilterStrategy(string propertyName = null)
            : base(propertyName)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Try to perform the filter parse to the simple value supported by the strategy.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="simpleValue">The simple value.</param>
        /// <returns>True if parse was succeeded.</returns>
        protected override bool TryParse(string filter, out DateTime simpleValue)
        {
            var result = DateTime.TryParse(filter, CultureInfo.CurrentUICulture, DateTimeStyles.None, out simpleValue);

            if (result)
            {
                simpleValue = DateTime.SpecifyKind(simpleValue.TrimSeconds(), DateTimeKind.Local);
            }

            return result;
        }

        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <returns>
        /// The expression.
        /// </returns>
        public override Expression CreateExpression(Expression left)
        {
            return base.CreateExpression(left.TrimSeconds());
        }
        #endregion
    }
}
