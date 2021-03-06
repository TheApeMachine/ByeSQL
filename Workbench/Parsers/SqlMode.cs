using System;
using System.Collections.Generic;
using System.Text;

namespace Workbench.Parsers
{
    [Flags]
    public enum SqlMode : int
    {
        NoMode = 0,
        AnsiQuotes = 1 << 0,
        HighNotPrecedence = 1 << 1,
        PipesAsConcat = 1 << 2,
        IgnoreSpace = 1 << 3,
        NoBackslashEscapes = 1 << 4
    }
}
