using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class CreateTableCommandExecutionResult : IDb1CommandExecutionResult
    {
        public CreateTableCommandExecutionResult(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}