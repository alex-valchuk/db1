using System;
using System.Collections.Generic;
using System.Text;

namespace Db1.Helpers
{
    public static class RowParser
    {
        /// <summary>
        /// It is considered that row is trimmed.
        /// </summary>
        /// <param name="row">String containing column values.</param>
        /// <returns>Array of column values.</returns>
        /// <exception cref="ArgumentException">When 'row' argument is invalid.</exception>
        public static string[] Parse(string row)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                throw new ArgumentException(nameof(row));
            }

            var columnValues = new List<string>();
            var varcharModeOn = false;
            var valueAssemblingModeOn = true;

            var valueBasket = new StringBuilder();
            
            for (var i = 0; i < row.Length; i++)
            {
                var sign = row[i];
                switch (sign)
                {
                    case '\'':
                        varcharModeOn = !varcharModeOn;
                        rows.Add(new List<string>());
                        break;
                    
                    default:
                    {
                        if (sign == ',' && valueBasket.Length == 0) // end of row
                        {
                            // ignore
                            break;
                        }
                            
                        if (EndOfValueSigns.Contains(sign))
                        {
                            rows[i].Add(valueBasket.ToString());
                            valueBasket = new StringBuilder();
                        }
                        else
                        {
                            valueBasket.Append(sign);
                        }

                        break;
                    }
                }
            }

            return columnValues.ToArray();
        }
    }
}