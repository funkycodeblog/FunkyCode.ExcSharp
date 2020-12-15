using System;
using System.Collections.Generic;
using FunkyCode.ExcSharp.Engine.Tools;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public interface IExcelSheet : IDisposable
    {
        List<string> GetTableNames();
        string GetTableAddress(string tableName);

        string GetCellText(int row, int column);
        object GetCellObject(int row, int column);
        ExcelCellCoords GetLastCell();
        string[,] GetTextTableData(string address);
        object[,] GetObjectTableData(string address);

        void SetValue(int row, int column, object value);


        void InsertTable(List<HeaderInfo> headers, List<object[]> data, CreateTableResult result);

        void Save();
    }


    public struct ExcelCellCoords
    {
        public int Row { get; set; }
        public int Column { get; set; }


    }
}
