using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FunkyCode.ExcSharp.Console;
using FunkyCode.ExcSharp.Engine;
using NUnit.Framework;

namespace FunkyCode.ExcSharp.UnitTests
{
    public class DictionaryTests
    {
        static readonly string WorkBookPath = @$"..\..\..\Excel\{nameof(DictionaryTests)}.xlsx";

        [Test]
        public void Test_DictionaryOwner()
        {
            var sheetName = nameof(Test_DictionaryOwner);

            if (sheetName.Length > 31)
                throw new ArgumentException($"{nameof(sheetName)} length must be less or eq 31");

            var dictionaryOwner = TestDataRepository.CreateSingleDictionaryOwner();
            var dictionaryOwnerList = new List<DictionaryOwner> {dictionaryOwner};
            

            ExcelData.InsertObject(dictionaryOwnerList, WorkBookPath, sheetName);

            var loaded = ExcelData.LoadObject<DictionaryOwner>(WorkBookPath, sheetName);

            dictionaryOwnerList.Should().BeEquivalentTo(loaded);
        }
    }
}
