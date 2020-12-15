using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.Engine.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string str)
        {
            var isNumeric =
                byte.TryParse(str, out _) ||
                sbyte.TryParse(str, out _) ||
                decimal.TryParse(str, out _) ||
                double.TryParse(str, out _) ||
                float.TryParse(str, out _) ||
                int.TryParse(str, out _) ||
                uint.TryParse(str, out _) ||
                long.TryParse(str, out _) ||
                ulong.TryParse(str, out _) ||
                short.TryParse(str, out _) ||
                ushort.TryParse(str, out _);

            return isNumeric;
        }
        
    }
}
