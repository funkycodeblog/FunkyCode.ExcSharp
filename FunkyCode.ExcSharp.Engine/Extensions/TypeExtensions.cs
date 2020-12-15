using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using FunkyCode.ExcSharp.Engine.Tools;

namespace FunkyCode.ExcSharp.Engine
{
    public static class TypeExtensions
    {

        public static bool IsCollection(this Type type)
        {

            var isCollection = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
           return isCollection;
        }

        public static bool IsDictionary(this Type type)
        {
            var isDictionary = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
            return isDictionary;
        }


        public static bool IsReferenceType(this Type type)
        {

            var isReferenceType = type.IsClass && !(type == typeof(string));
            return isReferenceType;
        }

        public static ObjectData.DataTypeEnum ResolveDataType(Type type)
        {
            if (type.IsCollection()) return ObjectData.DataTypeEnum.Collection;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                return ObjectData.DataTypeEnum.Dictionary;
            if (type.IsReferenceType()) return ObjectData.DataTypeEnum.Class;
            return ObjectData.DataTypeEnum.Primitive;
        }
    }

    public static class ObjectDataFactory
    {

        public static ObjectData CreateObjectData(Type type, ObjectData parent)
        {
            
            if (type.IsCollection())
            {
                var objData = new CollectionObjectData(parent);
                return objData;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var objData = new DictionaryObjectData(parent);
                return objData;
            }

            if (type.IsReferenceType())
            {
                return new ComplexObjectData(parent);
            }

            return new PrimitiveObjectData(parent);

        }

    }
}
