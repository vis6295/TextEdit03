using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditLib
{
    public class Class1
    {
        public override string ToString()
        {
            Console.WriteLine("Class1");
            return "Class1";
        }
    }

    public class c1 : IEnumerator
    {
        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class c2 : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }



    public class TextBuffer {
    }
}
