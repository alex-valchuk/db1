using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class InsertCommandExecutionResult : IDb1CommandExecutionResult
    {
        public InsertCommandExecutionResult(string message, int count)
        {
            Message = message;
            Count = count;
        }

        public string Message { get; }

        public int Count { get; }
    }
}