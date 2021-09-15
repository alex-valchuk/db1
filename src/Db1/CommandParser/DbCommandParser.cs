using System;
using System.Threading.Tasks;
using Db1.CommandHandlers;
using Db1.CommandHandlers.Abstractions;
using Db1.CommandHandlers.AlterTable;
using Db1.Exceptions;

namespace Db1.CommandParser
{
    public class DbCommandParser
    {
        public async Task<IDb1Command> GetDbCommandAsync()
        {
            var commandText = (await Console.In.ReadLineAsync())?.Trim();
            if (string.IsNullOrWhiteSpace(commandText))
            {
                return new WrongCommand(commandText);
            }

            if (!commandText.EndsWith(";"))
            {
                throw new InvalidCommandFormatException("Command must end with ';'");
            }

            var commandParts = commandText.Split(" ");
            switch (commandParts[0].ToLower())
            {
                case Commands.Create:
                    return new CreateTableCommand(commandParts);
                
                case Commands.Alter:
                    return new AlterTableCommand(commandParts);
                
                case Commands.Insert:
                    return new InsertCommand(commandParts);
                
                case Commands.Quit:
                case Commands.QuitShort:
                    return new QuitCommand();

                default:
                    return new WrongCommand(commandText);
            }
        }
    }
}