using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pinning
{
    class Program
    {
        static void Main(string[] args)
        {
            // FileSystemWatcher implicitly pins memory internally.
            var tempPath = Path.GetTempPath();
            using (var fsw = new FileSystemWatcher(tempPath, "*.*"))
            {
                fsw.NotifyFilter = NotifyFilters.LastWrite;
                
                fsw.Created += (sender, e) => Console.WriteLine("Created: " + e.Name);
                fsw.Changed += (sender, e) => Console.WriteLine("Changed: " + e.Name);
                fsw.Deleted += (sender, e) => Console.WriteLine("Deleted: " + e.Name);

                fsw.EnableRaisingEvents = true;

                Task.Run(() =>
                    {
                        var tempFile = Path.GetTempFileName();
                        try
                        {
                            while (true)
                            {
                                File.WriteAllText(tempFile, DateTime.Now.ToString());
                                Thread.Sleep(100);
                            }
                        }
                        finally
                        {
                            File.Delete(tempFile);
                        }
                    });
                Console.WriteLine("Press Any key to exit");
                Console.ReadKey();
            }
            
        }
    }
}
