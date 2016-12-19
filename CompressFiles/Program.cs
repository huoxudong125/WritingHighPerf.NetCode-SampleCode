using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressFiles
{
    class Program
    {
        static void Main(string[] args)
        {            
            if (args.Length < 2 || (args[0] != "sync" && args[0] != "async"))
            {
                Console.WriteLine("Usage: CompressFiles [sync|async] file1 file2 file3 ...");
                File.WriteAllText("testfile.txt", new string('a', 100000000));
                return;
            }

            bool async = args[0] == "async";

            var fileList = args.Skip(1);
            
            if (async)
            {
                AsyncCompress(fileList).Wait();
            }
            else
            {
                SyncCompress(fileList);
            }
        }

        private static void SyncCompress(IEnumerable<string> fileList)
        {
            byte[] buffer = new byte[16384];
            foreach (var file in fileList)
            {
                using (var inputStream = File.OpenRead(file))
                using (var outputStream = File.OpenWrite(file+".compressed"))
                using (var compressStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    int read = 0;
                    while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        compressStream.Write(buffer, 0, read);
                    }
                }
            }
        }

        private static async Task AsyncCompress(IEnumerable<string> fileList)
        {
            byte[] buffer = new byte[16384];
            foreach (var file in fileList)
            {
                using (var inputStream = File.OpenRead(file))
                using (var outputStream = File.OpenWrite(file + ".compressed"))
                using (var compressStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    int read = 0;
                    while ((read = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await compressStream.WriteAsync(buffer, 0, read);
                    }
                }
            }
        }
    }
}
