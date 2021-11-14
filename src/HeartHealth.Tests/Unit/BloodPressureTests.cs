using NUnit.Framework;
using FluentAssertions;
using System;
using HeartHealth.Domain.Shared;
using HeartHealth.Domain.ValueObjects;

namespace HeartHealth.Tests.Unit
{
    [TestFixture]
    public class BloodPressureTests
    {
        [TestCase(0, 50)]
        [TestCase(-44, 100)]
        [TestCase(100, 0)]
        [TestCase(105, -20)]
        public void Throws_If_Systolic_Or_Diastolic_Are_Less_Than_One(int systolic, int diastolic)
        {
            Action act = () => new BloodPressure(systolic, diastolic);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void BloodPressures_Are_Equal_If_Systolic_Diastolic_And_Units_Match()
        {
            var bloodPressure = new BloodPressure(120, 80);
            var otherBloodPressure = new BloodPressure(120, 80);

            var areEqualWithMethod = bloodPressure.Equals(otherBloodPressure);
            var areEqualWithOperator = bloodPressure == otherBloodPressure;
            var areNotEqualWithOperator = bloodPressure != otherBloodPressure;

            areEqualWithMethod.Should().BeTrue();
            areEqualWithOperator.Should().BeTrue();
            areNotEqualWithOperator.Should().BeFalse();
        }

        [TestCase(119, 79, Stages.Normal)]
        [TestCase(121, 79, Stages.Elevated)]
        [TestCase(121, 81, Stages.Stage1)]
        [TestCase(131, 79, Stages.Stage1)]
        [TestCase(132, 82, Stages.Stage1)]
        [TestCase(141, 70, Stages.Stage2)]
        [TestCase(119, 91, Stages.Stage2)]
        [TestCase(143, 93, Stages.Stage2)]
        [TestCase(181, 70, Stages.Crisis)]
        [TestCase(119, 121, Stages.Crisis)]
        [TestCase(147, 131, Stages.Crisis)]
        public void Get_Approprate_Stage_For_BloodPressure(int systolic, int diastolic, Stages expected)
        {
            var bloodPressure = new BloodPressure(systolic, diastolic);

            bloodPressure.Stage.Should().Be(expected);
        }
    }
}
