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
        public int StartTime { get; private set; }
        [Required]
        public int EndTime { get; private set; }

        public StepModel(int dailySteps, int startTime, int endTime)
        {
            Validation(dailySteps, startTime, endTime);
        }

        public void Validation(int dailySteps, int startTime, int endTime)
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
        }

        public void SetSteps(int steps)
        {
            this.DailySteps = steps;
        }
    }
}
