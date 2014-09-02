using System;

namespace AutoFilter
{
    /// <summary>
    /// IAutoFilterStrategy's implementation that compare Int32 values.
    /// </summary>
    public class Int32EqualAutoFilterStrategy : SimpleValueAutoFilterStrategyBase<Int32>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Int32EqualAutoFilterStrategy"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public Int32EqualAutoFilterStrategy(string propertyName = null)
            : base(propertyName)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tenta realizar o parse do filtro para o valor simples suportado pela estratégia.
        /// </summary>
        /// <param name="filter">O filtro.</param>
        /// <param name="simpleValue">O valor simples.</param>
        /// <returns>True se conseguiu fazer o parse.</returns>
        protected override bool TryParse(string filter, out int simpleValue)
        {
            return int.TryParse(filter, out simpleValue);
        }
        #endregion
    }
}
