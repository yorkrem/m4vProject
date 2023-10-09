using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Models
{
    public class HeartPointModel
    {
        [Required]
        public string id { get; private set; }
        [Required]
        public int HeartPoints { get; private set; }
        [Required]
        public double StartTime { get; private set; }
        [Required]
        public double EndTime { get; private set; }
        [Required]
        public string UserEmail { get; private set; }


        [JsonConstructor]
        public HeartPointModel(string id, int heartPoints, double startTime, double endTime, string userEmail)
        {
            this.id = id;
            HeartPoints = heartPoints;
            StartTime = startTime;
            EndTime = endTime;
            UserEmail = userEmail;
        }

        public HeartPointModel(int heartPoints, double startTime, double endTime, string userEmail)
        {
            Validation(heartPoints, startTime, endTime, userEmail);
        }

        public void Validation(int heartPoints, double startTime, double endTime, string userEmail)
        {
            this.id = Guid.NewGuid().ToString();
            if (heartPoints > 0)
            {
                HeartPoints = heartPoints;
            }
            else
            {
                throw new Exception("daily heartPoints is not valid");
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

        public void SetHeartPoint(int heartPoints)
        {
            this.HeartPoints = heartPoints;
        }
    }
}
