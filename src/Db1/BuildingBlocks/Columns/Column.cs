namespace Db1.BuildingBlocks.Columns
{
    public abstract class Column
    {
        public string Name { get; }
        
        public abstract ColumnType Type { get; }

        public Column(string name)
        {
            Name = name;
        }

        public abstract string GetColumnDefinition();
    }
}