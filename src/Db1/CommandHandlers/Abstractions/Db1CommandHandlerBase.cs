using System;
using System.Threading.Tasks;

namespace Db1.CommandHandlers.Abstractions
{
    public abstract class Db1CommandHandlerBase<C, R> : IDb1CommandHandler<C, R>
        where C : IDb1Command
        where R : IDb1CommandExecutionResult
    {
        protected abstract Task<R> ExecuteAsync(C command);

        public async Task<IDb1CommandExecutionResult> ExecuteAsync(IDb1Command command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (!(command is C cCommand)) throw new InvalidOperationException($"This handler works only with commands of type '{typeof(C).Name}'.");

            return await ExecuteAsync(cCommand);
        }
    }
}