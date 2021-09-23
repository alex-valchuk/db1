namespace Db1.BuildingBlocks.Columns
{
    public class VarcharColumn : Column
    {
        public override ColumnType Type => ColumnType.Varchar;

        public byte Size { get; }

        public VarcharColumn(string name, byte size)
            : base(name)
        {
            Size = size;
        }
    }
}