using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Db1.FileSystem.Abstractions;

namespace Db1.FileSystem
{
    public class FileSystemHelper : IFileSystemHelper
    {
        public bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public Task<string> ReadAllTextAsync(string fileName)
        {
            return File.ReadAllTextAsync(fileName);
        }

        public Task WriteAllTextAsync(string fileName, string contents)
        {
            return File.WriteAllTextAsync(fileName, contents);
        }

        public Task AppendAllLinesAsync(string fileName, IEnumerable<string> lines)
        {
            return File.AppendAllLinesAsync(fileName, lines);
        }

        public string[] GetFileNamesWithPrefix(string directory, string filePrefix)
        {
            return Directory
                .GetFiles(directory)
                .Where(f => f.Contains(filePrefix, StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }
    }
}