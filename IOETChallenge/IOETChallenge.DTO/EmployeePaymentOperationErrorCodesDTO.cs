using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOETChallenge.DTO
{
    public enum EmployeePaymentOperationErrorCodesDTO
    {
        epoecOK = 0,
        epoecNoRowProcessed = 1,
        epoecFileError = 2,
        epoecPartiallyProcessed = 3,
        epoecUnhandledError = 10
    }
}
