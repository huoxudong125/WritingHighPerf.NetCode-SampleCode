using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllocateAndRelease
{
    class MyObject
    {
        int x;
        int y;
        int z;
        int t;
        string label;

        public MyObject()
        {
            x = 0;
            y = 1;
            z = 2;
            t = 3;
            label = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2},{3}): {4}", x, y, z, t, label);
        }

    }
}
