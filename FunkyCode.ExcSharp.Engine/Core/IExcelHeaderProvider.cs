using FunkyCode.ExcSharp.Engine.Tools;

namespace FunkyCode.ExcSharp.Engine.Core
{
    public interface IExcelHeaderProvider
    {
        ObjectData GetHeaders(string[,] headerTable);
    }
}
