using HeartHealth.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeartHealth.Domain.Entities
{
    public class History
    {
        public DateRange DateRange{ get; }
        public BloodPressure AverageBloodPressure { get; set; }
        public IEnumerable<Measurement> Measurements => _measurements.AsReadOnly();
        private List<Measurement> _measurements;
        
        public History(DateTime start, DateTime end, List<Measurement> measurements)
        {
            DateRange = new DateRange(start, end);
            _measurements = measurements;

            CalculateAverageBloodPressure();
        }

        private void CalculateAverageBloodPressure()
        {
            if (IsSmallSampleSize() || IsNotEvenDistribution()) return;

            var averageSystolic = (int)_measurements.Average(m => m.BloodPressure.Systolic);
            var averageDiastolic = (int)_measurements.Average(m => m.BloodPressure.Diastolic);

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
    }
}
