﻿namespace Db1.BuildingBlocks.Columns
{
    public abstract class Column
    {
        public string Name { get; }
        
        public abstract ColumnType Type { get; }
        
        public abstract int Size { get; }

        protected Column(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}