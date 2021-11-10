using NUnit.Framework;
using FluentAssertions;
using System;

namespace HeartHealth.Tests.Unit
{
    [TestFixture]
    public class BloodPressure
    {
        [TestCase(0, 50)]
        [TestCase(-44, 100)]
        [TestCase(100, 0)]
        [TestCase(105, -20)]
        public void Throws_If_Systolic_Or_Diastolic_Are_Less_Than_One(int systolic, int diastolic)
        {
            Action act = () => new Domain.ValueObjects.BloodPressure(systolic, diastolic);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void BloodPressures_Are_Equal_If_Systolic_Diastolic_And_Units_Match()
        {
            var bloodPressure = new Domain.ValueObjects.BloodPressure(120, 80);
            var otherBloodPressure = new Domain.ValueObjects.BloodPressure(120, 80);

            var areEqualWithMethod = bloodPressure.Equals(otherBloodPressure);
            var areEqualWithOperator = bloodPressure == otherBloodPressure;
            var areNotEqualWithOperator = bloodPressure != otherBloodPressure;

            areEqualWithMethod.Should().BeTrue();
            areEqualWithOperator.Should().BeTrue();
            areNotEqualWithOperator.Should().BeFalse();
        }
    }
}
