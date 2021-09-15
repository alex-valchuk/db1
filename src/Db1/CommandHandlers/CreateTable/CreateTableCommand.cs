﻿using Db1.BuildingBlocks.Columns;
using Db1.CommandHandlers.Abstractions;
using Db1.CommandParser;
using Db1.Validators;

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

        public string TableName { get; }

        public Column[] Columns { get; }
        
        public CreateTableCommand(string[] commandParts)
        {
            ValidateCommand(commandParts);

            TableName = commandParts[TokenIndex_TableName];

            Columns = ColumnsParser.CollectColumns(ColumnsIndex, commandParts);
        }

        private void ValidateCommand(string[] commandParts)
        {
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Create], Commands.Create);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Table], Tokens.Table);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_With], Tokens.With);
            TokenValidator.ValidateTokenExistence(commandParts[TokenIndex_Columns], Tokens.Columns);
        }
    }
}