using System;
using System.Collections.Generic;
using System.Linq;
using FunkyCode.ExcSharp.Engine.Tools;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public class ExcelSheet : IExcelSheet
    {
        private readonly ExcelWorksheet _sheet;
        private ExcelPackage _package;

        public ExcelSheet(ExcelWorksheet sheet, ExcelPackage package)
        {
            _package = package;
            _sheet = sheet;
        }

        public void Dispose()
        {
            _sheet?.Dispose();
            _package?.Dispose();
        }

        public List<string> GetTableNames()
        {
            throw new NotImplementedException();
        }

        public string GetTableAddress(string name)
        {
            var nameToFind = $"{{{{{name}}}}}";

            var address = _sheet.Dimension.Address;

            foreach (var cell in _sheet.Cells[address])
                if (cell.Text == nameToFind)
                    return cell.Address;

            return null;
        }

        public string GetCellText(int row, int column)
        {
            return _sheet.Cells[row, column].Text;
        }

        public object GetCellObject(int row, int column)
        {
            return _sheet.Cells[row, column].Value;
        }

        public string[,] GetTextTableData(string address)
        {
            var table = _sheet.GetAsTextTable(address);
            return table;
        }

        public object[,] GetObjectTableData(string address)
        {
            var table = _sheet.GetAsObjectTable(address);
            return table;
        }

        public void SetValue(int row, int column, object value)
        {
            _sheet.Cells[row, column].Value = value;
        }

        public ExcelCellCoords GetLastCell()
        {
            return new ExcelCellCoords
            {
                Column = _sheet.Dimension.End.Column,
                Row = _sheet.Dimension.End.Row
            };
        }

        public void InsertTable(List<HeaderInfo> headers, List<object[]> data, CreateTableResult result)
        {
            foreach (var header in headers)
            {
                _sheet.Cells[header.Row, header.Column].Value = header.Name;

                var headerRange = _sheet.Cells[header.Row, header.Column, header.LastRow, header.LastColumn];
                headerRange.Merge = true;
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            if (data.Count == 0) return;

            var width = data[0].Length;
            var colStart = 1;
            var colEnd = colStart + width - 1;

            var heightOffset = headers.Max(h => h.Level);

            for (var r = 0; r < data.Count; r++)
            for (var c = 0; c < width; c++)
                _sheet.Cells[r + 1 + heightOffset, c + 1].Value = data[r][c];


            var tableRange = _sheet.Cells[heightOffset + 1, 1, heightOffset + result.TotalHeight,
                data[0].Length];

            tableRange.Style.Border.BorderAroundAndInside(ExcelBorderStyle.Thick, ExcelBorderStyle.Dotted);

            var iRow = heightOffset + 1;
            foreach (var h in result.Heights)
            {
                var recordRange = _sheet.Cells[iRow, colStart, iRow + h - 1, colEnd];
                recordRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                iRow += h;
            }

            tableRange.AutoFitColumns();
        }

        public void Save()
        {
            _package.Save();
        }
    }
}
