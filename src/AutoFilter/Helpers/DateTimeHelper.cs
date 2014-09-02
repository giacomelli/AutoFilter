using System;

namespace AutoFilter.Helpers
{
    /// <summary>
    /// DateTime helpers.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Creates a new DateTime without seconds.
        /// </summary>
        /// <param name="value">The DateTime.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime TrimSeconds(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
        }
    }
}
