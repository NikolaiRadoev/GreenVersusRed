using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVersusRed
{
    public interface ICell
    {
        int Row { get; }
        int Col { get; }
    }
}
