using System.Linq;
using OfficeOpenXml;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public class ExcelWorkbook : IExcelWorkbook
    {
        private readonly ExcelPackage _excelPackage;

        public ExcelWorkbook(ExcelPackage excelPackage)
        {
            _excelPackage = excelPackage;
        }

      

        public IExcelSheet GetSheet(string name)
        {
            var sheet = _excelPackage.Workbook.Worksheets.FirstOrDefault(s => s.Name == name);
            return new ExcelSheet(sheet, _excelPackage);
        }

        public void Dispose()
        {
            _excelPackage?.Dispose();
        }
    }
}
