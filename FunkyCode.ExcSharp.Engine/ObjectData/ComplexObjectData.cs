using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using FunkyCode.ExcSharp.Engine.Core;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class ComplexObjectData : ObjectData
    {
        public ComplexObjectData(ObjectData parent) : base(parent)
        {
            DataType = DataTypeEnum.Class;
        }

        public override void Create(string name, Type type)
        {

            Name = name;
            Type = type;

            var props = type.GetProperties();

            foreach (var iProp in props)
            {
                var iType = iProp.PropertyType;
                var iObjectData = ObjectDataFactory.CreateObjectData(iType, this);
                iObjectData.Create(iProp.Name, iProp.PropertyType);
                Children.Add(iObjectData);
            }
        }

        public override void GetDataAsTableNew(List<object[]> table, object obj, int? maxsize, int parentPosition, int currentHeight,
            CreateTableResult result)
        {
            var iPosition = parentPosition;

            if (table.Count < currentHeight)
                table.Add(new object[maxsize.Value]);
            
            foreach (var iPropInfo in obj.GetType().GetProperties())
            {
                var iChild = Children.FirstOrDefault(c => c.Name == iPropInfo.Name);
                if (null != iChild)
                {
                    var iObj = iPropInfo.GetValue(obj);
                    iChild.GetDataAsTableNew(table, iObj, maxsize, iPosition, currentHeight, result);
                    iPosition += iChild.Width;
                }
            }
        }

        public override int GetHeight(object obj)
        {
            var maxHeight = 0;
            foreach (var iPropInfo in obj.GetType().GetProperties())
            {
                var iChild = Children.FirstOrDefault(c => c.Name == iPropInfo.Name);
                var iValue = iPropInfo.GetValue(obj);
                var iHeight = iChild.GetHeight(iValue);
                if (iHeight > maxHeight)
                    maxHeight = iHeight;
            }

            return maxHeight;
        }

        public override object GetRecordFromTable(ExcelLoadData data, Type type, int iColumn, int currentRow, int maxRow, string parentName = "")
        {

            var instance = Activator.CreateInstance(type);

            var propInfos = type.GetProperties();
     
        
            foreach (var iPropInfo in propInfos)
            {
                var iType = iPropInfo.PropertyType;
                var child = Children.First(f => f.Name == iPropInfo.Name);

                var iHeader = data.Headers.First(h => h.FullName == child.FullName);

                var iObj = child.GetRecordFromTable(data, iType, iHeader.Column, currentRow, maxRow, parentName);

                iPropInfo.SetValue(instance, iObj);
               
            }

            return instance;
        }
    }
}
