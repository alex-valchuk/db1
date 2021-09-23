using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers.AlterTable
{
    public class AlterTableCommandExecutionResult : IDb1CommandExecutionResult
    {
        public AlterTableCommandExecutionResult(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}