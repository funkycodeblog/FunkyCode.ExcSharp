using System;
using System.Collections;
using System.Collections.Generic;
using FunkyCode.ExcSharp.Engine.Core;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class DictionaryObjectData : ObjectData
    {
        public DictionaryObjectData(ObjectData parent) : base(parent)
        {
            DataType = DataTypeEnum.Dictionary;
        }

        public override void Create(string name, Type type)
        {
            Name = name;

            var args = type.GetGenericArguments();
            var keyType = args[0];

            var keyObjectData = ObjectDataFactory.CreateObjectData(keyType, this);
            keyObjectData.Create("Key", keyType);

            var valueType = args[1];
            var valueObjectData = ObjectDataFactory.CreateObjectData(valueType, this);
            valueObjectData.Create("Value", valueType);

            Children.Add(keyObjectData);
            Children.Add(valueObjectData);
        }

        public override void GetDataAsTableNew(List<object[]> table, object obj, int? maxsize, int parentPosition, int currentHeight,
            CreateTableResult result)
        {
            int? size = maxsize ?? Width;

            var collection = (IDictionary)obj;

            var keyObjectData = Children[0];
            var valueObjectData = Children[1];

            var iCurrentHeight = currentHeight;
            foreach (var keyObj in collection.Keys)
            {
                var valueObj = collection[keyObj];

                if (table.Count < iCurrentHeight)
                    table.Add(new object[maxsize.Value]);

                keyObjectData.GetDataAsTableNew(table, keyObj, size, parentPosition, iCurrentHeight, result);
                valueObjectData.GetDataAsTableNew(table, valueObj, size, parentPosition + keyObjectData.Width, iCurrentHeight, result);

                iCurrentHeight++;
            }

        }

        public override object GetRecordFromTable(ExcelLoadData data, Type type, int iColumn, int currentRow, int maxRow,
            string parentName = "")
        {
            var instance = Activator.CreateInstance(type);
            var iDictionary = (IDictionary) instance;

            var keyObjectData = Children[0];
            var valueObjectData = Children[1];

            for (var r = currentRow; r <= currentRow + 3; r++)
            {
                var keyObj = keyObjectData.GetRecordFromTable(data, keyObjectData.Type, iColumn, r, -1, parentName);
                var valueObj = valueObjectData.GetRecordFromTable(data, valueObjectData.Type, iColumn + keyObjectData.Width, r, -1, parentName);

                iDictionary.Add(keyObj, valueObj);
            }

            return instance;
        }
    }
}
