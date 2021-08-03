using System;
using System.Threading.Tasks;
using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class InsertCommandHandler : IDb1CommandHandler<InsertCommand, InsertCommandExecutionResult>
    {
        public Task<InsertCommandExecutionResult> ExecuteAsync(InsertCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<IDb1CommandExecutionResult> ExecuteAsync(IDb1Command command)
        {
            throw new NotImplementedException();
        }
    }
}