using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public interface IExcelFactory
    {
     

        IExcelSheet GetSheet(string path, string name);
      

    }
}
