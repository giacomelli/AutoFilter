using System;
using System.Linq.Expressions;
using AutoFilter.UnitTests.Stubs;
using NUnit.Framework;
using TestSharp;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class DoubleEqualAutoFilterStrategyTest
    {       
        [Test()]
        public void CanFilter_DiffPropertyName_False()
        {
            var target = new DoubleEqualAutoFilterStrategy("number");
            Assert.IsFalse(target.CanFilter(typeof(TargetStub).GetProperty("Double")));
        }

        [Test()]
        public void CanFilter_SamePropertyName_True()
        {
            var target = new DoubleEqualAutoFilterStrategy("Double");
            Assert.IsTrue(target.CanFilter(typeof(TargetStub).GetProperty("Double")));
        }

        [Test()]
        public void CreateExpression_LeftExpression_Expression()
        {
            var target = new DoubleEqualAutoFilterStrategy("Double");
            var builder = new AutoFilterBuilder<TargetStub>(target);
            
            var earget = new TargetStub() { Double = 1.23 };
            Assert.IsFalse(builder.Build("1.23").Compile()(earget));
            Assert.IsTrue(builder.Build(earget.Double.ToString()).Compile()(earget));
        }
    }
}