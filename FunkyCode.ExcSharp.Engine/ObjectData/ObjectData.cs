using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using FunkyCode.ExcSharp.Engine.Core;
using Microsoft.Win32.SafeHandles;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class ObjectData : Tree<ObjectData>
    {

        public enum DataTypeEnum
        {
            Unknown,
            Primitive,
            Class,
            Collection,
            Dictionary
        }


        public ObjectData(ObjectData parent)
        {
            Parent = parent;
        }

        #region virtuals

        public virtual void Create(string name, Type type)
        {

        }

        public virtual object GetRecordFromTable(ExcelLoadData tableData, Type type, int iColumn, int currentRow, int maxRow, string parentName = "")
        {
            return null;
        }

        public virtual void GetDataAsTableNew(List<object[]> table, object obj, int? maxsize, int parentPosition,
            int currentHeight, CreateTableResult result)
        {

        }

        public virtual int Width => Children.Sum(c => c.Width);

        public virtual int GetHeight(object obj) => 1;

        #endregion


        public DataTypeEnum DataType { get; set; }
        public string Name { get; set; }

        public Type Type { get; set; }

        public List<string> Names { get; set; } = new List<string>();

        public string FullName
        {
            get
            {
                if (null == Parent || null == Parent.Name) return Name;
                return $"{Parent.FullName}.{Name}";
            }
        }
     
        public int Number
        {
            get
            {
                if (null == Parent) return 1;
                return (Parent.Children.IndexOf(this)) + 1;
            }
        }

        public int Position
        {
            get
            {
                if (null == Parent) return 1;
                if (Number > 1)
                {
                    var predecessor = Parent.Children.FirstOrDefault(c => c.Number == Number - 1);
                    return predecessor.Position + predecessor.Width;
                }
                else
                {
                    return Parent.Position;
                }


            }
        }
   

        public override string ToString() => $"{FullName ?? "<root>"} : {DataType}";

    }
}
