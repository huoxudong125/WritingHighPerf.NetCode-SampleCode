using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionCost
{
    class Program
    {
        const int NumIterations = 1000;
        static void Main(string[] args)
        {
            // first get them to JIT themselves
            EmptyMethod();
            try
            {
                ExceptionMethod(1);
            }
            catch (InvalidOperationException) { }

            Stopwatch watch = new Stopwatch();

            watch.Restart();
            for (int i = 0; i < NumIterations; i++)
            {
                EmptyMethod();
            }
            watch.Stop();
            long baselineTime = watch.ElapsedTicks;
            Console.WriteLine("Empty Method: 1x");

            for (int depth = 1; depth <= 10; depth++)
            {
                watch.Restart();
                for (int i = 0; i < NumIterations; i++)
                {
                    try
                    {
                        ExceptionMethod(depth);
                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
                watch.Stop();
                Console.WriteLine("Exception (depth = {0}): {1:F1}x", depth, (double)watch.ElapsedTicks / baselineTime);
            }

            for (int depth = 1; depth <= 10; depth++)
            {
                watch.Restart();
                for (int i = 0; i < NumIterations; i++)
                {
                    try
                    {
                        ExceptionMethod(depth);
                    }
                    catch (ArgumentNullException)
                    {
                    }
                    catch (ArgumentException)
                    {

                    }
                    catch (InvalidOperationException)
                    {
                    }
                    catch (Exception)
                    {
                    }

                }
                watch.Stop();
                Console.WriteLine("Exception (catchlist, depth = {0}): {1:F1}x", depth, (double)watch.ElapsedTicks / baselineTime);
            }   

            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void EmptyMethod()
        {
            
        }

        static void ExceptionMethod(int depth)
        {
            if (depth > 1)
            {
                ExceptionMethod(depth - 1);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
