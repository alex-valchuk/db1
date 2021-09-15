using Db1.BuildingBlocks.Columns;
using Db1.CommandHandlers.Abstractions;
using Db1.CommandParser;
using Db1.Validators;

namespace Db1.CommandHandlers.AlterTable
{
    public class AlterTableCommand : IDb1Command
    {
        private const byte TokenIndex_Alter = 0;
        private const byte TokenIndex_Table = TokenIndex_Alter + 1;
        private const byte TokenIndex_TableName = TokenIndex_Table + 1;
        private const byte TokenIndex_Action = TokenIndex_TableName + 1;
        private const byte TokenIndex_Columns = TokenIndex_Action + 1;
        private const byte ColumnsIndex = TokenIndex_Columns + 1;
 
        public string TableName { get; }
        
        public string Action { get; }

        public Column[] Columns { get; }

        public AlterTableCommand(string[] commandParts)
        {
            ValidateCommand(commandParts);

            TableName = commandParts[TokenIndex_TableName];
            Action = commandParts[TokenIndex_Action];
            
            Columns = ColumnsParser.CollectColumns(ColumnsIndex, commandParts);
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