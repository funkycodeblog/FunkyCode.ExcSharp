using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.ExcSharp.Console;
using FunkyCode.ExcSharp.Engine;
using FunkyCode.ExcSharp.Engine.Tools;
using NUnit.Framework;

namespace FunkyCode.ExcSharp.UnitTests
{
    public class SingleItemTests
    {
        static readonly string WorkBookPath = @$"..\..\..\Excel\{nameof(SingleItemTests)}.xlsx";

        [Test]
        public void Test()
        {
            var sheetName = nameof(Test);

            var user = TestDataRepository.GetExpectedUser();

            var objectData = DataPrototypeFactory.CreatePrototypeNew<User>();

            ExcelData.InsertObject(user, WorkBookPath, sheetName);

            var loaded = ExcelData.LoadObject<User>(WorkBookPath, sheetName);
        }

    }
}
