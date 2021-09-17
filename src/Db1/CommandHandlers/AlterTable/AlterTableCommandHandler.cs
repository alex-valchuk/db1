using System;
using System.IO;
using System.Threading.Tasks;
using Db1.BuildingBlocks;
using Db1.CommandHandlers.Abstractions;
using Db1.Exceptions;
using Newtonsoft.Json;

namespace Db1.CommandHandlers.AlterTable
{
    public class AlterTableCommandHandler : Db1CommandHandlerBase<AlterTableCommand, AlterTableCommandExecutionResult>
    {
        protected override async Task<AlterTableCommandExecutionResult> ExecuteAsync(AlterTableCommand command)
        {
            var alterTableDefinition = command.TableDefinition;
            var fileName = $"{alterTableDefinition.TableName}.tbl";
            if (!File.Exists(fileName)) throw new NonExistingResourceException($"Table with name '{alterTableDefinition.TableName}' does not exists.");

            var tableDefString = await File.ReadAllTextAsync(fileName);
            var existingTableDefinition = JsonConvert.DeserializeObject<TableDefinition>(tableDefString);

            switch (command.Action)
            {
                case Tokens.Add:
                    foreach (var newColumn in alterTableDefinition.Columns)
                    {
                        existingTableDefinition.AddColumn(newColumn);
                    }
                    break;
                case Tokens.Remove:
                    foreach (var columnToRemove in alterTableDefinition.Columns)
                    {
                        existingTableDefinition.RemoveColumn(columnToRemove);
                    }
                    break;
                default:
                    throw new NotSupportedException($"The action '{command.Action}' is not supported by '{nameof(AlterTableCommandHandler)}'.");
            }

            var serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var resultTableDefContent = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(existingTableDefinition, serializerSettings));
            await File.WriteAllTextAsync(fileName, resultTableDefContent);
            
            return new AlterTableCommandExecutionResult();
        }
    }
}