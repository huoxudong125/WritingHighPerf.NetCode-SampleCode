using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task task = Task.Run(() =>
            {
                while (true)
                {
                    // do some work...
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation requested");
                        return;
                    }
                    Thread.Sleep(100);
                }
            }, token);
            
            Console.WriteLine("Press any key to exit");

            Console.ReadKey();

            tokenSource.Cancel();
            
            task.Wait();

            Console.WriteLine("Task completed");
        }       
    }
}
