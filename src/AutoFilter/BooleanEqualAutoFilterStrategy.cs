using System;

namespace AutoFilter
{
    /// <summary>
    /// IAutoFilterStrategy's implementation that compare boolean values.
    /// </summary>
    public class BooleanEqualAutoFilterStrategy : SimpleValueAutoFilterStrategyBase<Boolean>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanEqualAutoFilterStrategy"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public BooleanEqualAutoFilterStrategy(string propertyName = null)
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
        protected override bool TryParse(string filter, out Boolean simpleValue)
        {
            return Boolean.TryParse(filter, out simpleValue);
        }
        #endregion
    }
}
