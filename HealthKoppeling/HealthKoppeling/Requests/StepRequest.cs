namespace HealthKoppeling.Requests
{
    public class StepRequest
    {
        public int DailySteps { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set;}
        public string UserEmail { get; set; }
    }
}
