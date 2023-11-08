using System.ComponentModel.DataAnnotations;

namespace HealthKoppeling.Models
{
    public class UserModel
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Email { get; private set; }
        [Required]
        public string AccessToken { get; private set; }

        public UserModel(string name, string email, string accesstoken)
        {
            this.id = Guid.NewGuid().ToString();
            Validation(name, email, accesstoken);
        }

        private void Validation(string name, string email, string accesstoken)
        {
            if (name != null)
            {
                this.Name = name;
            }
            else
            {
                throw new Exception("name may not be empty");
            }
            if (email != null && email.Contains("@"))
            {
                this.Email = email;
            }
            else
            {
                throw new Exception("this is not a valid email");
            }
            if (accesstoken != null)
            {
                this.AccessToken = accesstoken;
            }
            else
            {
                throw new Exception("accesstoken may not be empty");
            }
        }
    }
}
