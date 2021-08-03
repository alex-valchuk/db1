using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class CommandHandlersFactory
    {
        public IDb1CommandHandler GetHandlerByCommand(IDb1Command command)
        {
            if (command is InsertCommand)
            {
                return new InsertCommandHandler();
            }

            if (command is CreateTableCommand)
            {
                return new CreateTableCommandHandler();
            }

            return null;
        }
    }
}