using System.Linq;
using System.Text.RegularExpressions;
using Db1.CommandHandlers.Abstractions;
using Db1.Helpers;
using Db1.Validators;

namespace Db1.CommandHandlers
{
    public class InsertCommand : IDb1Command
    {
        private const byte TokenIndex_Insert = 0;
        private const byte TokenIndex_Into = TokenIndex_Insert + 1;
        private const byte TokenIndex_TableName = TokenIndex_Into + 1;
        private const byte TokenIndex_Columns = TokenIndex_TableName + 1;
        private const byte TokenIndex_Rows = TokenIndex_Columns + 1;

        private const string RowsPattern = "(?<=\\().+(?=\\) *(,|\n|]$))";
        
        public InsertCommand(string[] commandParts)
        {
            ValidateCommand(commandParts);

            TableName = commandParts[TokenIndex_TableName];
            Columns = GetColumns(commandParts[TokenIndex_Columns]);
            Rows = GetRows(commandParts[TokenIndex_Rows]);
        }

        public string TableName { get; }

        public string[] Columns { get; }

        public string[][] Rows { get; }

        private void ValidateCommand(string[] commandParts)
        {
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Insert], Commands.Insert);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Into], Tokens.Into);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Rows], Tokens.Rows);
        }

        private string[] GetColumns(string columnsPart)
        {
            // (?<=\[).+(?=\])
            return columnsPart.Split(',');
        }

        private string[][] GetRows(string rowsPart)
        {
            var rawRows = Regex.Matches(rowsPart, RowsPattern, RegexOptions.IgnoreCase);
            return rawRows
                .Select(r => r.Value.Trim())
                .Select(RowParser.Parse)
                .ToArray();
        }
    }
}