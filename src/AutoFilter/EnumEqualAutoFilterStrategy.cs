using System;

namespace AutoFilter
{
    /// <summary>
    /// IAutoFilterStrategy's implementation that compare enumerations values.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    public class EnumEqualAutoFilterStrategy<TEnum> : SimpleValueAutoFilterStrategyBase<TEnum> where TEnum : struct
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumEqualAutoFilterStrategy{TEnum}"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public EnumEqualAutoFilterStrategy(string propertyName = null)
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
        protected override bool TryParse(string filter, out TEnum simpleValue)
        {
            return Enum.TryParse<TEnum>(filter, out simpleValue);
        }
        #endregion
    }
}
