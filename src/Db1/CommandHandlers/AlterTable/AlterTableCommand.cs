using System;
using System.Linq;
using Db1.BuildingBlocks;
using Db1.CommandHandlers.Abstractions;
using Db1.CommandParser;
using Db1.Validators;

namespace Db1.CommandHandlers.AlterTable
{
    public class AlterTableCommand : IDb1TableCommand
    {
        private const byte TokenIndex_Alter = 0;
        private const byte TokenIndex_Table = TokenIndex_Alter + 1;
        private const byte TokenIndex_TableName = TokenIndex_Table + 1;
        private const byte TokenIndex_Action = TokenIndex_TableName + 1;
        private const byte TokenIndex_Columns = TokenIndex_Action + 1;
        private const byte ColumnsIndex = TokenIndex_Columns + 1;

        public AlterTableCommand(string[] commandParts)
        {
            ValidateCommand(commandParts);

            var tableName = commandParts[TokenIndex_TableName];
            var columns = ColumnsParser.CollectColumns(ColumnsIndex, commandParts).ToHashSet();

            Action = commandParts[TokenIndex_Action];
            TableDefinition = new TableDefinition(tableName)
            {
                Columns = columns
            };
        }

        public TableDefinition TableDefinition { get; }
        
        public string Action { get; }

        public AlterTableCommand(string action, TableDefinition tableDefinition)
        {
            Action = string.IsNullOrWhiteSpace(action) ? throw new ArgumentNullException(nameof(action)) : action;
            TableDefinition = tableDefinition ?? throw new ArgumentNullException(nameof(tableDefinition));
        }

        private void ValidateCommand(string[] commandParts)
        {
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Alter], Commands.Alter);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Table], Tokens.Table);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Action], Tokens.Add, Tokens.Remove);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Columns], Tokens.Columns);
        }
    }
}