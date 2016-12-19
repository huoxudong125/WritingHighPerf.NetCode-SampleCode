using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadRand
{
    class Program
    {
        static Random rand = new Random();
        
        [ThreadStatic]
        static Random safeRand;

        static void Main(string[] args)
        {
            int[] results = new int[100];
                                    
            Parallel.For(0, 5000,
                i =>
                {
                    // thread statics are not initialized
                    if (safeRand == null) safeRand = new Random();
                    var randomNumber = safeRand.Next(100);
                    Interlocked.Increment(ref results[randomNumber]);
                });
            
            PrintHistogram(results);
        }

        private static void PrintHistogram(int[] results)
        {
            for (int i = 0; i < results.Length / 10; i++)
            {
                int sum = 0;
                for (int j = i * 10; j < ((i + 1) * 10); j++)
                {
                    sum += results[j];
                }
                Console.Write("{0:D2}-{1:D3}: ",i*10,(i+1)*10);
                for (int j = 0; j < sum / 10; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }
        }
    }
}
