using System;

namespace Db1.BuildingBlocks.Columns
{
    public static class CreateColumnFactory
    {
        public static Column CreateColumnByType(string columnName, ColumnType columnType, byte? size = null)
        {
            switch (columnType)
            {
                case ColumnType.Integer:
                    return new IntegerColumn(columnName);
                
                case ColumnType.Varchar:
                    return new VarcharColumn(columnName, size.Value);
                
                default:
                    throw new NotImplementedException($"Column type '{columnType}' is not implemented.");
            }
        }
    }
}