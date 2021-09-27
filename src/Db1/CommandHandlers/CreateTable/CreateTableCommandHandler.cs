using System;
using System.Threading.Tasks;
using Db1.CommandHandlers.Abstractions;
using Db1.Exceptions;
using Db1.FileSystem.Abstractions;
using Newtonsoft.Json;

namespace Db1.CommandHandlers
{
    public class CreateTableCommandHandler : Db1CommandHandlerBase<CreateTableCommand, CreateTableCommandExecutionResult>
    {
        private readonly IFileSystemHelper _fileSystemHelper;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public CreateTableCommandHandler(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        protected override async Task<CreateTableCommandExecutionResult> ExecuteAsync(CreateTableCommand command)
        {
            var tableDefinition = command.TableDefinition;
            var fileName = $"{tableDefinition.TableName}.tbl";
            if (_fileSystemHelper.Exists(fileName)) throw new DuplicationException($"Table with name '{tableDefinition.TableName}' already exists.");

            var tableDefContent = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(tableDefinition, _serializerSettings));
            await _fileSystemHelper.WriteAllTextAsync(fileName, tableDefContent);

            return new CreateTableCommandExecutionResult($"Table '{tableDefinition.TableName}' has been successfully created.");
        }
    }
}