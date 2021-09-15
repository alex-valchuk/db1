using System;
using System.Collections.Generic;
using Db1.BuildingBlocks.Columns;
using Db1.Exceptions;

namespace Db1.CommandParser
{
    public static class ColumnsParser
    {
        private const bool IgnoreCase = true;

        public static Column[] CollectColumns(int columnsIndex, string[] commandParts)
        {
            var columns = new List<Column>();
            for (int i = columnsIndex; i < commandParts.Length; i += 2)
            {
                var columnName = commandParts[i]
                    .Replace("(", "")
                    .ToLower();

                var columnsTypeString = commandParts[i + 1]
                    .Replace(")", "")
                    .Replace(",", "")
                    .Replace(";", "")
                    .ToLower();

                byte? size = null;
                ColumnType columnType;

                if (columnsTypeString.StartsWith(ColumnType.Varchar.ToString().ToLower()))
                {
                    columnType = ColumnType.Varchar;
                    size = byte.Parse(columnsTypeString
                        .Substring(ColumnType.Varchar.ToString().Length)
                        .Replace("(", "")
                        .Replace(")", "")
                        .Trim());
                }
                else if (!Enum.TryParse(columnsTypeString, IgnoreCase, out columnType))
                {
                    throw new InvalidCommandFormatException("Invalid command string: column type is expected.");
                }

                columns.Add(CreateColumnFactory.CreateColumnByType(columnName, columnType, size));
            }
            
            return columns.ToArray();
        }
    }
}