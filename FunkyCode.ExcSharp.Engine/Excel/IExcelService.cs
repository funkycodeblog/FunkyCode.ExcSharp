using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.Engine
{
    public interface  IExcelService
    {
        void InsertObject<T>(T obj, string workBookPath, string sheetName);
        T LoadObject<T>(string workBookPath, string sheetName);
    }
}
