using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitAsync
{
    class Program
    {
        const int Size = 256;
        static int[] array = new int[Size];
        static int length = 0;
        static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        static void Main(string[] args)
        {
            //var writerTask = Task.Run((Action)WriterFunc);
            //var readerTask = Task.Run((Action)ReaderFunc);

            var writerTask = Task.Run((Action)WriterFuncAsync);
            var readerTask = Task.Run((Action)ReaderFuncAsync);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void WriterFunc()
        {
            while (true)
            {
                semaphore.Wait();
                Console.WriteLine("Writer: Obtain");
                for (int i = length; i < array.Length; i++)
                {
                    array[i] = i * 2;
                }
                Console.WriteLine("Writer: Release");
                semaphore.Release();
            }
        }

        static void ReaderFunc()
        {
            while (true)
            {
                semaphore.Wait();
                Console.WriteLine("Reader: Obtain");
                for (int i = length; i >= 0; i--)
                {
                    array[i] = 0;
                }
                length = 0;
                Console.WriteLine("Reader: Release");
                semaphore.Release();
            }
        }

        static void WriterFuncAsync()
        {            
            semaphore.WaitAsync().ContinueWith(_ =>
            {
                Console.WriteLine("Writer: Obtain");
                for (int i = length; i < array.Length; i++)
                {
                    array[i] = i * 2;
                }
                Console.WriteLine("Writer: Release");
                semaphore.Release();
            }).ContinueWith(_=>WriterFuncAsync());            
        }

        static void ReaderFuncAsync()
        {            
            semaphore.WaitAsync().ContinueWith(_ =>
            {
                Console.WriteLine("Reader: Obtain");
                for (int i = length; i >= 0; i--)
                {
                    array[i] = 0;
                }
                length = 0;
                Console.WriteLine("Reader: Release");
                semaphore.Release();
            }).ContinueWith(_=>ReaderFuncAsync());            
        }
    }
}
