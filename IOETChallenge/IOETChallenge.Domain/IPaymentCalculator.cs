namespace IOETChallenge.Domain
{
    public interface IPaymentCalculator
    {
        float CalculateTotalToPay(Dictionary<string, List<TimeRange>> workedTime);
    }
}
