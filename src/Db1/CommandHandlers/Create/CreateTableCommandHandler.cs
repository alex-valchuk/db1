using System.Threading.Tasks;
using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers
{
    public class CreateTableCommandHandler : IDb1CommandHandler<CreateTableCommand, CreateTableCommandExecutionResult>
    {
        public Task<CreateTableCommandExecutionResult> ExecuteAsync(CreateTableCommand command)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDb1CommandExecutionResult> ExecuteAsync(IDb1Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}