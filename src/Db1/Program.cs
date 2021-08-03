using System;
using System.Threading.Tasks;
using Db1.CommandHandlers;
using Db1.CommandParser;

namespace Db1
{
    public static class Program
    {
        private static readonly DbCommandParser _commandParser = new DbCommandParser();
        private static readonly CommandHandlersFactory _commandHandlersFactory = new CommandHandlersFactory();
        
        public static async Task Main()
        {
            Console.WriteLine("Db1 is started.");

            var exitCommandReceived = false;
            while (!exitCommandReceived)
            {
                try
                {
                    var command = await _commandParser.GetDbCommandAsync();
                    switch (command)
                    {
                        case WrongCommand wrongCommand:
                            Console.WriteLine(wrongCommand.Message);
                            continue;
                        
                        case QuitCommand _:
                            exitCommandReceived = true;
                            break;
                        
                        default:
                        {
                            var commandHandler = _commandHandlersFactory.GetHandlerByCommand(command);
                            await commandHandler.ExecuteAsync(command);
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Db1 is finishing it's work.");
        }
    }
}
