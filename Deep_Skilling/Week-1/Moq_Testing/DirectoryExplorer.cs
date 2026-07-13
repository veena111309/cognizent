using System.Collections.Generic;
using System.IO;

namespace Moq_Testing
{
    public class DirectoryExplorer : IDirectoryExplorer
    {
        public ICollection<string> GetFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException($"The folder at '{folderPath}' was not found.");
            }
            return Directory.GetFiles(folderPath);
        }
    }
}
