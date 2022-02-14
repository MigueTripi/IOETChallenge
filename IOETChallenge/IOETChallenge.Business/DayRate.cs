namespace IOETChallenge.Business
{
    internal class DayRate
    {
        public string Day { get; set; }
        public TimeSpan HourFrom { get; set; }
        public TimeSpan HourTo { get; set; }
        public float Rate { get; set; }
    }
}