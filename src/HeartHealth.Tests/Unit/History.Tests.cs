using FluentAssertions;
using HeartHealth.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HeartHealth.Tests.Unit
{
    [TestFixture]
    public class History
    {
        [Test]
        public void Calculate_Average_Systolic_And_Diastolic_Pressures()
        {
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(130, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(110, 70)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(140, 100)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 5),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(100, 60)
                }
            };
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);

            var history = new Domain.Entities.History(start, end, measurements);

            history.AverageBloodPressure.Systolic.Should().Be(120);
            history.AverageBloodPressure.Diastolic.Should().Be(80);
        }

        [Test]
        public void Average_Systolic_And_Diastolic_Are_Null_If_Given_Empty_List_Of_Measurements()
        {
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);

            var history = new Domain.Entities.History(start, end, new List<Measurement>());

            history.AverageBloodPressure.Should().BeNull();
        }

        [Test]
        public void Average_Systolic_And_Diastolic_Are_Null_If_No_Measurements_Are_Given()
        {
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);

            var history = new Domain.Entities.History(start, end, null);

            history.AverageBloodPressure.Should().BeNull();
        }

        [Test]
        public void Average_Systolic_And_Diastolic_Are_Null_If_Less_Than_Five_Measurements_Spanning_Five_Days()
        {
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);

            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(110, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(117, 72)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(121, 77)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(127, 81)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new Domain.ValueObjects.BloodPressure(119, 77)
                }
            };

            var history = new Domain.Entities.History(start, end, measurements);

            history.AverageBloodPressure.Should().BeNull();
        }
    }
}
