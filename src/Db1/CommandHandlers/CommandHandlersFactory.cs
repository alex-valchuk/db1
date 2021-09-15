using Db1.CommandHandlers.Abstractions;
using Db1.CommandHandlers.AlterTable;

namespace Db1.CommandHandlers
{
    public class CommandHandlersFactory
    {
        public IDb1CommandHandler GetHandlerByCommand(IDb1Command command)
        {
            return command switch
            {
                CreateTableCommand _ => new CreateTableCommandHandler(),
                AlterTableCommand _ => new AlterTableCommandHandler(),

                InsertCommand _ => new InsertCommandHandler(),
                _ => null
            };
        }
    }
}