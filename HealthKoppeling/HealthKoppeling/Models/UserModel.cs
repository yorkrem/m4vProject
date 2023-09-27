namespace HealthKoppeling.Models
{
    public class UserModel
    {
        public string name { get; private set; }
        public string email { get; private set; }
        public string accessToken { get; private set; }

        public UserModel(string name, string email, string accesstoken)
        {
            Validation(name, email, accesstoken);
        }

        private void Validation(string name, string email, string accesstoken)
        {
            if (name != null)
            {
                this.name = name;
            }
            else
            {
                throw new Exception("name may not be empty");
            }
            if (email != null && email.Contains("@"))
            {
                this.email = email;
            }
            else
            {
                throw new Exception("this is not a valid email");
            }
            if (accesstoken != null)
            {
                this.accessToken = accesstoken;
            }
            else
            {
                throw new Exception("accesstoken may not be empty");
            }
        }
    }
}
