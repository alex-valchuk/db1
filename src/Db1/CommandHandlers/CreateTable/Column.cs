namespace Db1.CommandHandlers
{
    public abstract class Column
    {
        public string Name { get; }
        
        public abstract ColumnType Type { get; }

        public Column(string name)
        {
            Name = name;
        }
    }
}