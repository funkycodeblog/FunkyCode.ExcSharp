using System;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public class ExcelFactory : IExcelFactory
    {

        

        public IExcelSheet GetSheet(string path, string name)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(path));
            var sheet = package.Workbook.Worksheets.FirstOrDefault(s => s.Name == name) ?? package.Workbook.Worksheets.Add(name);
            return new ExcelSheet(sheet, package);
        }
    }
}
