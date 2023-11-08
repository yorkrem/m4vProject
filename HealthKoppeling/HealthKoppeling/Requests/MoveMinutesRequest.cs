namespace HealthKoppeling.Requests
{
    public class MoveMinutesRequest
    {
        public int MoveMinutes { get;  set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string UserEmail { get; set; }
    }
}
