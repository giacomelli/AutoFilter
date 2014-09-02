using System;
using System.Linq.Expressions;
using AutoFilter.UnitTests.Stubs;
using NUnit.Framework;
using TestSharp;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class DateTimeEqualAutoFilterStrategyTest
    {       
        [Test()]
        public void CanFilter_DiffPropertyName_False()
        {
            var target = new DateTimeEqualAutoFilterStrategy("TheDate");
            Assert.IsFalse(target.CanFilter(typeof(TargetStub).GetProperty("DateTime")));
        }

        [Test()]
        public void CanFilter_SamePropertyName_True()
        {
            var target = new DateTimeEqualAutoFilterStrategy("DateTime");
            Assert.IsTrue(target.CanFilter(typeof(TargetStub).GetProperty("DateTime")));
        }

        [Test()]
        public void CreateExpression_LeftExpression_Expression()
        {
            var target = new DateTimeEqualAutoFilterStrategy("DateTime");
            var builder = new AutoFilterBuilder<TargetStub>(target);

            var now = new TargetStub() { DateTime = DateTime.Now };
            Assert.IsFalse(builder.Build("none").Compile()(now));
            Assert.IsTrue(builder.Build(now.DateTime.ToString()).Compile()(now));
        }
    }
}