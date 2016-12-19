using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoadExecutor
{
    class Program
    {
        class TimingDummy
        {
            public bool DoWork(string arg) { return true; }
        }

        static void Main(string[] args)
        {
            var argument = Int64.MaxValue.ToString();
            
            // Since we've already taken a dependency on the Extension (as an easy way to get it into the same directory),
            // this line will load the assembly "without context" and .Net will consider it a completely different assembly.
            Assembly assembly = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "DynamicLoadExtension.dll"));

            // Find the type and method we need to execute
            Type type = assembly.GetType("DynamicLoadExtension.Extension");
            MethodInfo methodInfo = type.GetMethod("DoWork");

            // Codegen a delegate to create the Extension instance
            Func<object> creationDel = GenerateNewObjDelegate<Func<object>>(type);
            Func<object, string, bool> doWorkDel = GenerateMethodCallDelegate<Func<object, string, bool>>(methodInfo, type, typeof(bool), new Type[] { typeof(object), typeof(string) });                     
                        
            // Compare execution times
            Console.WriteLine("==CREATE INSTANCE==");
            const int IterationCount = 100000;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < IterationCount; i++)
            {
                object extensionObject = new TimingDummy();
            }
            watch.Stop();
            var elapsedBaseline = watch.ElapsedTicks;
            Console.WriteLine("Direct ctor: 1.0x");

            watch.Start();
            for (int i = 0; i < IterationCount; i++)
            {
                object extensionObject = Activator.CreateInstance(type);
            }
            watch.Stop();
            Console.WriteLine("Activator.CreateInstance: {0:F1}x", (double)watch.ElapsedTicks / elapsedBaseline);

            watch.Restart();
            for (int i = 0; i < IterationCount; i++)
            {
                object extensionObject = creationDel();
            }
            watch.Stop();
            Console.WriteLine("Codegen: {0:F1}x", (double)watch.ElapsedTicks / elapsedBaseline);

            Console.WriteLine();
            Console.WriteLine("==METHOD INVOKE==");

            var extension = new TimingDummy();
            watch.Start();
            for (int i = 0; i < IterationCount; i++)
            {
                bool result = extension.DoWork(argument);
            }
            watch.Stop();
            elapsedBaseline = watch.ElapsedTicks;
            Console.WriteLine("Direct method: 1.0x");

            object instance = Activator.CreateInstance(type);    
            watch.Start();
            for (int i = 0; i < IterationCount; i++)
            {
                bool result = (bool)methodInfo.Invoke(instance, new object[] { argument });
            }
            watch.Stop();
            Console.WriteLine("MethodInfo.Invoke: {0:F1}x", (double)watch.ElapsedTicks / elapsedBaseline);
            
            object extensionObj = creationDel();
            watch.Restart();
            for (int i = 0; i < IterationCount; i++)
            {
                doWorkDel(extensionObj, argument);
            }
            watch.Stop();
            Console.WriteLine("Codegen: {0:F1}x", (double)elapsedBaseline / watch.ElapsedTicks);
        }

        private static T GenerateNewObjDelegate<T>(Type type) where T:class
        {
            // Create a new, parameterless (specified by Type.EmptyTypes) dynamic method.
            var dynamicMethod = new DynamicMethod("Ctor_" + type.FullName, type, Type.EmptyTypes, true);
            var ilGenerator = dynamicMethod.GetILGenerator();

            // Look up the constructor info for the type we want to create
            var ctorInfo = type.GetConstructor(Type.EmptyTypes);
            if (ctorInfo != null)
            {
                ilGenerator.Emit(OpCodes.Newobj, ctorInfo);
                ilGenerator.Emit(OpCodes.Ret);

                object del = dynamicMethod.CreateDelegate(typeof(T));
                return (T)del;
            }
            return null;
        }

        private static T GenerateMethodCallDelegate<T>(MethodInfo methodInfo, Type extensionType, Type returnType, Type[] parameterTypes) where T : class
        {
            var dynamicMethod = new DynamicMethod("Invoke_" + methodInfo.Name, returnType, parameterTypes, true);
            var ilGenerator = dynamicMethod.GetILGenerator();
            
            // object's this parameter
            ilGenerator.Emit(OpCodes.Ldarg_0);
            // cast it to the correct type
            ilGenerator.Emit(OpCodes.Castclass, extensionType);
            // actual method argument            
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.EmitCall(OpCodes.Call, methodInfo, null);
            ilGenerator.Emit(OpCodes.Ret);

            object del = dynamicMethod.CreateDelegate(typeof(T));
            
            return (T)del;
        }
    }
}
