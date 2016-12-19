using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighContention
{
    class Program
    {
        static object collectionLock = new object();
        static List<string> collection = new List<string>();
        const int NumTasks = 5;

        static void Main(string[] args)
        {
            Task[] allTasks = new Task[NumTasks];
            for (int i = 0; i < NumTasks; i++)
            {
                allTasks[i] = Task.Run(() =>
                    {
                        while (true)
                        {
                            lock (collectionLock)
                            {
                                if (collection.Count > 1000000)
                                {
                                    collection.RemoveAt(collection.Count - 1);
                                }
                                else
                                {
                                    collection.Add(DateTime.Now.ToString());
                                }
                            }
                        }
                    });                
            }
            Task.WaitAll(allTasks);
        }
    }
}
