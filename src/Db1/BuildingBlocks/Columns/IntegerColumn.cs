namespace Db1.BuildingBlocks.Columns
{
    public class IntegerColumn : Column
    {
        public override ColumnType Type => ColumnType.Integer;

        public override int Size => 10; // 2147483647 is a max value which consists of 10 numbers

        public int MaxValue => int.MaxValue;

        public IntegerColumn(string name)
            : base(name)
        {
        }
    }
}