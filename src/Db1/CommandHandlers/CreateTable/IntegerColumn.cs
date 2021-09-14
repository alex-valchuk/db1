namespace Db1.CommandHandlers
{
    public class IntegerColumn : Column
    {
        public override ColumnType Type => ColumnType.Integer;

        public IntegerColumn(string name)
            : base(name)
        {
        }

        public override string GetColumnDefinition()
        {
            return $"{Name} {Type}";
        }
    }
}