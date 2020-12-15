using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.ExcSharp.UnitTests.Models
{
    public class ClassLevelBase
    {
        public string Description { get; set; }
        public int Number { get; set; }
    }

    public class ClassLevel01 : ClassLevelBase
    {
        
    }

    public class ClassLevel02 : ClassLevelBase
    {
        public ClassLevel01 Child { get; set; }
    }

    public class ClassLevel03 : ClassLevelBase
    {
        public ClassLevel02 Child { get; set; }
    }

    public class ClassLevel04 : ClassLevelBase
    {
        public ClassLevel03 Child { get; set; }
    }

    public class ClassLevel05 : ClassLevelBase
    {
        public ClassLevel04 Child { get; set; }
    }
}
