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
            //string a = "abcdefghijklmnop";
            string a = "abcd";
            //string a = null;
            Console.WriteLine(a);

            int fromPos = 5;

            int lenA = a.Length;

            int w = 10; //ширина
            int pos = fromPos; //откуда копируем
            int len = ((lenA - pos)>0) ? lenA - pos:0;
            int fil = (w>len) ? w - len:0;

            string b = string.Concat((len>0)?a.Substring(pos, len):"", (fil>0)?new string('.', fil):"");

            Console.WriteLine(b);
            Console.WriteLine("1234567890");

            //AttributeTargets

            Console.ReadKey();
        }

    }
}
