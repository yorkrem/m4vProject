using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Requests
{
    public class BurnedCaloriesRequest
    {
        public float Calories { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
