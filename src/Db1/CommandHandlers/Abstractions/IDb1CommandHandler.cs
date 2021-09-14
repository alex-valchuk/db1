using System.Threading.Tasks;

namespace Db1.CommandHandlers.Abstractions
{
    public interface IDb1CommandHandler
    {
        Task<IDb1CommandExecutionResult> ExecuteAsync(IDb1Command command);
    }

    public interface IDb1CommandHandler<in C, R> : IDb1CommandHandler
        where C : IDb1Command
        where R : IDb1CommandExecutionResult
    {
    }
}