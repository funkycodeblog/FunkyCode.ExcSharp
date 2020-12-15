using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunkyCode.ExcSharp.Engine.Tools
{
	public class Tree<T> where T : Tree<T>
    {

        #region <props>
        public List<T> Children { get; private set; }
        public T Parent { get; set; }
       
        #endregion

        #region <ctor>


        public Tree()
        {
            Children = new List<T>();
        }
        
        #endregion

        #region <pub>
        public int Level
        {
            get
            {
                if (null == Parent) return 0;
                return Parent.Level + 1;

            }

        }

        public int MaxLevel
        {
            get
            {
                if (!HasChildren) return Level;
               
                return Children.Max(c => c.MaxLevel);
            }
        }

        public bool HasChildren => Children.Count > 0;

        public bool IsRoot => Parent == null;

        #endregion




    }
}
