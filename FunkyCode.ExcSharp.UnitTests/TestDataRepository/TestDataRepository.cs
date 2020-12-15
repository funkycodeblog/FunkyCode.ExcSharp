using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Bogus;
using FunkyCode.ExcSharp.UnitTests;

namespace FunkyCode.ExcSharp.Console
{
    public static class TestDataRepository
    {
        public static User CreateUser(int userNumber, int numberOfSkills, int numberOfWorks)
        {
            var user = new User
            {
                Name = $"User {userNumber}",
                LastName = "Szczudlo",
                Address = new Address
                {
                    Street = "Mieczykowa",
                    ZipCode = 30389
                },
                Skills = new List<Skill>(),
                Works = new List<Work>()
                
            };

            for (var i = 0; i < numberOfSkills; i++)
            {
                var skill = new Skill
                {
                    Name = $"SkillName{i}",
                    Grade = new Grade
                    {
                        Value = (i + 1) * 5,
                        Unit = $"Unit{i + 1}"
                    }
                };

                user.Skills.Add(skill);
            }

            for (var i = 0; i < numberOfWorks; i++)
            {
                var work = new Work
                {
                    Company = $"Company{i}",
                    Salary = i * 1000
                };

                user.Works.Add(work);
            }

            return user;
        }
        
        public static User GetExpectedUser()
        {
            var user = new User
            {
                Name = "User 2",
                LastName = "Szczudlo",
                Works = new List<Work>
                {
                    new Work
                    {
                        Company = "Company0",
                        Salary = 500d
                    },
                    new Work
                    {
                        Company = "Company1",
                        Salary = 1000d
                    },
                    new Work
                    {
                        Company = "Company2",
                        Salary = 2000d
                    }
                },
                Address = new Address
                {
                    Street = "Mieczykowa",
                    ZipCode = 30389
                },
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "SkillName0",
                        Grade = new Grade
                        {
                            Value = 5,
                            Unit = "Unit1"
                        }
                    },
                    new Skill
                    {
                        Name = "SkillName1",
                        Grade = new Grade
                        {
                            Value = 10,
                            Unit = "Unit2"
                        }
                    },
                    new Skill
                    {
                        Name = "SkillName2",
                        Grade = new Grade
                        {
                            Value = 15,
                            Unit = "Unit3"
                        }
                    },
                    new Skill
                    {
                        Name = "SkillName3",
                        Grade = new Grade
                        {
                            Value = 20,
                            Unit = "Unit4"
                        }
                    }
                }
            };

            return user;
        }

        public static DictionaryOwner CreateSingleDictionaryOwner()
        {
            var owner = new DictionaryOwner
            {
                Name = "Mr Dictionary Owner",
                Dictionary = new Dictionary<int, string>
                {
                    {1, "One"},
                    {2, "Two"},
                    {3, "Three"}
                }
            };

            return owner;
        }

        public static List<T> CreateByExtFaker<T>(int min, int max) where T : class
        {
            var faker = new FakerExt<T>();
            var item = faker.Generate(min, max);
            return item;

        }

        public static T Create<T>() where T : class
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
            
            return item;
        }

        public static List<T> Create<T>(int count) where T : class
        {
            var faker = new Faker<T>().RuleForType(typeof(bool), t => t.Random.Bool())
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
                .RuleForType(typeof(string), t => t.Random.String(10, 50));

            var list = faker.Generate(count);

            return list;
        }
    }
}
