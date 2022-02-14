using System.Linq;

namespace IOETChallenge.Test
{
    public class EmployeePaymentBusiness_Test
    {
        public IFileManager _fileManager;
        private IEmployeePaymentDataWrapper _employeePaymentDataWrapper;
        private IPaymentCalculator _paymentCalculator;
        private IEmployeePaymentBusiness _employeePaymentBusiness;

        [OneTimeSetUp]
        public void CreateNeededObjects()
        {
            _fileManager = new FileManager();
            _employeePaymentDataWrapper = new EmployeePaymentDataWrapper();
            _paymentCalculator = new PaymentCalculator();
            _employeePaymentBusiness = new EmployeePaymentBusiness(_fileManager, _employeePaymentDataWrapper, _paymentCalculator);
        }

        [Test]
        public void CalculateOneEmployeeOneDay()
        {
            var result = _employeePaymentBusiness.CalculateEmployeePayments(
                "./Data/EmployeePaymentBusinessTest/OneEmployeeOneDay.txt");
            Assert.AreEqual(result.EmployeePayments.First().Amount, 15);
        }

        [Test]
        public void CalculateOneEmployeeOneDayTwoRateRange()
        {
            var result = _employeePaymentBusiness.CalculateEmployeePayments(
                "./Data/EmployeePaymentBusinessTest/OneEmployeeOneDayTwoRateRange.txt");
            Assert.AreEqual(result.EmployeePayments.First().Amount, 55);
        }

        [Test]
        public void CalculateOneEmployeeOneDayThreeRateRangesInTwoLines()
        {
            var result = _employeePaymentBusiness.CalculateEmployeePayments(
                "./Data/EmployeePaymentBusinessTest/OneEmployeeOneDayThreeRateRangeInTwoLines.txt");
            Assert.AreEqual(result.EmployeePayments.First().Amount, 75);
        }

        [Test]
        public void CalculateTwoEmployeeOneDay()
        {
            var result = _employeePaymentBusiness.CalculateEmployeePayments(
                "./Data/EmployeePaymentBusinessTest/TwoEmployeesOneDay.txt");
            var martinEmployee = result.EmployeePayments.First(x => x.Name == "MARTIN");
            var claraEmployee = result.EmployeePayments.First(x => x.Name == "CLARA");

            Assert.AreEqual(martinEmployee.Amount, 15);
            Assert.AreEqual(claraEmployee.Amount, 15);
        }

        [Test]
        public void CalculateOneEmployeeTwoDays()
        {
            var result = _employeePaymentBusiness.CalculateEmployeePayments(
                "./Data/EmployeePaymentBusinessTest/OneEmployeeTwoDays.txt");
            Assert.AreEqual(result.EmployeePayments.First().Amount, 30);
        }

        [Test]
        public void CalculateOneEmployeeTwoDaysTwoRange()
        {
            var result = _employeePaymentBusiness.CalculateEmployeePayments(
                "./Data/EmployeePaymentBusinessTest/OneEmployeeTwoDaysTwoRange.txt");
            Assert.AreEqual(result.EmployeePayments.First().Amount, 70);
        }

        [Test]
        public void CalculateTwoEmployeesOneDayThreeRateRangeInTwoLines()
        {
            var result = _employeePaymentBusiness.CalculateEmployeePayments(
                "./Data/EmployeePaymentBusinessTest/TwoEmployeesOneDayThreeRateRangeInTwoLines.txt");
            var juanEmployee = result.EmployeePayments.First(x => x.Name == "JUAN");
            var sofiaEmployee = result.EmployeePayments.First(x => x.Name == "SOFIA");

            Assert.AreEqual(juanEmployee.Amount, 60);
            Assert.AreEqual(sofiaEmployee.Amount, 75);
        }
    }
}