using Db1.BuildingBlocks;

namespace Db1.CommandHandlers.Abstractions
{
    public interface IDb1TableCommand : IDb1Command
    {
        TableDefinition TableDefinition { get; }
    }
}