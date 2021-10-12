using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.FileSystem.Abstractions;
using Db1.Services.Abstract;

namespace Db1.Services
{
    public class SimplestInsertService : IInsertService
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly IFileSystemHelper _fileSystemHelper;

        public SimplestInsertService(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        public async Task ExecuteAsync(List<string> rows, TableDefinition tableDefinition)
        {
            await _semaphore.WaitAsync();

            try
            {
                await InsertAsync(rows, tableDefinition);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private Task InsertAsync(List<string> rows, TableDefinition tableDefinition)
        {
            var fileNameToSaveTo = $"{tableDefinition.TableName}.data";

            return _fileSystemHelper.AppendAllLinesAsync(fileNameToSaveTo, rows);
        }
    }
}