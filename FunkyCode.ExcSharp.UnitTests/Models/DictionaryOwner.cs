using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.UnitTests
{
    public class DictionaryOwner
    {
        public string Name { get; set; }
        public Dictionary<int, string> Dictionary { get; set; } = new Dictionary<int, string>();
    }

    public class ComplexDictionaryValue
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class DictionaryOwner02
    {
        public string Name { get; set; }

        public Dictionary<int, ComplexDictionaryValue> Dictionary { get; set; } = new
            Dictionary<int, ComplexDictionaryValue>();
    }
}
