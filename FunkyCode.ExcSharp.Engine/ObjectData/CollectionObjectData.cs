using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FunkyCode.ExcSharp.Engine.Core;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class CollectionObjectData : ObjectData
    {
        public CollectionObjectData(ObjectData parent) : base(parent)
        {
            DataType = DataTypeEnum.Collection;
        }

        public override void Create(string name, Type type)
        {

            var genericArgumentType = type.GetGenericArguments().First();

            Name = name;
            Type = type;

            var iObjectData = ObjectDataFactory.CreateObjectData(genericArgumentType, this);
            iObjectData.Create(genericArgumentType.Name, genericArgumentType);
            Children.Add(iObjectData);
        }

        public override void GetDataAsTableNew(List<object[]> table, object obj, int? maxsize, int parentPosition,
            int currentHeight,
            CreateTableResult result)
        {
           
            int? size = maxsize ?? Width;
            var collection = (IList) obj;
            var argType = obj.GetType().GetGenericArguments().First();
            var childItem = Children.First(c => c.Name == argType.Name);

            var iCurrentHeight = currentHeight;
            var itemHeight = GetHeight(obj);
            if (table.Count < itemHeight)
            {
                var itemsToAdd = itemHeight - table.Count;
                for (var i = 0; i < itemsToAdd; i++)
                    table.Add(new object[maxsize.Value]);
            }

            for (var index = 0; index < collection.Count; index++)
            {
                var sourceItem = collection[index];
                childItem.GetDataAsTableNew(table, sourceItem, size, parentPosition, iCurrentHeight, result);
                iCurrentHeight += childItem.GetHeight(sourceItem);
            }

        }

        public override int GetHeight(object obj)
        {
            var collection = (IList)obj;
            var argType = obj.GetType().GetGenericArguments().First();
            var childItem = Children.First(c => c.Name == argType.Name);
            var height = 0;
            foreach (var iObj in collection)
            {
                var iHeight = childItem.GetHeight(iObj);
                height += iHeight;
            }

            return height;
        }

        public override object GetRecordFromTable(ExcelLoadData data, Type type, int iColumn, int currentRow, int maxRow,
            string parentName = "")
        {
            var instance = Activator.CreateInstance(type);

            var child = Children.First();



            var header = data.Headers.FirstOrDefault(h => h.FullName == child.FullName);


            var addMethod = type.GetMethod("Add");
            var argumentType = type.GetGenericArguments().FirstOrDefault();

            var propsInfos = argumentType.GetProperties().ToList();

            var firstPrimitive = propsInfos.FirstOrDefault(p => p.PropertyType.IsPrimitive || p.PropertyType == typeof(string));
            var primitiveChild = child.Children.First(f => f.Name == firstPrimitive.Name);
            var primitiveHeader = data.Headers.First(h => h.FullName == primitiveChild.FullName);
            var primitiveColumn = primitiveHeader.Column;

            var lastRow = false;
            for (var r = currentRow; r <= maxRow; )
            {
                var iMaxRow = ArraysExtensions.GetFirstNonNullable(data.Data, primitiveColumn, r + 1);
                if (iMaxRow == -1)
                {
                    lastRow = true;
                    iMaxRow = maxRow + 1;
                }
                var iObj = child.GetRecordFromTable(data, argumentType, iColumn, r, iMaxRow-1, parentName);
                var methodParameters = new object[] {iObj};
                addMethod.Invoke(instance, methodParameters);
                r = iMaxRow;

                if (lastRow)
                    break;
            }

            return instance;
        }

        static int GetLastCollectionRow(object[,] table, int startRow, int startColumn, int endColumn)
        {
            var rows = table.RowCount();

            for (var r = rows - 1; r >= startRow; r--)
            {
                for (var c = startColumn; c <= endColumn; c++)
                {
                    var value = table[r, c];
                    if (null != value) return r + 1;
                }
            }

            return startRow;
        }
    }
}
