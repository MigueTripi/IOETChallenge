global using IOETChallenge.Business;
global using IOETChallenge.Domain;
global using NUnit.Framework;

namespace IOETChallenge.Test
{
    public class FileManager_Test
    {
        public IFileManager _fileManager;

        [OneTimeSetUp]
        public void CreateNeededObjects()
        {
            _fileManager = new FileManager();
        }

        [Test]
        public void OpenExistingFile()
        {
            var contentFileOperation = _fileManager.GetFileContent("./Data/FileManagerTest/ExistingFile.txt");
            Assert.IsTrue(contentFileOperation.success);
        }

        [Test]
        public void OpenEmptyFile()
        {
            var contentFileOperation = _fileManager.GetFileContent("./Data/FileManagerTest/EmptyFile.txt");
            Assert.IsTrue(contentFileOperation.success && contentFileOperation.Rows?.Length == 0);
        }


        [Test]
        public void OpenFileWithOneRow()
        {
            var contentFileOperation = _fileManager.GetFileContent("./Data/FileManagerTest/OneRowFile.txt");
            Assert.IsTrue(contentFileOperation.success && contentFileOperation.Rows?.Length == 1);
        }

        [Test]
        public void OpenFileWithOneHundredRow()
        {
            var contentFileOperation = _fileManager.GetFileContent("./Data/FileManagerTest/OneHundredRowFile.txt");
            Assert.IsTrue(contentFileOperation.success && contentFileOperation.Rows?.Length == 100);
        }
    }
}