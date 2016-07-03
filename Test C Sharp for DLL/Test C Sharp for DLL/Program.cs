using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Test_C_Sharp_for_DLL
{
    class Program
    {
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisplayHelloFromDLL();
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Add(double a, double b);

        static void Main()
        {
            Console.WriteLine("This is C# program");
            DisplayHelloFromDLL();
            Console.WriteLine("1 + 1 = {0}", Add(1,1));
            Console.ReadKey();
        }
    }
}
