using System;
using System.Linq.Expressions;
using NUnit.Framework;
using TestSharp;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class StringContainsAutoFilterStrategyTest
    {       
        [Test()]
        public void CanFilter_DiffPropertyName_False()
        {
            var target = new StringContainsAutoFilterStrategy("Name");
            Assert.IsFalse(target.CanFilter(typeof(TimeZoneInfo).GetProperty("DisplayName")));
        }

        [Test()]
        public void CanFilter_SamePropertyName_True()
        {
            var target = new StringContainsAutoFilterStrategy("DisplayName");            
            Assert.IsTrue(target.CanFilter(typeof(TimeZoneInfo).GetProperty("DisplayName")));
        }

        [Test()]
        public void CreateExpression_LeftExpression_Expression()
        {
            var target = new StringContainsAutoFilterStrategy("DaylightName");
            var builder = new AutoFilterBuilder<TimeZone>(target);
            
            Assert.IsFalse(builder.Build("none").Compile()(TimeZone.CurrentTimeZone));
            Assert.IsTrue(builder.Build(TimeZone.CurrentTimeZone.DaylightName).Compile()(TimeZone.CurrentTimeZone));
        }
    }
}