using System;
using Db1.CommandHandlers.Abstractions;
using Db1.CommandHandlers.AlterTable;
using Db1.FileSystem.Abstractions;

namespace Db1.CommandHandlers
{
    public class CommandHandlersFactory
    {
        private readonly IFileSystemHelper _fileSystemHelper;

        public CommandHandlersFactory(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        public IDb1CommandHandler GetHandlerByCommand(IDb1Command command)
        {
            return command switch
            {
                CreateTableCommand _ => new CreateTableCommandHandler(),
                AlterTableCommand _ => new AlterTableCommandHandler(_fileSystemHelper),

                InsertCommand _ => new InsertCommandHandler(),
                _ => null
            };
        }
    }
}