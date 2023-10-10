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
        public double StartTime { get; private set; }
        [Required]
        public double EndTime { get; private set; }
        [Required]
        public string UserEmail { get; private set;  }


        [JsonConstructor]
        public BurnedCaloriesModel(string id, float calories, double startTime, double endTime, string userEmail)
        {
            this.id = id;
            Calories = calories;
            StartTime = startTime;
            EndTime = endTime;
            UserEmail = userEmail;
        }

        public BurnedCaloriesModel(float calories, double startTime, double endTime, string userEmail)
        {
            Validation(calories, startTime, endTime, userEmail);   
        }

        private void Validation(float calories, double startTime,double endTime, string userEmail)
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
            if (userEmail.Contains("@"))
            {
                UserEmail = userEmail;
            }
            else
            {
                throw new Exception("email is not valid");
            }
        }

        public void SetCalories(float calories)
        {
            this.Calories = calories;
        }
    }
}
