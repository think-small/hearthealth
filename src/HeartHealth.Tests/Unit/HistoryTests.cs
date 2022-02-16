using FluentAssertions;
using HeartHealth.Domain.Entities;
using HeartHealth.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeartHealth.Tests.Unit
{
    [TestFixture]
    public class HistoryTests
    {
        [Test]
        public void Calculate_Average_Systolic_And_Diastolic_Pressures()
        {
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new BloodPressure(130, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new BloodPressure(110, 70)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new BloodPressure(140, 100)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 5),
                    BloodPressure = new BloodPressure(100, 60)
                }
            };
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);

            var history = new History(start, end, measurements);

            history.AverageBloodPressure.Systolic.Should().Be(120);
            history.AverageBloodPressure.Diastolic.Should().Be(80);
        }

        [Test]
        public void Average_Systolic_And_Diastolic_Are_Null_If_Given_Empty_List_Of_Measurements()
        {
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);

            var history = new History(start, end, new List<Measurement>());

            history.AverageBloodPressure.Should().BeNull();
        }

        [Test]
        public void Average_Systolic_And_Diastolic_Are_Null_If_No_Measurements_Are_Given()
        {
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);

            var history = new History(start, end, null);

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
                    BloodPressure = new BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new BloodPressure(110, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new BloodPressure(117, 72)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new BloodPressure(121, 77)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new BloodPressure(127, 81)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new BloodPressure(119, 77)
                }
            };

            var history = new History(start, end, measurements);

            history.AverageBloodPressure.Should().BeNull();
        }

        [Test]
        public void History_Includes_Added_BloodPressure_With_Same_Stage_And_Updates_AverageBloodPressure()
        {
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new BloodPressure(130, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new BloodPressure(110, 70)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new BloodPressure(140, 100)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 5),
                    BloodPressure = new BloodPressure(100, 60)
                }
            };
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);
            var sut = new History(start, end, measurements);
            var newMeasurement = new Measurement
            {
                Timestamp = new DateTime(2020, 1, 5),
                BloodPressure = new BloodPressure(129, 89)
            };

            sut.AddMeasurement(newMeasurement);

            newMeasurement.RequiresVerification.Should().BeFalse();
            sut.AverageBloodPressure.Should().BeEquivalentTo(new BloodPressure(122, 82));
            sut.Measurements.Should().HaveCount(6);
        }

        [Test]
        public void Adding_Measurement_With_Stage_Higher_Than_Average_Blood_Pressure_Will_Not_Recalculate_Running_Average()
        {
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new BloodPressure(130, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new BloodPressure(110, 70)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new BloodPressure(140, 100)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 5),
                    BloodPressure = new BloodPressure(100, 60)
                }
            };
            var newMeasurement = new Measurement
            {
                Timestamp = new DateTime(2020, 1, 5),
                BloodPressure = new BloodPressure(140, 120)
            };

            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);
            var sut = new History(start, end, measurements);
            var originalAverageBloodPressure = sut.AverageBloodPressure;

            sut.AddMeasurement(newMeasurement);

            newMeasurement.RequiresVerification.Should().BeTrue();
            originalAverageBloodPressure.Should().BeEquivalentTo(sut.AverageBloodPressure);
        }

        [Test]
        public void Adding_Measurement_With_Stage_Higher_Than_Average_Blood_Pressure_Will_Mark_It_As_Requiring_Verification()
        {
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new BloodPressure(130, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new BloodPressure(110, 70)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new BloodPressure(140, 100)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 5),
                    BloodPressure = new BloodPressure(100, 60)
                }
            };
            var newMeasurement = new Measurement
            {
                Timestamp = new DateTime(2020, 1, 5),
                BloodPressure = new BloodPressure(140, 85)
            };
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);
            var sut = new History(start, end, measurements);

            sut.AddMeasurement(newMeasurement);

            var addedMeasurement = sut.GetMeasurementsBy(newMeasurement.Timestamp)
                                      .FirstOrDefault(m => m == newMeasurement);

            addedMeasurement.RequiresVerification.Should().BeTrue();
        }

        [Test]
        public void Adding_Measurement_With_Stage_Equal_To_Average_Blood_Pressure_Will_Not_Mark_It_As_Requiring_Verification()
        {
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 1),
                    BloodPressure = new BloodPressure(120, 80)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 2),
                    BloodPressure = new BloodPressure(130, 90)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 3),
                    BloodPressure = new BloodPressure(110, 70)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 4),
                    BloodPressure = new BloodPressure(140, 100)
                },
                new Measurement
                {
                    Timestamp = new DateTime(2020, 1, 5),
                    BloodPressure = new BloodPressure(100, 60)
                }
            };
            var newMeasurement = new Measurement
            {
                Timestamp = new DateTime(2020, 1, 5),
                BloodPressure = new BloodPressure(90, 75)
            };
            var start = new DateTime(2020, 1, 1);
            var end = new DateTime(2020, 1, 5);
            var sut = new History(start, end, measurements);

            sut.AddMeasurement(newMeasurement);

            var addedMeasurement = sut.GetMeasurementsBy(newMeasurement.Timestamp)
                                      .FirstOrDefault(m => m == newMeasurement);

            addedMeasurement.RequiresVerification.Should().BeFalse();
        }
    }
}
