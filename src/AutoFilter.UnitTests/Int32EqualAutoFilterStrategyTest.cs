using System;
using System.Linq.Expressions;
using NUnit.Framework;
using TestSharp;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class Int32EqualAutoFilterStrategyTest
    {       
        [Test()]
        public void CanFilter_DiffPropertyName_False()
        {
            var target = new Int32EqualAutoFilterStrategy("TheDay");
            Assert.IsFalse(target.CanFilter(typeof(DateTime).GetProperty("Day")));
        }

        [Test()]
        public void CanFilter_SamePropertyName_True()
        {
            var target = new Int32EqualAutoFilterStrategy("Day");
            Assert.IsTrue(target.CanFilter(typeof(DateTime).GetProperty("Day")));
        }

        [Test()]
        public void CreateExpression_LeftExpression_Expression()
        {
            var target = new Int32EqualAutoFilterStrategy("Day");
            var builder = new AutoFilterBuilder<DateTime>(target);

            var now = DateTime.Now;
            Assert.IsFalse(builder.Build("none").Compile()(now));
            Assert.IsTrue(builder.Build(now.Day.ToString()).Compile()(now));
        }
    }
}