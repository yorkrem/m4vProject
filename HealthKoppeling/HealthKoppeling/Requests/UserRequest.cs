namespace HealthKoppeling.Requests
{
    public class UserRequest
    {
        public string name { get; private set; }
        public string email { get; private set; }
        public string accessToken { get; private set; }

        public UserRequest(string name, string email, string accesstoken)
        {
            this.name = name;
            this.email = email;
            this.accessToken = accesstoken;
        }
    }
}
