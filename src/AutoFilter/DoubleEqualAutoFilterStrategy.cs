using System;
using System.Globalization;

namespace AutoFilter
{
    /// <summary>
    /// Estratégia de filtro automático para comparação com valores double.
    /// </summary>
    public class DoubleEqualAutoFilterStrategy : SimpleValueAutoFilterStrategyBase<Double>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleEqualAutoFilterStrategy"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public DoubleEqualAutoFilterStrategy(string propertyName = null)
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
        protected override bool TryParse(string filter, out Double simpleValue)
        {
            return double.TryParse(filter, NumberStyles.Number, CultureInfo.CurrentUICulture, out simpleValue);
        }
        #endregion
    }
}
