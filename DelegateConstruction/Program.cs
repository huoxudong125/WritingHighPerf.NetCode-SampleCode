using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateConstruction
{
    class Program
    {
        private delegate int MathOp(int x, int y);

        private static int Add(int x, int y) { return x + y; }
        private static int DoOperation(MathOp op, int x, int y) { return op(x, y); }

        static void Main(string[] args)
        {            
            // Examine the IL for these two loops

            //Option 1: Bad
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(DoOperation(Add, 1, 2));
            }

            //Options 2: Good
            MathOp op = Add;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(DoOperation(op, 1, 2));
            }            
        }
    }
}
