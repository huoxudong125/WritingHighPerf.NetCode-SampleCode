using ReflectionInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExtension
{
    public class AddExtension : IExtension
    {
        public int Execute(int arg1, int arg2)
        {
            return arg1 + arg2;
        }
    }
}
