using System.Collections.Generic;

namespace Moq_Testing
{
    public interface IDirectoryExplorer
    {
        ICollection<string> GetFiles(string folderPath);
    }
}
