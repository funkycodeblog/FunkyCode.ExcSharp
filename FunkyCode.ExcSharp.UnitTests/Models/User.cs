using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.Console
{
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        
        public List<Work> Works { get; set; }
      
        public Address Address { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class UserWithDictionaries
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Dictionary<int, string> Accounts { get; set; }
        public Dictionary<int, Skill> Skills { get; set; }
    }

    


    public class Address
    {
        public string Street { get; set;}
        public int ZipCode { get; set; }
    }

    public class Skill
    {
        public string Name { get; set; }
        public Grade Grade { get; set; }
    }

    public class Grade
    {
        public int Value { get; set; }
        public string Unit { get; set; }
    }

    public class Work
    {
        public string Company { get; set; }
        public double Salary { get; set; }

    }
}
