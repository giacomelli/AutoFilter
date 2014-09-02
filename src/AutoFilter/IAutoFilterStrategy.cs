using System.Linq.Expressions;
using System.Reflection;

namespace AutoFilter
{
    /// <summary>
    /// Defines an interface for an auto filter strategy.
    /// </summary>
    public interface IAutoFilterStrategy
    {
        #region Properties
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        string Filter { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this instance can filter the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>True if can filter the property.</returns>
        bool CanFilter(PropertyInfo propertyInfo);

        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <returns>The expression.</returns>
        Expression CreateExpression(Expression left);
        #endregion
    }
}
