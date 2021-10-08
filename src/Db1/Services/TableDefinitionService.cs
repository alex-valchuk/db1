using System;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.Exceptions;
using Db1.FileSystem.Abstractions;
using Db1.Services.Abstract;
using Newtonsoft.Json;

namespace Db1.Services
{
    public class TableDefinitionService : ITableDefinitionService
    {
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None
        };

        private readonly IFileSystemHelper _fileSystemHelper;

        public TableDefinitionService(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        public async Task<TableDefinition> GetTableDefinitionAsync(string tableName)
        {
            var tableDefFileName = $"{tableName}.tbl";
            if (!_fileSystemHelper.Exists(tableDefFileName)) throw new NonExistingResourceException($"Table with name '{tableName}' does not exists.");

            var tableDefContent = await _fileSystemHelper.ReadAllTextAsync(tableDefFileName);
            return JsonConvert.DeserializeObject<TableDefinition>(tableDefContent, _serializerSettings);
        }
    }
}