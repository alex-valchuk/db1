using System.IO;
using System.Threading.Tasks;
using Db1.CommandHandlers.Abstractions;
using Db1.Exceptions;
using Newtonsoft.Json;

namespace Db1.CommandHandlers
{
    public class CreateTableCommandHandler : Db1CommandHandlerBase<CreateTableCommand, CreateTableCommandExecutionResult>
    {
        protected override async Task<CreateTableCommandExecutionResult> ExecuteAsync(CreateTableCommand command)
        {
            var tableDefinition = command.TableDefinition;
            var fileName = $"{tableDefinition.TableName}.tbl";
            if (File.Exists(fileName)) throw new DuplicationException($"Table with name '{tableDefinition.TableName}' already exists.");

            var serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var tableDefContent = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(tableDefinition, serializerSettings));
            await File.WriteAllTextAsync(fileName, tableDefContent);

            return new CreateTableCommandExecutionResult($"Table '{tableDefinition.TableName}' has been successfully created.");
        }
    }
}