﻿using System;
using System.Collections.Generic;
using System.Linq;
using Db1.BuildingBlocks.Columns;
using Db1.Exceptions;

namespace Db1.BuildingBlocks
{
    public class TableDefinition
    {
        private List<Column> _columns;

        public string TableName { get; set; }

        public Column[] Columns
        {
            get => _columns.ToArray();
            set => _columns = value.ToList();
        }

        public void AddColumn(Column newColumn)
        {
            if (newColumn == null)
            {
                throw new ArgumentNullException(nameof(newColumn));
            }

            if (_columns.Any(c => c.Name == newColumn.Name))
            {
                throw new DuplicationException($"Column with the name '{newColumn.Name}' is already exists in the '{TableName}' table.");
            }
            
            _columns.Add(newColumn);
        }

        public void RemoveColumn(Column columnToRemove)
        {
            if (columnToRemove == null)
            {
                throw new ArgumentNullException(nameof(columnToRemove));
            }

            if (_columns.All(c => c.Name != columnToRemove.Name))
            {
                throw new NonExistingResourceException($"Column with the name '{columnToRemove.Name}' does not exist in the '{TableName}' table.");
            }
            
            _columns.Remove(columnToRemove);
        }
    }
}