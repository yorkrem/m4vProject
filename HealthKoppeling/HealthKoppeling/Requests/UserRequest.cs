namespace HealthKoppeling.Requests
{
    public class UserRequest
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string AccessToken { get; private set; }

        public UserRequest(string name, string email, string accesstoken)
        {
            this.Name = name;
            this.Email = email;
            this.AccessToken = accesstoken;
        }
    }
}
