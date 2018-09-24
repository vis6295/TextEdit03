using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEdit.Lib
{
    struct string2
    {
        public string str;
        public styleRec[] strStyle;
    }

    struct styleRec
    {
        public int style;
        public int len;
    }

    delegate void lineChange(int lineNumber);

    interface ITextData2 {
        string GetLine(int lineNumber);
        int Count();
        event lineChange onLineChange;
    }
}
