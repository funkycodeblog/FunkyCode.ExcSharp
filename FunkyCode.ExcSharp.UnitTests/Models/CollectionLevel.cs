using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.UnitTests.Models
{
    public class CollectionLevel01
    {
        public string Description { get; set; }
        public int Number { get; set; }
        public double Area { get; set; }
        public List<CollectionLevel02> Collection { get; set; }

    }

    public class CollectionLevel02
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<CollectionLevel03> Collection { get; set; }
        public double Coefficient { get; set; }
       
    }

    public class CollectionLevel03
    {
        public List<CollectionLevel04> Collection { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Amount { get; set; }
    }

    public class CollectionLevel04
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }






}
