using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.FileSystem.Abstractions;
using Db1.Services.Abstract;
using Newtonsoft.Json;

namespace Db1.Services
{
    public class InsertService : IInsertService
    {
        private const int PageSize = 3;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None
        };

        private readonly IFileSystemHelper _fileSystemHelper;

        public InsertService(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        public async Task ExecuteAsync(List<string> rows, TableDefinition tableDefinition)
        {
            await _semaphore.WaitAsync();
            
            await InsertAsync(rows, tableDefinition);
            
            _semaphore.Release();
        }

        private Task InsertAsync(List<string> rows, TableDefinition tableDefinition)
        {
            var insertTasks = new List<Task>();

            foreach (var row in rows)
            {

                insertTasks.Add(_fileSystemHelper.WriteAllTextAsync(page.Key, contents));
            }

            return Task.WhenAll(insertTasks);
        }

        private string GetFileNameToSaveTo(TableDefinition tableDefinition)
        {
            var filePrefix = $"{tableDefinition.TableName}-pages-";

            var pages = _fileSystemHelper.GetFileNamesWithPrefix(tableDefinition.TableName, filePrefix);
            if (pages.Any())
            {
                var pageName = pages.Last();
                var lastPageRange = pageName.Replace(filePrefix, "").Split('-');
                    
                var numberOfExistingEntries = int.Parse(lastPageRange[1]); 
                if (numberOfExistingEntries == PageSize)
                {
                    pageName = $"{filePrefix}{numberOfExistingEntries + 1}-{numberOfExistingEntries + 1 + PageSize}";
                }

                return pageName;
            }
            
            return $""
        }

        private List<string> GetFileData(string fileName, InsertCommand command)
        {
            throw new NotImplementedException();
        }
    }
}