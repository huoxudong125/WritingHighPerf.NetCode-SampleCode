using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static Stopwatch watch = new Stopwatch();
        static int pendingTasks;

        static void Main(string[] args)
        {            
            const int MaxValue = 1000000000;
            
            // Sequential for comparison
            watch.Start();
            long sum = 0;
            for (int i = 0; i <= MaxValue; i++)
            {
                sum += (long)Math.Sqrt(i);
            }
            watch.Stop();

            Console.WriteLine("Sequential: {0}", watch.Elapsed);

            watch.Restart();
            int numTasks = Environment.ProcessorCount;
            pendingTasks = numTasks;
            int perThreadCount = MaxValue / numTasks;
            int perThreadLeftover = MaxValue % numTasks;

            Task<long>[] tasks = new Task<long>[numTasks];

            for (int i = 0; i < numTasks; i++)
            {                
                int start = i * perThreadCount;
                int end = (i+1) * perThreadCount;
                if (i == numTasks - 1)
                {
                    end += perThreadLeftover;
                }
                tasks[i] = Task<long>.Run(() =>
                {
                    long threadSum = 0;
                    for (int j = start; j <= end; j++)
                    {
                        threadSum += (long)Math.Sqrt(j);
                    }
                    return threadSum;
                });
                tasks[i].ContinueWith(OnTaskEnd);
            }
            
            // You shouldn't normally wait on tasks, but in this case we want to wait 
            // until the previous test is complete
            Task.WaitAll(tasks);

            watch.Restart();
            sum = 0;
            Parallel.For(0, MaxValue, (i) =>
                {
                    Interlocked.Add(ref sum, (long)Math.Sqrt(i));
                });
            watch.Stop();
            Console.WriteLine("Parallel.For: {0}", watch.Elapsed);
        }

        private static void OnTaskEnd(Task<long> task)
        {
            Console.WriteLine("Thread sum: {0}", task.Result);
            if (Interlocked.Decrement(ref pendingTasks) == 0)
            {
                watch.Stop();
                Console.WriteLine("Tasks: {0}", watch.Elapsed);
            }            
        }
    }
}
