using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            const int MaxValue = 1000000000;

            long sum = 0;
            
            // Naive For-loop
            watch.Restart();
            sum = 0;
            Parallel.For(0, MaxValue, (i) =>
            {
                Interlocked.Add(ref sum, (long)Math.Sqrt(i));
            });
            watch.Stop();
            Console.WriteLine("Parallel.For: {0}", watch.Elapsed);

            // Partitioned For-loop
            var partitioner = Partitioner.Create(0, MaxValue);
            watch.Restart();
            sum = 0;
            Parallel.ForEach(partitioner, 
                (range) =>
                {
                    long partialSum = 0;
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        partialSum += (long)Math.Sqrt(i);
                    }
                    Interlocked.Add(ref sum, partialSum);
                });
            watch.Stop();
            Console.WriteLine("Partitioned Parallel.For: {0}", watch.Elapsed);            
        }        
    }
}
