using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunkyCode.ExcSharp.Engine.Tools;

namespace FunkyCode.ExcSharp.UnitTests
{
    public static class CSharpToExcelDataConverter
    {

        public static CreateTableResult FillTable(List<object[]> table, ObjectData collection, object obj)
        {
            var creationResult = new CreateTableResult();
            collection.GetDataAsTableNew(table, obj, collection.Width, 1, 1, creationResult);
            creationResult.TotalHeight = collection.GetHeight(obj);
            return creationResult;
        }

        //public static CreateTableResult FillTable(List<object[]> table, ObjectData collection, object obj)
        //{

        //    var collectionItem = new CollectionObjectData(null);
        //    collectionItem.Children.Add(collection);
        //    collection.Parent = collectionItem;

        //    var creationResult = new CreateTableResult();

        //    collectionItem.GetDataAsTableNew(table, obj, collection.Width, 1, 1, creationResult);

        //    creationResult.TotalHeight += creationResult.Heights.Sum();
        //    return creationResult;

        //}
    }
}
