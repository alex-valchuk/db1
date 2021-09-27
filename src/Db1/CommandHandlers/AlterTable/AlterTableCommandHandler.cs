using System;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.CommandHandlers.Abstractions;
using Db1.Exceptions;
using Db1.FileSystem.Abstractions;
using Newtonsoft.Json;

namespace Db1.CommandHandlers.AlterTable
{
    public class AlterTableCommandHandler : Db1CommandHandlerBase<AlterTableCommand, AlterTableCommandExecutionResult>
    {
        private readonly IFileSystemHelper _fileSystemHelper;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public AlterTableCommandHandler(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        protected override async Task<AlterTableCommandExecutionResult> ExecuteAsync(AlterTableCommand command)
        {
            var alterTableDefinition = command.TableDefinition;

            var fileName = $"{alterTableDefinition.TableName}.tbl";
            if (!_fileSystemHelper.Exists(fileName))
            {
                throw new NonExistingResourceException($"Table with name '{alterTableDefinition.TableName}' does not exists.");
            }

            return command.Action.ToLower() switch
            {
                Tokens.Add => await PerformAdditionAsync(fileName, alterTableDefinition),
                Tokens.Remove => await PerformRemovalAsync(fileName, alterTableDefinition),
                _ => throw new NotSupportedException($"The action '{command.Action}' is not supported by '{nameof(AlterTableCommandHandler)}'.")
            };
        }

        private async Task<AlterTableCommandExecutionResult> PerformAdditionAsync(string fileName, TableDefinition alterTableDefinition)
        {
            var existingTableDefinition = await GetTableDefinitionAsync(fileName);

            foreach (var newColumn in alterTableDefinition.Columns)
            {
                existingTableDefinition.AddColumn(newColumn);
            }

            await CommitAsync(fileName, existingTableDefinition);

            return new AlterTableCommandExecutionResult($"Column(s) '{string.Join(",", alterTableDefinition.Columns)}' has(ve) been successfully added.");
        }

        private async Task<TableDefinition> GetTableDefinitionAsync(string fileName)
        {
            var tableDefContent = await _fileSystemHelper.ReadAllTextAsync(fileName);
            return JsonConvert.DeserializeObject<TableDefinition>(tableDefContent, _serializerSettings);
        }

        private async Task CommitAsync(string fileName, TableDefinition finalTableDefinition)
        {
            var finalTableDefContent = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(finalTableDefinition, _serializerSettings));
            await _fileSystemHelper.WriteAllTextAsync(fileName, finalTableDefContent);
        }

        private async Task<AlterTableCommandExecutionResult> PerformRemovalAsync(string fileName, TableDefinition alterTableDefinition)
        {
            var existingTableDefinition = await GetTableDefinitionAsync(fileName);

            foreach (var columnToRemove in alterTableDefinition.Columns)
            {
                existingTableDefinition.RemoveColumn(columnToRemove);
            }

            await CommitAsync(fileName, existingTableDefinition);

            return new AlterTableCommandExecutionResult($"Column(s) '{string.Join(",", alterTableDefinition.Columns)}' has(ve) been successfully removed.");
        }
    }
}