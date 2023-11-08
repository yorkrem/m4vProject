using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace HealthKoppeling.Models
{
    public class BMRModel
    {
        [Key]
        public string id { get; private set; }
        [Required]
        public float Calories { get; private set; }
        [Required]
        public double StartTime { get; private set; }
        [Required]
        public double EndTime { get; private set; }

        [JsonConstructor]
        public BMRModel(string id, float calories, double startTime, double endTime) {
            this.id = id;
            Calories = calories;
            StartTime = startTime;
            EndTime = endTime;
        }

        public BMRModel(float calories, double startTime, double endTime)
        {
            Validation(calories, startTime, endTime);
        }

        private void Validation(float calories, double startTime, double endTime)
        {
            this.id = Guid.NewGuid().ToString();
            if (calories > 0)
            {
                Calories = calories;
            }
            else
            {
                throw new Exception("daily burnedcalories is not valid");
            }
            if (startTime > 0)
            {
                StartTime = startTime;
            }
            else
            {
                throw new Exception("start time is not valid");
            }
            if (endTime > 0)
            {
                EndTime = endTime;
            }
            else
            {
                throw new Exception("end time is not valid");
            }
        }

        public void SetCalories(float calories)
        {
            this.Calories = calories;
        }

    }
}
