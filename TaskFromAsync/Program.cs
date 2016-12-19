using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFromAsync
{
    class Program
    {
        const int TotalLength = 1024;
        const int ReadSize = TotalLength / 4;

        static void Main(string[] args)
        {
            string tempFile = Path.GetTempFileName();
            
            string contents = new string('a', TotalLength);
            Console.WriteLine("Writing to file");
            File.WriteAllText(tempFile, contents);

            Console.WriteLine("Reading from file");
            var results = GetStringFromFileBetter(tempFile).Result;
            Console.WriteLine("Length: {0}", results.Length);
            Console.WriteLine(results);

            Console.ReadLine();
        }

        static Task<string> GetStringFromFile(string path)
        {
            byte[] buffer = new byte[TotalLength];

            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, buffer.Length, FileOptions.DeleteOnClose | FileOptions.Asynchronous);

            //It's <int> because stream.EndRead returns an int, indicating the number of bytes read.
            Task<int> task = Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, 0, buffer.Length, null);

            return task.ContinueWith(readTask =>
            {
                stream.Close();
                int bytesRead = readTask.Result;
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            });
        }

        static Task<string> GetStringFromFileBetter(string path)
        {
            byte[] buffer = new byte[TotalLength];

            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, buffer.Length, FileOptions.DeleteOnClose | FileOptions.Asynchronous);

            //It's <int> because stream.EndRead returns an int, indicating the number of bytes read.
            Task<int> task = Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, 0, ReadSize, null);

            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            task.ContinueWith(readTask => OnReadBuffer(readTask, stream, buffer, 0, tcs));

            return tcs.Task;
        }

        static void OnReadBuffer(Task<int> readTask, Stream stream, byte[] buffer, int offset, TaskCompletionSource<string> tcs)
        {
            int bytesRead = readTask.Result;
            if (bytesRead > 0)
            {
                Task<int> task = Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, offset + bytesRead, Math.Min(buffer.Length - (offset + bytesRead), ReadSize), null);

                task.ContinueWith(callbackTask => OnReadBuffer(callbackTask, stream, buffer, offset + bytesRead, tcs));
            }
            else
            {
                tcs.TrySetResult(Encoding.UTF8.GetString(buffer, 0, offset));
            }
        }
    }
}
