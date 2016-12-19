using ReflectionInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExe
{
    class Program
    {
        const int Iterations = 10000;

        static void Main(string[] args)
        {
            string extensionFile = @"ReflectionExtension";
            var assembly = Assembly.Load(extensionFile);

            var types = assembly.GetTypes();
            Type extensionType = null;
            foreach (var type in types)
            {
                var interfaceType = type.GetInterface("IExtension");
                if (interfaceType != null)
                {
                    extensionType = type;
                    break;
                }
            }

            object extensionObject = null;
            if (extensionType != null)
            {
                extensionObject = Activator.CreateInstance(extensionType);
            }
            MethodInfo executeMethod = extensionType.GetMethod("Execute");
            IExtension extensionViaInterface = extensionObject as IExtension;

            // Pre-execute to account for JIT
            executeMethod.Invoke(extensionObject, new object[] { 1, 2 });
            extensionViaInterface.Execute(1, 2);
            
            Stopwatch watch = Stopwatch.StartNew();
            int actualResult = 0;

            // Execute via MethodInfo.Invoke
            for (int i = 0; i < Iterations; i++)
            {
                object result = executeMethod.Invoke(extensionObject, new object[] { 1, 2 });
                actualResult = (int)result;
            }
            watch.Stop();
            Console.WriteLine("{0}, {1} ticks", actualResult, watch.ElapsedTicks);

            // Execute via interface
            watch.Restart();
            for (int i = 0; i < Iterations; i++)
            {
                actualResult = extensionViaInterface.Execute(1, 2);
            }
            watch.Stop();
            Console.WriteLine("{0}, {1} ticks", actualResult, watch.ElapsedTicks);
        }
    }
}
