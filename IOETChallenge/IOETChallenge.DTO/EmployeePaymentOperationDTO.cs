namespace IOETChallenge.DTO
{
    public class EmployeePaymentOperationDTO
    {
        /// <summary>
        /// True if at least one row could be processed
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// ErrorCode = 0  -> Without error. All rows were processed correctly.
        /// ErrorCode = 1  -> No row processed.
        /// ErrorCode = 2  -> File error.
        /// ErrorCode = 3  -> Processed but some rows with issues.
        /// ErrorCode = 10 -> Unhandled error.
        /// </summary>
        public byte ErrorCode { get; set; }
        /// <summary>
        /// Technical error. Filled when ErrorCode = 10.
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// All rows with format error
        /// </summary>
        public List<string> RowsWithErrors { get; set; }
        /// <summary>
        /// Contains the payments that must be applied to the employees
        /// </summary>
        public List<EmployeePaymentDTO> EmployeePayments { get; set; }
    
        public EmployeePaymentOperationDTO()
        {
            ErrorCode = 1;
            EmployeePayments = new List<EmployeePaymentDTO>();
            RowsWithErrors = new List<string>();
        }

    }
}