using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class DataPrototypeFactory
    {


        public static ObjectData CreatePrototypeNew<T>()
        {
            var type = typeof(T);
            var name = type.Name;

            var obj = ObjectDataFactory.CreateObjectData(type, null);
            obj.Create(name, type);
            return obj;
        }
     
        public static List<HeaderInfo> GetHeaders(ObjectData objectData)
        {
            var headers = new List<HeaderInfo>();
            GetHeaders(headers, objectData, objectData.MaxLevel);
            return headers;
        }

        static void GetHeaders(List<HeaderInfo> headers, ObjectData objectData, int height)
        {
            for (var i = 0; i < objectData.Children.Count; i++)
            {
                var child = objectData.Children[i];
                var name = child.Name;

                var headerHeight = 1;
                if (child.DataType == ObjectData.DataTypeEnum.Primitive)
                    headerHeight = height - objectData.Level;
                
                var header = new HeaderInfo
                {
                    Name = name,
                    FullName = child.FullName,
                    Column = child.Position,
                    Row = objectData.Level + 1,
                    Width = child.Width,
                    Height = headerHeight,
                    Level = child.Level
                };
                headers.Add(header);

                GetHeaders(headers, child, height);
            }

        }


        

        //public static int GetHeight(object obj)
        //{
        //    var type = obj.GetType();
        //    var resolvedType = TypeExtensions.ResolveDataType(type);

        //    var max = 1;

        //    if (resolvedType == ObjectData.DataTypeEnum.Dictionary)
        //    {
        //        var dictionary = (IDictionary) obj;
        //        return dictionary.Count;
        //    }

        //    if (type.IsCollection())
        //    {
        //        var list = (IList) obj;
        //        max = list.Count;

        //        var collection = (IList) obj;
        //        foreach (var iCol in collection)
        //        {
        //            var iMax = GetHeight(iCol);
        //            if (iMax > max)
        //                max = iMax;
        //        }

        //        return max;
        //    }

        //    foreach (var iProp in type.GetProperties())
        //    {
        //        var iType = iProp.PropertyType;
        //        if (iType.IsReferenceType())
        //        {
        //            var iVal = iProp.GetValue(obj);
        //            var iMax = GetHeight(iVal);
        //            if (iMax > max)
        //                max = iMax;
        //        }
        //    }

        //    return max;

        //}
        
    }
}
