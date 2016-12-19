using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtlDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleListener = new ConsoleListener(
                new SourceConfig[] 
                {
                    new SourceConfig(){
                        Name = "EtlDemo", 
                        Level = EventLevel.Informational, 
                        Keywords = Events.Keywords.General}                    
                });

            var fileListener = new FileListener(
                new SourceConfig[]
                {
                    new SourceConfig(){
                        Name = "EtlDemo", 
                        Level = EventLevel.Verbose, 
                        Keywords = Events.Keywords.PrimeOutput}
                },
                "PrimeOutput.txt");

            long start = 1000000;
            long end = 10000000;
            
            Events.Write.ProcessingStart();
            for (long i = start; i < end; i++)
            {
                if (IsPrime(i))
                {
                    Events.Write.FoundPrime(i);
                }
            }
            
            Events.Write.ProcessingFinish();
            consoleListener.Dispose();
            fileListener.Dispose();
        }

        private static bool IsPrime(long number)
        {
            if (number % 2 == 0)
            {
                if (number ==2)
                {
                    return true;
                }
                return false;
            } 
            long sqrt = (long)Math.Sqrt(number);
            for (int i = 3; i <= sqrt; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
