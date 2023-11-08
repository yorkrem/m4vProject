using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Models
{
    public class BurnedCaloriesModel
    {
        [Key]
        public string id { get; private set; }
        [Required]
        public float Calories { get; private set; }

        [Required]
        public string StartTime { get; private set; }
        [Required]
        public string EndTime { get; private set; }


        [JsonConstructor]
        public BurnedCaloriesModel(string id, float calories, string startTime, string endTime)
        {
            this.id = id;
            Calories = calories;
            StartTime = startTime;
            EndTime = endTime;
        }

        public BurnedCaloriesModel(float calories, string startTime, string endTime)
        {
            Validation(calories, startTime, endTime);   
        }

        private void Validation(float calories, string startTime,string endTime)
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
            if (startTime != "")
            {
                StartTime = startTime;
            }
            else
            {
                throw new Exception("start time is not valid");
            }
            if (endTime != "")
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
