namespace HealthKoppeling.Requests
{
    public class MoveMinutesRequest
    {
        public int MoveMinutes { get;  set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public string UserEmail { get; set; }
    }
}
