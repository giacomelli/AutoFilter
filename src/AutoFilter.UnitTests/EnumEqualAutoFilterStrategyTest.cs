using System;
using AutoFilter.UnitTests.Stubs;
using NUnit.Framework;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class EnumEqualAutoFilterStrategyTest
    {
        [Test()]
        public void CanFilter_DiffPropertyName_False()
        {
            var target = new EnumEqualAutoFilterStrategy<DayOfWeek>("Day");
            Assert.IsFalse(target.CanFilter(typeof(DateTime).GetProperty("DayOfWeek")));
        }

        [Test()]
        public void CanFilter_SamePropertyName_True()
        {
            var target = new EnumEqualAutoFilterStrategy<DayOfWeek>("DayOfWeek");
            Assert.IsTrue(target.CanFilter(typeof(DateTime).GetProperty("DayOfWeek")));
        }

        [Test()]
        public void CreateExpression_LeftExpression_Expression()
        {
            var target = new EnumEqualAutoFilterStrategy<DayOfWeek>("DayOfWeek");
            var builder = new AutoFilterBuilder<DateTime>(target);

            var earget = DateTime.Now;
            Assert.IsFalse(builder.Build("none").Compile()(earget));
            Assert.IsTrue(builder.Build(earget.DayOfWeek.ToString()).Compile()(earget));
        }
    }
}