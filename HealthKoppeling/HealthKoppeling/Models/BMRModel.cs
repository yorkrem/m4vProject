using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Models
{
    public class BMRModel
    {
        [Key]
        public string id { get; private set; }
        [Required]
        public float Bmr { get; private set; }
        [Required]
        public double EndTime { get; private set; }
        [Required]
        public string UserEmail { get; private set; }

        public BMRModel(float bmr, double endTimeNanos, string userEmail)
        {
            Validation(bmr, endTimeNanos, userEmail);
        }

        [JsonConstructor]
        public BMRModel(string id, float bmr, double endTimeNanos, string userEmail)
        {
            this.id = id;
            Bmr = bmr;
            EndTime = endTimeNanos;
            UserEmail = userEmail;
        }

        private void Validation(float bmr, double endTime, string userEmail)
        {
            this.id = Guid.NewGuid().ToString();
            if (bmr > 0)
            {
                Bmr = bmr;
            }
            else
            {
                throw new Exception("daily bmr is not valid");
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

        public void SetBMR(float bmr)
        {
            this.Bmr = bmr;
        }

    }
}
