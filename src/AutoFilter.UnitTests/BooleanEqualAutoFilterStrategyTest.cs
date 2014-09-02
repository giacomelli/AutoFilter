using System;
using System.Linq.Expressions;
using NUnit.Framework;
using TestSharp;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class BooleanEqualAutoFilterStrategyTest
    {       
        [Test()]
        public void CanFilter_DiffPropertyName_False()
        {            
            var target = new BooleanEqualAutoFilterStrategy("SupportsDaylight");
            Assert.IsFalse(target.CanFilter(typeof(TimeZoneInfo).GetProperty("SupportsDaylightSavingTime")));
        }

        [Test()]
        public void CanFilter_SamePropertyName_True()
        {
            var target = new BooleanEqualAutoFilterStrategy("SupportsDaylightSavingTime");
            Assert.IsTrue(target.CanFilter(typeof(TimeZoneInfo).GetProperty("SupportsDaylightSavingTime")));
        }

        [Test()]
        public void CreateExpression_LeftExpression_Expression()
        {
            var target = new BooleanEqualAutoFilterStrategy("SupportsDaylightSavingTime");
            var builder = new AutoFilterBuilder<TimeZoneInfo>(target);
            
            Assert.IsFalse(builder.Build("none").Compile()(TimeZoneInfo.Local));
            Assert.IsTrue(builder.Build(TimeZoneInfo.Local.SupportsDaylightSavingTime.ToString()).Compile()(TimeZoneInfo.Local));
        }
    }
}