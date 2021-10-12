using System.Collections.Generic;
using System.Threading.Tasks;

namespace Db1.FileSystem.Abstractions
{
    public interface IFileSystemHelper
    {
        bool Exists(string fileName);
        Task<string> ReadAllTextAsync(string fileName);
        Task WriteAllTextAsync(string fileName, string contents);
        Task AppendAllLinesAsync(string fileName, IEnumerable<string> lines);
        string[] GetFileNamesWithPrefix(string directory, string filePrefix);
    }
}