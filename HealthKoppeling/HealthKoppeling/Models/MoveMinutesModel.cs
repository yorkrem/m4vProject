using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Models
{
    public class MoveMinutesModel
    {
        [Key]
        public string id { get; private set; }
        [Required]
        public int moveMinutes { get; private set; }
        [Required]
        public double StartTime { get; private set; }
        [Required]
        public double EndTime { get; private set; }
        [Required]
        public string UserEmail { get; private set; }

        [JsonConstructor]
        public MoveMinutesModel(string id, int moveMinutes, double startTime, double endTime, string userEmail)
        {
            this.id = id;
            this.moveMinutes = moveMinutes;
            StartTime = startTime;
            EndTime = endTime;
            UserEmail = userEmail;
        }

        public MoveMinutesModel(int moveMinutes, double startTime, double endTime, string userEmail)
        {
            Validation(moveMinutes, startTime, endTime, userEmail);
        }

        private void Validation(int moveMinutes, double startTime, double endTime, string userEmail)
        {
            this.id = Guid.NewGuid().ToString();
            if (moveMinutes > 0)
            {
                this.moveMinutes = moveMinutes;
            }
            else
            {
                throw new Exception("daily steps is not valid");
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

        public void SetMoveMinutes(int moveMinutes)
        {
            this.moveMinutes = moveMinutes;
        }
    }
}
