using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Db1.CommandHandlers.Abstractions;
using Db1.Exceptions;

namespace Db1.CommandHandlers
{
    public class CreateTableCommandHandler : Db1CommandHandlerBase<CreateTableCommand, CreateTableCommandExecutionResult>
    {
        protected override Task<CreateTableCommandExecutionResult> ExecuteAsync(CreateTableCommand command)
        {
            var fileName = $"{command.TableName}.tbl";
            if (File.Exists(fileName)) throw new DuplicationException($"Table with name '{command.TableName}' already exists.");

            var fileContentBuilder = new StringBuilder();
            fileContentBuilder.AppendLine($"{command.TableName}:");
            fileContentBuilder.AppendLine("Columns:");
            foreach (var column in command.Columns)
            {
                fileContentBuilder.AppendLine(column.GetColumnDefinition());
            }
            
            File.WriteAllText(fileName, fileContentBuilder.ToString());

            Console.WriteLine($"Table '{command.TableName}' has been successfully created.");
            return Task.FromResult(new CreateTableCommandExecutionResult());
        }
    }
}