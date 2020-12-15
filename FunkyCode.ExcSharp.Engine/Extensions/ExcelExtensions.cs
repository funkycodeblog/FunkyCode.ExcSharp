using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FunkyCode.ExcSharp.Engine
{
    public static class ExcelExtensions
    {
        public static void BorderAroundAndInside(this Border border, ExcelBorderStyle around, ExcelBorderStyle inside)
        {
            border.BorderAround(around);
            border.Top.Style = inside;
            border.Bottom.Style = inside;
            border.Left.Style = inside;
            border.Right.Style = inside;
        }

        public static string[,] GetAsTextTable(this ExcelWorksheet sheet, string range)
        {
            GetAsTable(sheet, range, out string[,] text, out object[,] objects, isText: true);
            return text;
        }

        public static object[,] GetAsObjectTable(this ExcelWorksheet sheet, string range)
        {
            GetAsTable(sheet, range, out string[,] text, out object[,] objects, isText: false);
            return objects;
        }



        public static void GetAsTable(ExcelWorksheet sheet, string range, out string[,] textTable, out object[,] objectTable, bool isText)
        {
            var address = new ExcelAddress(range);

            textTable = null;
            objectTable = null;

            if (isText)
                textTable = new string[address.Rows, address.Columns];
            else 
                objectTable = new object[address.Rows, address.Columns]; 

            var r0 = address.Start.Row;
            var c0 = address.Start.Column;

            for (var r = 0; r < address.Rows; r++)
            {
                for (var c = 0; c < address.Columns; c++)
                {
                    var obj = sheet.Cells[r + r0, c + c0].Value;
                    if (null != obj)
                    {
                        if (isText)
                        {
                            var text = sheet.Cells[r + r0, c + c0].Text;
                            textTable[r, c] = text;
                        }
                        else
                        {
                            objectTable[r, c] = obj;
                        }

                    }
                }
            }
        }
    }
}
