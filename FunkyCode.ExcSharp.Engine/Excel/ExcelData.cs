using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using OfficeOpenXml;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace FunkyCode.ExcSharp.Engine
{
    public static class ExcelData 
    {
        private static readonly IExcelService ExcelService;
        
        static ExcelData()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();

            var container = builder.Build();
            var scope = container.BeginLifetimeScope();
            ExcelService = scope.Resolve<IExcelService>();
        }

        public static T LoadObject<T>(string workBookPath, string sheetName)
        {
            var list = ExcelService.LoadObject<T>(workBookPath, sheetName);
            return list;
        }

        public static void InsertObject<T>(T csharpObj, string workBookPath, string sheetName)
        {
            ExcelService.InsertObject<T>(csharpObj, workBookPath, sheetName);
        }
    }
}
