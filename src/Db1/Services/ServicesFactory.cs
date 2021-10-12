using System;
using Db1.BuildingBlocks;
using Db1.FileSystem.Abstractions;
using Db1.Services.Abstract;

namespace Db1.Services
{
    public class ServicesFactory : IServicesFactory
    {
        private readonly IFileSystemHelper _fileSystemHelper;

        public ServicesFactory(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper ?? throw new ArgumentNullException(nameof(fileSystemHelper));
        }

        public IInsertService GetInsertService(TableDefinition tableDefinition)
        {
            if (tableDefinition == null)
            {
                throw new ArgumentNullException(nameof(tableDefinition));
            }

            return new SimplestInsertService(_fileSystemHelper);
        }

        public ITableDefinitionService GetTableDefinitionService()
        {
            return new TableDefinitionService(_fileSystemHelper);
        }
    }
}