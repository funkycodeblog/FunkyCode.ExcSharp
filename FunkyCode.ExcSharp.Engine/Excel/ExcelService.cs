using System.Collections.Generic;
using FunkyCode.ExcSharp.Engine.Core;
using FunkyCode.ExcSharp.Engine.Tools;
using FunkyCode.ExcSharp.UnitTests;
using OfficeOpenXml;

namespace FunkyCode.ExcSharp.Engine
{
    public class ExcelService : IExcelService
    {
        private IExcelFactory _factory;
        private IExcelHeaderProvider _headerProvider;

        public ExcelService(IExcelFactory factory, IExcelHeaderProvider headerProvider)
        {
            _headerProvider = headerProvider;
            _factory = factory;
        }

        public void InsertObject<T>(T csharpObj, string workBookPath, string sheetName)
        {
            var sheet = _factory.GetSheet(workBookPath, sheetName);

            var info = DataPrototypeFactory.CreatePrototypeNew<T>();
            var headers = DataPrototypeFactory.GetHeaders(info);

            var table = new List<object[]>();

            var creationResult = CSharpToExcelDataConverter.FillTable(table, info, csharpObj);

            sheet.InsertTable(headers, table, creationResult);
            sheet.Save();
        }

        public T LoadObject<T>(string workBookPath, string sheetName)
        {

            var sheet = _factory.GetSheet(workBookPath, sheetName);

            var csharpHeaderInfo = DataPrototypeFactory.CreatePrototypeNew<T>();

            var height = csharpHeaderInfo.MaxLevel + 1;
            var width = csharpHeaderInfo.Width;

            var headerAddress = new ExcelAddress(1, 1, height, width);

            var lastCell = sheet.GetLastCell();
    

            var tableAddress = new ExcelAddress(height + 1, 1, lastCell.Row, lastCell.Column);

            var headerTable = sheet.GetTextTableData(headerAddress.Address);

            var excelHeaderStructure = _headerProvider.GetHeaders(headerTable);
            
            var excelHeaders = DataPrototypeFactory.GetHeaders(excelHeaderStructure);

            var table = sheet.GetObjectTableData(tableAddress.Address);

            var lastRow = table.RowCount();

            var excelLoadData = new ExcelLoadData
            {
                Headers = excelHeaders,
                Data = table
            };

            var obj = (T)csharpHeaderInfo.GetRecordFromTable(excelLoadData, typeof(T), 1, 1, lastRow, "User");

            return obj;
        }

        

    }
}
