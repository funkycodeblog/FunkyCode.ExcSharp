using System;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public interface IExcelWorkbook : IDisposable
    {
        IExcelSheet GetSheet(string name);
    }
}
