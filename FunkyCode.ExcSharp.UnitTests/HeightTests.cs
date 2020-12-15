using System;
using System.Linq;
using FluentAssertions;
using FunkyCode.ExcSharp.Console;
using FunkyCode.ExcSharp.Engine;
using FunkyCode.ExcSharp.Engine.Tools;
using FunkyCode.ExcSharp.UnitTests.Models;
using NSubstitute;
using NUnit.Framework;

namespace FunkyCode.ExcSharp.UnitTests
{
    [TestFixture]
    public class HeightTests
    {
        [OneTimeSetUp]
        public void Init()
        {

        }

        [OneTimeTearDown]
        public void CleanUp()
        {

        }

        [Test]
        public void WHEN_something_THEN_something()
        {
            var user = TestDataRepository.GetExpectedUser();

            var objectData = DataPrototypeFactory.CreatePrototypeNew<User>();

            var height = objectData.GetHeight(user);

            height.Should().Be(4);
        }

        [Test]
        public void Test2()
        {
            var sut = TestDataRepository.CreateByExtFaker<CollectionLevel01>(5, 5).FirstOrDefault();

            var objectData = DataPrototypeFactory.CreatePrototypeNew<CollectionLevel01>();

            var height = objectData.GetHeight(sut);

            height.Should().Be(125);
        }
    }
}
