using System;
using NUnit.Framework;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class StringContainsIgnoreCaseAutoFilterStrategyTest
    {
        [Test()]
        public void CanFilter_DiffPropertyName_False()
        {
            var target = new StringContainsIgnoreCaseAutoFilterStrategy("Name");
            Assert.IsFalse(target.CanFilter(typeof(TimeZoneInfo).GetProperty("DisplayName")));
        }

        [Test()]
        public void CanFilter_SamePropertyName_True()
        {
            var target = new StringContainsIgnoreCaseAutoFilterStrategy("DisplayName");
            Assert.IsTrue(target.CanFilter(typeof(TimeZoneInfo).GetProperty("DisplayName")));
        }

        [Test()]
        public void CreateExpression_LeftExpression_Expression()
        {
            var target = new StringContainsIgnoreCaseAutoFilterStrategy("DaylightName");
            var builder = new AutoFilterBuilder<TimeZone>(target);

            Assert.IsFalse(builder.Build("none").Compile()(TimeZone.CurrentTimeZone));
            Assert.IsTrue(builder.Build(TimeZone.CurrentTimeZone.DaylightName.ToUpperInvariant()).Compile()(TimeZone.CurrentTimeZone));
        }
    }
}