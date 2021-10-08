using System.Threading.Tasks;
using Db1.BuildingBlocks;

namespace Db1.Services.Abstract
{
    public interface ITableDefinitionService
    {
        Task<TableDefinition> GetTableDefinitionAsync(string tableName);
    }
}