using System;
using System.Collections.Generic;
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
        private const byte ColumnsIndex = TokenIndex_Columns + 1;

        private const bool IgnoreCase = true;

        private readonly List<Column> _columns = new List<Column>();
 
        public string TableName { get; }

        public Column[] Columns => _columns.ToArray();
        
        public CreateTableCommand(string command)
        {
            var commandParts = command.Split(' ');
            ValidateCommand(commandParts);

            TableName = commandParts[TokenIndex_TableName];
            CollectColumns(commandParts);
        }

        private void ValidateCommand(string[] commandParts)
        {
            ValidateTokenExistence(commandParts[TokenIndex_Create], Commands.Create);
            ValidateTokenExistence(commandParts[TokenIndex_Table], Tokens.Table);
            ValidateTokenExistence(commandParts[TokenIndex_With], Tokens.With);
            ValidateTokenExistence(commandParts[TokenIndex_Columns], Tokens.Columns);
        }

        private void ValidateTokenExistence(string actualToken, string expectedToken)
        {
            if (actualToken.ToLower() != expectedToken)
            {
                throw new InvalidCommandFormatException($"Invalid command string: '{expectedToken}' is expected.");
            }
        }

        private void CollectColumns(string[] commandParts)
        {
            for (int i = ColumnsIndex; i < commandParts.Length; i += 2)
            {
                var columnName = commandParts[i]
                    .Replace("(", "")
                    .Replace(")", "")
                    .ToLower();

                var columnsTypeString = commandParts[i + 1]
                    .Replace(",", "")
                    .ToLower();

                byte? size = null;
                ColumnType columnType;

                if (columnsTypeString.StartsWith(ColumnType.Varchar.ToString().ToLower()))
                {
                    columnType = ColumnType.Varchar;
                    size = byte.Parse(columnsTypeString
                        .Substring(ColumnType.Varchar.ToString().Length)
                        .Replace("(", "")
                        .Replace(")", "")
                        .Trim());
                }
                else if (!Enum.TryParse(columnsTypeString, IgnoreCase, out columnType))
                {
                    throw new InvalidCommandFormatException("Invalid command string: column type is expected.");
                }

                _columns.Add(CreateColumnFactory.CreateColumnByType(columnName, columnType, size));
            }
        }
    }
}