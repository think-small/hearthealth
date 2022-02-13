using HeartHealth.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeartHealth.Domain.Entities
{
    public class History
    {
        public Guid Id { get; set; }
        public DateRange DateRange{ get; }
        public BloodPressure AverageBloodPressure { get; set; }
        public IEnumerable<Measurement> Measurements => _measurements.AsReadOnly();
        private List<Measurement> _measurements;

        public History(){ }
        public History(DateTime start, DateTime end, List<Measurement> measurements)
        {
            DateRange = new DateRange(start, end);
            _measurements = measurements;

            CalculateAverageBloodPressure();
        }

        private void CalculateAverageBloodPressure()
        {
            if (IsSmallSampleSize() || IsNotEvenDistribution()) return;

            var averageSystolic = (int)Math.Round(_measurements.Average(m => m.BloodPressure.Systolic));
            var averageDiastolic = (int)Math.Round(_measurements.Average(m => m.BloodPressure.Diastolic));

            AverageBloodPressure = new BloodPressure(averageSystolic, averageDiastolic);
        }

        private bool IsSmallSampleSize()
        {
            var minimumSampleSize = 5;
            return _measurements is null || _measurements.Count < minimumSampleSize;
        }

        /// <summary>
        /// Ensures there is at least one measurement for five consecutive days.
        /// </summary>
        /// <returns>true if requirement is NOT met</returns>
        private bool IsNotEvenDistribution()
        {
            var requiredConsecutiveDays = 5;
            var aggregatedMeasurements = _measurements.GroupBy(m => m.Timestamp.Date)
                                                      .OrderBy(m => m.Key.Date)
                                                      .ToList();
            var uniqueDays = aggregatedMeasurements.Count();
            var consecutiveDays = 1;
            for (int i = 0; i < uniqueDays - 1; i++)
            {
                if (aggregatedMeasurements[i].Key.AddDays(1).Date == aggregatedMeasurements[i + 1].Key.Date)
                {
                    consecutiveDays++;
                }
                else
                {
                    consecutiveDays = 0;
                }
            }

            return consecutiveDays < requiredConsecutiveDays;
        }

        /// <summary>
        /// Adds new measurement and updates AverageBloodPressure.<br />
        /// Compares the new measurement's BloodPressure.Stage with AverageBloodPressure.Stage. User should confirm delta checks with another measurement.
        /// </summary>
        /// <param name="measurement">New measurement submitted from user.</param>
        /// <returns>
        /// True if measurement is consistent with AverageBloodPressure.Stage.
        /// Returns false if measurement is at least one stage higher.
        /// </returns>
        public bool AddMeasurement(Measurement measurement)
        {
            if (measurement is null) return false;

            DeltaCheckFor(measurement);
            _measurements.Add(measurement);
            if (measurement.BloodPressure.Stage > AverageBloodPressure.Stage) return false;

            CalculateAverageBloodPressure();
            return true;
        }

        //  DeltaCheck should only apply if a running average has been established.
        private void DeltaCheckFor(Measurement measurement)
        {
            var recentMeasurement = GetMeasurementsBy(DateRange).Last();
            measurement.RequiresVerification = AverageBloodPressure != null
                                            && recentMeasurement.BloodPressure.Stage < measurement.BloodPressure.Stage;
        }

        public IEnumerable<Measurement> GetMeasurementsBy(DateTime dateTime)
        {
            return _measurements.Where(m => m.Timestamp.Date == dateTime.Date)
                                .OrderBy(m => m.Timestamp);
        }

        public IEnumerable<Measurement> GetMeasurementsBy(DateRange dateRange)
        {
            return _measurements.Where(m => dateRange.Has(m.Timestamp))
                                .OrderBy(m => m.Timestamp);                         
        }
    }
}
