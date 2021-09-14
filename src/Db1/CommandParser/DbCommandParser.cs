using System;
using System.Threading.Tasks;
using Db1.CommandHandlers;
using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandParser
{
    public class DbCommandParser
    {
        public async Task<IDb1Command> GetDbCommandAsync()
        {
            var commandText = await Console.In.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(commandText))
            {
                return new WrongCommand(commandText);
            }

            var commandParts = commandText.Split(" ");
            switch (commandParts[0].ToLower())
            {
                case Commands.Create:
                    return new CreateTableCommand(commandParts);
                
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