namespace IOETChallenge.Domain
{
    public interface IEmployeePaymentDataWrapper
    {
        (string Employee, bool AllPaymentDataProcessed, Dictionary<string, List<TimeRange>> WorkedTime) GetEmployeePaymentData(string employeePaymentData);
    }
}
