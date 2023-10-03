using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Requests
{
    public class BurnedCaloriesRequest
    {
        public float Calories { get; set; }
        public double StartTimeNanos { get; set; }
        public double EndTimeNanos { get; set; }
        public string UserEmail { get; set; }
    }
}
