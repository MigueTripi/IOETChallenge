global using IOETChallenge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOETChallenge.Business
{
    public class FileManager : IFileManager
    {
        public (bool success, string? ErrorMessage, string[]? Rows) GetFileContent(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return (false, $"File is not located at {fileName}.", null);
            }

            try
            {
                var fileContent = File.ReadAllLines(fileName);
                return (true, null, fileContent);
            }
            catch(Exception ex)
            {
                return (false,ex.Message, null);
            }


        }
    }
}
