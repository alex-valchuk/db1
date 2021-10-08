using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class InsertCommand : IDb1Command
    {
        public InsertCommand(string[] commandParts)
        {
        }

        public string TableName { get; }

        public string[] Columns { get; } = new string[0];

        public string[][] Rows { get; } = new string[0][];
    }
}