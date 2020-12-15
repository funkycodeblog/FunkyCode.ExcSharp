using System;
using System.Collections.Generic;
using FluentAssertions;
using FunkyCode.ExcSharp.Console;
using FunkyCode.ExcSharp.Engine;
using FunkyCode.ExcSharp.UnitTests.Models;
using FunkyCode.ExcSharp.UnitTests.Types;
using NUnit.Framework;

namespace FunkyCode.ExcSharp.UnitTests
{
    public class SimpleStructuresTests
    {
        static readonly string WorkBookPath = @$"..\..\..\Excel\{nameof(SimpleStructuresTests)}.xlsx";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_User()
        {
            CreateInsertLoadAndCompare<User>(nameof(Test_User));
        }

        [Test]
        public void Test_CollectionLevel01()
        {
            CreateInsertLoadAndCompare<CollectionLevel01>(nameof(Test_CollectionLevel01));
        }


        [Test]
        public void Test_ClassLevel05()
        {
            CreateInsertLoadAndCompare<ClassLevel05>(nameof(Test_ClassLevel05));
        }
        
        [Test]
        public void Test_ClassWithFigure()
        {
            CreateInsertLoadAndCompare<ClassWithFigure> (nameof(Test_ClassWithFigure));
        }
        
        [Test]
        public void Test_ClassWithPrimitives()
        {
            CreateInsertLoadAndCompare<ClassWithPrimitives>(nameof(Test_ClassWithPrimitives));
        }

        private void CreateInsertLoadAndCompare<T>(string sheetName) where T : class
        {
            if (sheetName.Length > 31) 
                throw new ArgumentException($"{nameof(sheetName)} length must be less or eq 31");

            var collection = TestDataRepository.CreateByExtFaker<T>(2,3);

            ExcelData.InsertObject(collection, WorkBookPath, sheetName);
            
            var loaded = ExcelData.LoadObject<List<T>>(WorkBookPath, sheetName);

            collection.Should().BeEquivalentTo(loaded);
        }



        
    }
}
