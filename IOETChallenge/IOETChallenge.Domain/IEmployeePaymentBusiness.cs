using IOETChallenge.DTO;

namespace IOETChallenge.Domain
{
    public interface IEmployeePaymentBusiness
    {
        EmployeePaymentOperationDTO CalculateEmployeePayments(string fileName, int minimumRowsToProcess = 1);
    }
}