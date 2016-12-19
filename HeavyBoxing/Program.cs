using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyBoxing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> collection = new List<object>();
            while (true)
            {
                if (collection.Count > 100000)
                {
                    collection.Clear();
                }
                collection.Add(13);
            }
        }
    }
}
