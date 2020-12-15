using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Bogus;
using FunkyCode.ExcSharp.Engine;

namespace FunkyCode.ExcSharp.UnitTests
{
    public class FakerExt<T> where T : class
    {
        readonly Random _randomizer = new Random(DateTime.UtcNow.Millisecond);

        private int _min = 1;
        private int _max = 1;

        public List<T> Generate(int min, int max)
        {
            _min = min;
            _max = max;

            var rnd = _randomizer.Next(_min, _max + 1);

            var list = new List<T>();
            for (int i = 0; i < rnd; i++)
            {
                var t = Generate();
                list.Add(t);
            }

            return list;

        }

        public T Generate()
        {
            var faker = new Faker<T>()
                .RuleForType(typeof(bool), t => t.Random.Bool())
                .RuleForType(typeof(byte), t => t.Random.Byte())
                .RuleForType(typeof(sbyte), t => t.Random.SByte())
                .RuleForType(typeof(char), t => t.Random.Char())
                .RuleForType(typeof(decimal), t => t.Random.Decimal())
                .RuleForType(typeof(double), t => t.Random.Double())
                .RuleForType(typeof(float), t => t.Random.Float())
                .RuleForType(typeof(int), t => t.Random.Int())
                .RuleForType(typeof(uint), t => t.Random.UInt())
                .RuleForType(typeof(long), t => t.Random.Long())
                .RuleForType(typeof(ulong), t => t.Random.ULong())
                .RuleForType(typeof(short), t => t.Random.Short())
                .RuleForType(typeof(ushort), t => t.Random.UShort())
                .RuleForType(typeof(string), t => t.Random.Word());

            var item = faker.Generate();


            foreach (var propInfo in item.GetType().GetProperties())
            {
                var isToSkip = propInfo.PropertyType.IsPrimitive || propInfo.PropertyType == typeof(string) || propInfo.PropertyType == typeof(decimal);
                if (isToSkip) continue;


                Type argType;
                object[] args;

                if (propInfo.PropertyType.IsCollection())
                {
                    var genericArgs = propInfo.PropertyType.GetGenericArguments();
                    argType = genericArgs[0];

                    
                    args = new object[] { _min, _max };
                }
                else
                {

                    argType = propInfo.PropertyType;
                    args = new object[] { };
                }

                if (argType.Name.Contains("Decimal"))
                {

                }

                var result = GetFakerResult(argType, args);
                propInfo.SetValue(item, result);
            }
            
            return item;
        }

        private object GetFakerResult(Type fakerGenericType, object[] methodArgs)
        {
            var fakerType = typeof(FakerExt<>);

            var typeArgs = new[] { fakerGenericType };
            var fakerConstructed = fakerType.MakeGenericType(typeArgs);

            var iFaker = Activator.CreateInstance(fakerConstructed);
            var createMethod = iFaker.GetType().GetMethods().FirstOrDefault(m =>
                m.Name == nameof(Generate) && m.GetParameters().Length == methodArgs.Length);

            var result = createMethod.Invoke(iFaker, methodArgs);

            return result;
        }



    }
}
