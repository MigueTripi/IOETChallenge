using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOETChallenge.Domain
{
    public interface IFileManager
    {
        (bool success, string? ErrorMessage, string[]? Rows) GetFileContent(string fileName);
    }
}
