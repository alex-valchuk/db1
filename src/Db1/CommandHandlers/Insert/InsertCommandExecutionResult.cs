using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class InsertCommandExecutionResult : IDb1CommandExecutionResult
    {
        public InsertCommandExecutionResult(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}