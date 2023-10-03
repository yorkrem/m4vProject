using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Models
{
    public class StepModel
    {
        [Key]
        public string id { get; private set; }
        [Required]
        public int DailySteps { get; private set; }
        [Required]
        public double StartTime { get; private set; }
        [Required]
        public double EndTime { get; private set; }
        [Required]
        public string UserEmail { get; private set; }

        [JsonConstructor]
        public StepModel(string id, int dailySteps, double startTime, double endTime, string userEmail)
        {
            this.id = id;
            DailySteps = dailySteps;
            StartTime = startTime;
            EndTime = endTime;
            UserEmail = userEmail;
        }

        public StepModel(int dailySteps, double startTime, double endTime, string userEmail)
        {
            Validation(dailySteps, startTime, endTime, userEmail);
        }

        public void Validation(int dailySteps, double startTime, double endTime, string userEmail)
        {
            this.id = Guid.NewGuid().ToString();
            if(dailySteps > 0)
            {
                DailySteps = dailySteps;
            }
            else
            {
                throw new Exception("daily steps is not valid");
            }
            if(startTime > 0)
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

        public void SetSteps(int steps)
        {
            this.DailySteps = steps;
        }
    }
}
