using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.UnitTests.Types
{
    public class ClassWithPrimitives
    {
        public bool Bool { get; set; }
        public byte Byte { get; set; }
        public sbyte Sbyte { get; set; }
        public char Char { get; set; }
        public decimal Decimal { get; set; }
        public double Double { get; set; }
        public float Float { get; set; }
        public int Int { get; set; }
        public uint Uint { get; set; }
        // public long Long { get; set; } TODO: something wrong with accuracy
        public ulong Ulong { get; set; }
        public short Short { get; set; }
        public ushort Ushort { get; set; }
    }

    public class ClassWithFigure
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Figure Figure { get; set; }
    }

    public class Figure
    {
        public double Area { get; set; }
        public double Circumference { get; set; }
    }


}
