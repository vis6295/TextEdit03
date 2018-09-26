using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "abcdefghijklmnop";
            //string a = "abcd";
            //string a = null;
            Console.WriteLine(a);

            int leftChar = 0;
            int w = 10; //ширина

            //int lenA = a.Length;

            string bb = "";
            int aLen = (a == null) ? 0 : a.Length;
            if (aLen > leftChar) bb = a.Substring(leftChar, ((w < aLen - leftChar) ? w : aLen - leftChar));
            int ext = w - bb.Length;
            if (ext > 0) bb += new string('.', ext);

            Console.WriteLine(bb);
            Console.WriteLine("1234567890");

            //AttributeTargets

            Console.ReadKey();
        }

    }
}
