using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.Engine
{
    public static class ConverterFactory
    {
        private static readonly Dictionary<Type, Func<object, object>> _dict = new Dictionary<Type, Func<object, object>>
        {
            {typeof(bool), (o) => (object)Convert.ToBoolean(o)},
            {typeof(byte), (o) => (object)Convert.ToByte(o)},
            {typeof(sbyte), (o) => (object)Convert.ToSByte(o)},
            {typeof(char), (o) => (object)Convert.ToChar(o)},
            {typeof(decimal), (o) => (object)Convert.ToDecimal(o)},
            {typeof(double), (o) => (object)Convert.ToDouble(o)},
            {typeof(float), (o) => (object)Convert.ToSingle(o)},
            {typeof(int), (o) => (object)Convert.ToInt32(o)},
            {typeof(uint), (o) => (object)Convert.ToUInt32(o)},
            {typeof(long), (o) => (object)Convert.ToInt64(o)},
            {typeof(ulong), (o) => (object)Convert.ToUInt64(o)},
            {typeof(short), (o) => (object)Convert.ToInt16(o)},
            {typeof(ushort), (o) => (object)Convert.ToUInt16(o)},
            {typeof(string), (o) => (object)Convert.ToString(o)}
        };
        
        public static object GetValue(Type type, object obj)
        {
            var func = _dict[type];
            var val = func(obj);
            return val;

        }
    }
   
}
