using System.Collections.Generic;
using System.Text.RegularExpressions;
using Db1.CommandHandlers.Abstractions;
using Db1.Exceptions;

namespace Db1.CommandHandlers
{
    public class CreateTableCommand : IDb1Command
    {
        private const byte TokenIndex_Create = 0;
        private const byte TokenIndex_Table = TokenIndex_Create + 1;
        private const byte TokenIndex_TableName = TokenIndex_Table + 1;
        private const byte TokenIndex_With = TokenIndex_TableName + 1;
        private const byte TokenIndex_Columns = TokenIndex_With + 1;

        private const string TokenPattern = @"[-\w\d_]+";
        private readonly string ColumnsPattern = $@"(?<=with columns\t*\(){TokenPattern}\t*(,\t*{TokenPattern}\t*)*(<=\))";

        private readonly List<Column> _columns = new List<Column>();
 
        public string TableName { get; }

        public Column[] Columns => _columns.ToArray();
        
        public CreateTableCommand(string command)
        {
            var commandParts = command.Split(' ');
            TableName = commandParts[TokenIndex_TableName];
            CollectColumns(command);
        }

        private void CollectColumns(string command)
        {
            if (!Regex.IsMatch(command, ColumnsPattern))
            {
                throw new InvalidCommandFormatException("Columns are not in a correct format.");
            }

            var columnsString = Regex.Match(command, ColumnsPattern).Value;
            var columns = columnsString.Split(',');
        }
    }
}