﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LCW.Core.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }

    }
}
