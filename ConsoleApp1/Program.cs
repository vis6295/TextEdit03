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
            string a = "abcdef";
            //string a = null;
            Console.WriteLine(a);

            int fromPos = 2;
            int w = 10;

            string b="";
            if (a != null && a.Length > fromPos) b = a.Substring(fromPos);

            int len = b.Length;
            if (len > w) b = b.Substring(0, w);
            else {
                int ext = w - len;
                if (ext > 0) b += new string('*', ext);
            }
            Console.WriteLine(b);
            Console.WriteLine("1234567890");

            //AttributeTargets

            Console.ReadKey();
        }

    }
}
