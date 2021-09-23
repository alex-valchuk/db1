namespace Db1.BuildingBlocks.Columns
{
    public class IntegerColumn : Column
    {
        public override ColumnType Type => ColumnType.Integer;

        public IntegerColumn(string name)
            : base(name)
        {
        }
    }
}