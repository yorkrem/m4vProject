namespace HealthKoppeling.Requests
{
    public class HeartPointRequest
    {
        public int HeartPoints { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public string UserEmail { get; set; }
    }
}
