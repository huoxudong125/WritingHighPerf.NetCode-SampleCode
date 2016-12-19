using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JitCall
{
    class Program
    {
        static void Main(string[] args)
        {            
            int val = A();
            int val2 = A();
            Console.WriteLine(val + val2);
        }
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        static int A()
        {
            return 42;
        }
    }
}
