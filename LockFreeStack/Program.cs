using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockFreeStack
{
    class Program
    {
        const int NumItems = 10000000;

        static void Main(string[] args)
        {
            LockFreeStack<int> stack = new LockFreeStack<int>();

            Parallel.For(0, NumItems,
                (i) => stack.Push(i));
            Console.WriteLine("Count: {0}", stack.Count);

            Parallel.For(0, NumItems,
                (i) => stack.Pop());
            Console.WriteLine("Count: {0}", stack.Count);
        }
    }
}
