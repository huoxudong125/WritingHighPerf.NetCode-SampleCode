using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        // You can do other work here while the async task is running
        static Regex regex = new Regex("<title>(.*)</title>", RegexOptions.Compiled);

        static void Main(string[] args)
        {
            GetWebPageTitle(@"Http://www.bing.com").ContinueWith(task=>Console.WriteLine(task.Result));

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task<string> GetWebPageTitle(string url)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            Task<string> task = client.GetStringAsync(url);
                        
            // now we need the result so await
            string contents = await task;

            Match match = regex.Match(contents);
            if (match.Success)
            {
                return match.Groups[1].Captures[0].Value;
            }
            return string.Empty;
        }
    }
}
