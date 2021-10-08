using System.Collections.Generic;
using System.Threading.Tasks;
using Db1.BuildingBlocks;

namespace Db1.Services.Abstract
{
    public interface IInsertService
    {
        Task ExecuteAsync(List<string> rows, TableDefinition tableDefinition);
    }
}