using System;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public interface IExcelTableRangeResolver
    {
        string GetTableRange(IExcelSheet sheet, string tableNameAddress);
    }

    public class ExcelTableRangeResolver : IExcelTableRangeResolver
    {
        public string GetTableRange(IExcelSheet sheet, string tableNameAddress)
        {
            var startAddress = new ExcelAddress(tableNameAddress);

            var lastCell = sheet.GetLastCell();

            var scopeAddress = new ExcelAddress(startAddress.Start.Row + 1, startAddress.Start.Column, lastCell.Row, lastCell.Column);

            var c = scopeAddress.Start.Column;
            var r = scopeAddress.Start.Row;

            var cx = c;
            var rx = r;

            while (true)
            {
                var isEmptyColumn = false;
                var isEmptyRow = false;

                while (!isEmptyColumn || !isEmptyRow)
                {
                    if (!isEmptyColumn)
                        cx = GetColumnCandidate(r, cx + 1, scopeAddress.End.Column, sheet);

                    if (!isEmptyRow)
                        rx = GetRowCandidate(c, rx + 1, scopeAddress.End.Row, sheet);

                    if (rx == -1 && cx == -1)
                        return null;

                    isEmptyColumn = CheckIsEmptyColumn(cx, r, rx, sheet);
                    isEmptyRow = CheckIsEmptyRow(rx, c, cx, sheet);
                }

                if (isEmptyRow && isEmptyColumn)
                {
                    var tableAddress = new ExcelAddress(scopeAddress.Start.Row, scopeAddress.Start.Column, rx - 1, cx - 1);
                    return tableAddress.Address;
                }
            }
        }


        private static int GetColumnCandidate(int r, int cx, int cLast, IExcelSheet sheet)
        {
            for (var c = cx; c <= cLast; c++)
            {
                if (string.IsNullOrEmpty(sheet.GetCellText(r, c))) return c;
            }

            return -1;
        }

        private static int GetRowCandidate(int c, int rx, int rLast, IExcelSheet sheet)
        {
            for (var r = rx; r <= rLast; r++)
            {
                if (string.IsNullOrEmpty(sheet.GetCellText(r, c))) return r;
            }

            return -1;
        }

        private static bool CheckIsEmptyColumn(int c, int r0, int rx, IExcelSheet sheet)
        {
            for (var r = r0; r <= rx; r++)
            {
                if (!string.IsNullOrEmpty(sheet.GetCellText(r, c))) return false;
            }

            return true;
        }

        private static bool CheckIsEmptyRow(int r, int c0, int cx, IExcelSheet sheet)
        {
            for (var c = c0; c <= cx; c++)
            {
                if (!string.IsNullOrEmpty(sheet.GetCellText(r, c))) return false;
            }

            return true;
        }
    }
}
