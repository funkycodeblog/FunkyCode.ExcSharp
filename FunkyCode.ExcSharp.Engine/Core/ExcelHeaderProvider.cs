using System;
using FunkyCode.ExcSharp.Engine.Tools;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public class ExcelHeaderProvider : IExcelHeaderProvider
    {
        public ObjectData GetHeaders(string[,] headerTable)
        {
            var objData = new ObjectData(null);

            GetFromTable(objData, headerTable);

            return objData;
        }


        static void GetFromTable(ObjectData objData, string[,] table)
        {
            var rows = table.GetLength(0);
            var columns = table.GetLength(1);


            for (var c = 0; c < columns; c++)
            {
                var value = table[0, c];
                var isProperty = IsProperty(table, 0, c);
                if (isProperty)
                {
                    var iPrimitive = new PrimitiveObjectData(objData)
                    {
                        Parent = objData,
                        Name = value,
                        DataType = ObjectData.DataTypeEnum.Primitive
                    };
                    objData.Children.Add(iPrimitive);
                    continue;
                }

                var lastCol = GetLastColumn(table, 0, c);
                var subTable = table.GetRange(1, c, rows - 1, lastCol);
                var subObjData = new ObjectData(objData) { Parent = objData };
                objData.Children.Add(subObjData);

                subObjData.Name = value;
                GetFromTable(subObjData, subTable);
                c = lastCol;
            }
        }

        static bool IsProperty(string[,] table, int row, int column)
        {

            var value = table[row, column];
            if (value == null) return false;

            var rows = table.GetLength(0);
            if (row == rows - 1) return true;

            for (var r = row + 1; r < rows; r++)
            {
                var iValue = table[r, column];
                if (iValue != null) return false;
            }

            return true;
        }

        static int GetLastColumn(string[,] table, int row, int column)
        {
            var columns = table.GetLength(1);

            if (column == columns - 1) return column;

            for (var c = column + 1; c < columns; c++)
            {
                var value = table[row, c];
                if (value != null)
                    return c - 1;
            }

            return columns - 1;
        }
    }
}
