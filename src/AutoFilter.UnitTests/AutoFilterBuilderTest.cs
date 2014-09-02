using System;
using NUnit.Framework;
using TestSharp;

namespace AutoFilter.UnitTests
{
    [TestFixture()]
    public class AutoFilterBuilderTest
    {
        [Test()]
        public void Constructor_NullStrategies_Exception()
        {
            ExceptionAssert.IsThrowing(typeof(ArgumentNullException), () =>
            {
                new AutoFilterBuilder<DateTime>(null);
            });
        }

        [Test()]
        public void Constructor_ZeroStrategies_Exception()
        {
            ExceptionAssert.IsThrowing(typeof(ArgumentOutOfRangeException), () =>
            {
                new AutoFilterBuilder<DateTime>(new IAutoFilterStrategy[0]);
            });
        }

        [Test()]
        public void Build_TargetWithStaticProperties_IgnoreStaticProperties()
        {
            var target = new AutoFilterBuilder<DateTime>(
                 new Int32EqualAutoFilterStrategy()
            );

            var actual = target.Build(DateTime.Now.Day.ToString());
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void Build_TwoStrategies_FilterUsingStrategies()
        {
            var target = new AutoFilterBuilder<DateTime>(
                new Int32EqualAutoFilterStrategy("Day"),
                new EnumEqualAutoFilterStrategy<DayOfWeek>()
            );

            // Filter by Day.
            var actual = target.Build(DateTime.Now.Day.ToString()).Compile();
            Assert.IsTrue(actual(DateTime.Now));

            // Filter by Year.
            actual = target.Build(DateTime.Now.Year.ToString()).Compile();
            Assert.IsFalse(actual(DateTime.Now));

            // Filter by DayOfWeek.
            actual = target.Build(DateTime.Now.DayOfWeek.ToString()).Compile();
            Assert.IsTrue(actual(DateTime.Now));

            // Filter by none.
            actual = target.Build("none").Compile();
            Assert.IsFalse(actual(DateTime.Now));
        }
    }
}