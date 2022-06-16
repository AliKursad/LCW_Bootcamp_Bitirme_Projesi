using System;
using System.Collections.Generic;
using System.Text;

namespace LCW.Core.Results
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }

    }
}
