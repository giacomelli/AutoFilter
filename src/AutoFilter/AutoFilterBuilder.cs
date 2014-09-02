using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using HelperSharp;
using LinqKit;

namespace AutoFilter
{
    /// <summary>
    /// Builder to create auto filters.
    /// </summary>
    /// <typeparam name="TTarget">The target class type.</typeparam>
    public class AutoFilterBuilder<TTarget>
    {
        #region Fields
        private bool m_hasAnyPropertyFilter;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFilterBuilder{TTarget}"/> class.
        /// </summary>
        /// <param name="strategies">The strategies.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">strategies;At least one IAutoFilterTypeStrategy should be specified.</exception>
        public AutoFilterBuilder(params IAutoFilterStrategy[] strategies)
        {
            ExceptionHelper.ThrowIfNull("strategies", strategies);

            if (strategies.Length == 0)
            {
                throw new ArgumentOutOfRangeException("strategies", "At least one IAutoFilterTypeStrategy should be specified.");
            }

            Strategies = new List<IAutoFilterStrategy>(strategies);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the strategies thar will be used to build the filter.
        /// </summary> 
        public IList<IAutoFilterStrategy> Strategies { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Builds the filter's expression tree.
        /// </summary>
        /// <param name="filter">The filter string.</param>
        /// <param name="preFilter">The pre filter.</param>
        /// <returns>The filter expression.</returns>
        public virtual Expression<Func<TTarget, bool>> Build(string filter, Expression<Func<TTarget, bool>> preFilter = null)
        {
            var targetType = typeof(TTarget);
            var properties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var targetParameter = Expression.Parameter(targetType);
            Expression predicateBody = Expression.Constant(false);

            InitializeStrategies(filter);

            m_hasAnyPropertyFilter = false;

            foreach (var p in properties)
            {
                predicateBody = AppendPropertyFilter(predicateBody, p, targetParameter);
            }

            var propertiesFiltersExpression = Expression.Lambda<Func<TTarget, bool>>(predicateBody, targetParameter);
            return InsertPreFilter(propertiesFiltersExpression, preFilter);
        }

        private Expression<Func<TTarget, bool>> InsertPreFilter(Expression<Func<TTarget, bool>> propertiesFiltersExpression, Expression<Func<TTarget, bool>> preFilter)
        {
            if (preFilter != null)
            {
                return m_hasAnyPropertyFilter ? preFilter.And(propertiesFiltersExpression) : preFilter;
            }

            return propertiesFiltersExpression;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        private Expression AppendPropertyFilter(Expression predicateBody, PropertyInfo property, ParameterExpression targetParameter)
        {
            var propertyExp = Expression.Property(targetParameter, property.Name);

            foreach (var strategy in Strategies)
            {
                if (strategy.CanFilter(property))
                {
                    predicateBody = Expression.OrElse(predicateBody, strategy.CreateExpression(propertyExp));
                    m_hasAnyPropertyFilter = true;

                    break;
                }
            }

            return predicateBody;
        }

        private void InitializeStrategies(string filter)
        {
            // Informa uma única vez para todas as estratégias qual será o filtro.
            foreach (var strategy in Strategies)
            {
                strategy.Filter = filter;
            }
        }
        #endregion
    }
}
