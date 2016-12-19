using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEachVsFor
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[100];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            int sum = 0;
            foreach (int val in arr)
            {
                sum += val;
            }

            sum = 0;
            IEnumerable<int> arrEnum = arr;
            foreach (int val in arrEnum)
            {
                sum += val;
            }

            Console.WriteLine(sum);
        }
    }
}
