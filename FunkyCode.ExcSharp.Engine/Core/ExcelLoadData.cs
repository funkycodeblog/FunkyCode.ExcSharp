using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.ExcSharp.Engine.Tools;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public class ExcelLoadData
    {
        public List<HeaderInfo> Headers { get; set; }
        public object[,] Data { get; set; }
    }
}
