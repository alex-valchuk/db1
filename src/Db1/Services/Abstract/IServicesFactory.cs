using Db1.BuildingBlocks;

namespace Db1.Services.Abstract
{
    public interface IServicesFactory
    {
        IInsertService GetInsertService(TableDefinition tableDefinition);
        ITableDefinitionService GetTableDefinitionService();
    }
}