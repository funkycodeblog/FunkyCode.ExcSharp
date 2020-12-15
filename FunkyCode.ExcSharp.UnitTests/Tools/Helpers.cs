using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute.Exceptions;

namespace FunkyCode.ExcSharp.UnitTests
{
    public class Helpers
    {
        public static string TrimExcelWorksheetName(string name)
        {
            return name.Substring(0, 31);
        }

    }
}
