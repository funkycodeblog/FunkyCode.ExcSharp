using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.Engine
{
    public static class ArraysExtensions
    {
        public static T[,] GetRange<T>(this T[,] table, int row, int col, int toRow, int toCol)
        {
            var rows = toRow - row + 1;
            var cols = toCol - col + 1;

            var range = new T[rows, cols];

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    range[r, c] = table[row + r, col + c];
                }
            }

            return range;
        }

        public static T[,] GetsRangeRows<T>(this T[,] table, int row, int toRow)
        {
            var cols = table.ColumnCount();
            var range = GetRange(table, row, 0, toRow, cols - 1);
            return range;
        }

        public static int RowCount<T>(this T[,] table)
        {
            return table.GetLength(0);
        }

        public static int ColumnCount<T>(this T[,] table)
        {
            return table.GetLength(1);
        }

        

        public static T[,] GetTransposed<T>(this T[,] table)
        {
            var rows = table.RowCount();
            var columns = table.ColumnCount();

            var newTable = new T[columns, rows];
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    newTable[c, r] = table[r, c];
                }
            }

            return newTable;
        }

        public static int GetFirstNonNullable(object[,] table, int column, int startRow)
        {
            var lastRow = table.RowCount();
            for (var r = startRow-1; r < lastRow; r++)
            {
                var value = table[r, column];
                if (value != null)  return r + 1;
            }

            return -1;
        }

    }
}
