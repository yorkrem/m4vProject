namespace HealthKoppeling.Requests
{
    public class StepRequest
    {
        public int DailySteps { get; set; }
        public double StartTimeNanos { get; set; }
        public double EndTimeNanos { get; set;}
        public string UserEmail { get; set; }
    }
}
