using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class HeaderInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public int Level { get; set; }

        public int LastColumn => Column + Width - 1;
        public int LastRow => Row + Height - 1;

        public override string ToString() => FullName;

    }
}
