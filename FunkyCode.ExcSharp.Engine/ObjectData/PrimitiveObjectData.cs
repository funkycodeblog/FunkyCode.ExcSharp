using System;
using System.Collections.Generic;
using FunkyCode.ExcSharp.Engine.Core;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class PrimitiveObjectData : ObjectData
    {
        public PrimitiveObjectData(ObjectData parent) : base(parent)
        {
            DataType = DataTypeEnum.Primitive;
        }

        public override void Create(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public override int Width => 1;

        public override void GetDataAsTableNew(List<object[]> table, object obj, int? maxsize, int parentPosition, int currentHeight,
            CreateTableResult result)
        {
            table[currentHeight-1][parentPosition-1] = obj;
        }

        public override object GetRecordFromTable(ExcelLoadData data, Type type, int iColumn, int currentRow, int maxRow, string parentName = "")
        {
            var value = data.Data[currentRow-1, iColumn-1];

            var converted = ConverterFactory.GetValue(type, value);

            return converted;
        }

        public override int GetHeight(object obj) => 1;
    }

    
}
