using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoFilter
{
    /// <summary>
    /// Base class to IAutoFilterStrategy's implementation based on simple values, like int, double, bool, etc.
    /// </summary>
    /// <typeparam name="TSimpleValue">The type of simple value.</typeparam>
    public abstract class SimpleValueAutoFilterStrategyBase<TSimpleValue> : IAutoFilterStrategy
    {
        #region Fields
        private string m_propertyName;
        private string m_filter;
        private bool m_isFilterSupported;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleValueAutoFilterStrategyBase{TSimpleValue}"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected SimpleValueAutoFilterStrategyBase(string propertyName)
        {
            m_propertyName = propertyName;

            var defaultValue = default(TSimpleValue);
            Filter = defaultValue == null ? defaultValue as string : defaultValue.ToString();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter
        {
            get
            {
                return m_filter;
            }

            set
            {
                m_filter = value;

                TSimpleValue simpleValue;

                if (TryParse(m_filter, out simpleValue))
                {
                    SimpleValueExpression = Expression.Constant(simpleValue);
                    m_isFilterSupported = true;
                }
                else
                {
                    m_isFilterSupported = false;
                }
            }
        }

        /// <summary>
        /// Gets the simple value expression.
        /// </summary>        
        protected Expression SimpleValueExpression { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this instance can filter the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>
        /// True if can filter the property.
        /// </returns>
        public virtual bool CanFilter(PropertyInfo propertyInfo)
        {
            return m_isFilterSupported
                && propertyInfo.PropertyType == typeof(TSimpleValue)
                &&
                (String.IsNullOrEmpty(m_propertyName)
                || propertyInfo.Name.Equals(m_propertyName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="left">The left expression.</param>
        /// <returns>
        /// The expression.
        /// </returns>
        public virtual Expression CreateExpression(Expression left)
        {
            return Expression.Equal(left, SimpleValueExpression);
        }

        /// <summary>
        /// Try to perform the filter parse to the simple value supported by the strategy.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="simpleValue">The simple value.</param>
        /// <returns>True if parse was succeeded.</returns>
        protected abstract bool TryParse(string filter, out TSimpleValue simpleValue);
        #endregion
    }
}
