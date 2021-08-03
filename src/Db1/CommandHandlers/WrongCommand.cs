using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class WrongCommand : IDb1Command
    {
        public WrongCommand(string message)
        {
            Message = $"Incorrect command syntax: {message}";
        }

        public string Message { get; }
    }
}