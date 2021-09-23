using System.Threading.Tasks;

namespace Db1.FileSystem.Abstractions
{
    public interface IFileSystemHelper
    {
        bool Exists(string fileName);
        Task<string> ReadAllTextAsync(string fileName);
        Task WriteAllTextAsync(string fileName, string contents);
    }
}