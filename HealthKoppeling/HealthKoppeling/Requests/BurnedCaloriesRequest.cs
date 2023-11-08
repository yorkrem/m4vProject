using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Requests
{
    public class BurnedCaloriesRequest
    {
        public float Calories { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }
}
