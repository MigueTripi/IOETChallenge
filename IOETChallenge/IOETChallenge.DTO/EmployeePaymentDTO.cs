namespace IOETChallenge.DTO
{
    public class EmployeePaymentDTO
    {
        /// <summary>
        /// Employee's name provided in the file
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Amount to be paid
        /// </summary>
        public float Amount { get; set; }
        /// <summary>
        /// Currency to apply the payment. USD by default
        /// </summary>
        public string Currency { get; set; } = "USD";
    }
}