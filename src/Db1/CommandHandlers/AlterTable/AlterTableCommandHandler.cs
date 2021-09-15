using System.Threading.Tasks;
using Db1.CommandHandlers.Abstractions;

namespace Db1.CommandHandlers.AlterTable
{
    public class AlterTableCommandHandler : Db1CommandHandlerBase<AlterTableCommand, AlterTableCommandExecutionResult>
    {
        protected override Task<AlterTableCommandExecutionResult> ExecuteAsync(AlterTableCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}