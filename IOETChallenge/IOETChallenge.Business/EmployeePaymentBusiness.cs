using IOETChallenge.DTO;

namespace IOETChallenge.Business
{
    public class EmployeePaymentBusiness : IEmployeePaymentBusiness
    {

        private Dictionary<string, float> _employeePayments;
        private readonly IFileManager _fileManager;
        private readonly IEmployeePaymentDataWrapper _employeePaymentDataWrapper;
        private readonly IPaymentCalculator _paymentCalculator;
        public EmployeePaymentBusiness(
            IFileManager fileManager, 
            IEmployeePaymentDataWrapper employeePaymentDataWrapper,
            IPaymentCalculator paymentCalculator)
        {
            _fileManager = fileManager;
            _employeePaymentDataWrapper = employeePaymentDataWrapper;
            _paymentCalculator = paymentCalculator;
        }

        /// <summary>
        /// Calculate and return the total amount each worker will receive.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public EmployeePaymentOperationDTO CalculateEmployeePayments(string fileName, int minimumRowsToProcess = 1)
        {
            _employeePayments = new Dictionary<string, float>();
            var result = new EmployeePaymentOperationDTO();
            
            var fileContentOperation = _fileManager.GetFileContent(fileName);
            //validate file reading operation.
            if (!fileContentOperation.success)
            {
                result.Success = false;
                result.ErrorMessage = fileContentOperation.ErrorMessage;
                result.ErrorCode = (byte)EmployeePaymentOperationErrorCodesDTO.epoecFileError;

                return result;
            }

            //validate file rows lenght and leave if the it is lower that the minimum required.
            if (fileContentOperation.Rows?.Length < minimumRowsToProcess)
            {
                result.Success = false;
                result.ErrorMessage = $"There are less rows ({fileContentOperation.Rows?.Length}) than the minimum allowed ({minimumRowsToProcess}).";
                result.ErrorCode = (byte)EmployeePaymentOperationErrorCodesDTO.epoecNoRowProcessed;

                return result;
            }

            var processedAllRows = InternalCalculateEmployeePayments(fileContentOperation.Rows);

            foreach (var keyValuePair in this._employeePayments)
            {
                result.EmployeePayments.Add(new EmployeePaymentDTO()
                {
                    Name = keyValuePair.Key,
                    Amount = keyValuePair.Value
                });
            }
            result.Success = true;
            result.ErrorCode = processedAllRows ? (byte)EmployeePaymentOperationErrorCodesDTO.epoecOK : (byte)EmployeePaymentOperationErrorCodesDTO.epoecPartiallyProcessed;

            return result;
        }

        private bool InternalCalculateEmployeePayments(string[]? employeeDataToProcess)
        {
            var result = true;
            if (employeeDataToProcess == null) return true;
            
            foreach (var employeeData in employeeDataToProcess)
            {
                var wrappedData = _employeePaymentDataWrapper.GetEmployeePaymentData(employeeData);

                if (String.IsNullOrWhiteSpace(wrappedData.Employee)) continue;

                result &= wrappedData.AllPaymentDataProcessed;
                if (!this._employeePayments.ContainsKey(wrappedData.Employee)) this._employeePayments[wrappedData.Employee] = 0;
                this._employeePayments[wrappedData.Employee] += _paymentCalculator.CalculateTotalToPay(wrappedData.WorkedTime);
            }

            return result;
        }
    }
}