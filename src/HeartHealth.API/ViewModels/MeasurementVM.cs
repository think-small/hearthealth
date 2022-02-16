using System;

namespace HeartHealth.API.ViewModels
{
    public class MeasurementVM
    {
        public DateTime Timestamp { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public string Units { get; set; }
        public bool RequiresVerification { get; set; }
    }
}
