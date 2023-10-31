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
        public string StartDate { get; private set; }
        [Required]
        public string EndDate { get; private set; }

        [JsonConstructor]
        public StepModel(string id, int dailySteps, string startDate, string endDate)
        {
            this.id = id;
            DailySteps = dailySteps;
            StartDate = startDate;
            EndDate = endDate;
        }

        public StepModel(int dailySteps, string startDate, string endDate)
        {
            Validation(dailySteps, startDate, endDate);
        }

        public void Validation(int dailySteps, string startDate, string endDate)
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
            if(startDate != "")
            {
                StartDate = startDate;
            }
            else
            {
                throw new Exception("start time is not valid");
            }
            if (endDate != "")
            {
                EndDate = endDate;
            }
            else
            {
                throw new Exception("end time is not valid");
            }
        }

        public void SetSteps(int steps)
        {
            this.DailySteps = steps;
        }
    }
}
