using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastingPerf
{
    class Program
    {
        const int NumIterations = 1000000;

        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();           

            // Ensure everything is jitted before we record times
            new A().GetValue();
            new B().GetValue();
            new C().GetValue();
            new D().GetValue();
            new NotAnA();
            TimeTest("JIT (ignore)", () => { }, 0);

            A aNoCast = new A();
            long baseline = TimeTest("No cast", () => { A aNo = (A)aNoCast; aNo.GetValue(); }, 0);
                                
            // UP CASTING
            B b = new B();
            TimeTest("Up cast (1 gen)", () => { A a = (A)b; a.GetValue(); }, baseline);
                        
            C c = new C();
            TimeTest("Up cast (2 gens)", () => { A a = (A)c; a.GetValue(); }, baseline);
            
            D d = new D();
            TimeTest("Up cast (3 gens)", () => { A a = (A)d; a.GetValue(); }, baseline);

            // DOWN CASTING
            A ab = new B();
            TimeTest("Down cast (1 gen)", () => { B down = (B)ab; down.GetValue(); }, baseline);

            A ac = new C();
            TimeTest("Down cast (2 gens)", () => { C down = (C)ac; down.GetValue(); }, baseline);
            
            A da = new D();
            TimeTest("Down cast (3 gens)", () => { D down = (D)da; down.GetValue(); }, baseline);
                        
            // INTERFACE
            A aid = new D();
            TimeTest("Interface", () => { I id = (I)aid; id.GetValue(); }, baseline);
            
            // BAD CAST

            object notA = new NotAnA();
            TimeTest("Invalid Cast", () => { try { A oops = (A)notA; oops.GetValue(); } catch (InvalidCastException) { } }, baseline);

            TimeTest("as (success)", () => { D down = da as D; d.GetValue(); }, baseline);
            TimeTest("as (failure)", () => { D oops = notA as D; if (oops != null) oops.GetValue(); }, baseline);
            TimeTest("is (success)", () => { bool success = da is D; success.GetHashCode(); }, baseline);
            TimeTest("is (failure)", () => { bool success = da is D; success.GetHashCode(); }, baseline);
        }

        static long TimeTest(string title, Action action, long baseline)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < NumIterations; i++)
            {
                action();
            }
            watch.Stop();
            var ticks = watch.ElapsedTicks;
            Console.WriteLine("{0}: {1:F2}x", title, (double)ticks / ((baseline == 0) ? ticks : baseline));
            return ticks;
        }

        class A
        {
            public char GetValue() { return 'A'; }
        }
        interface I { char GetValue(); }
        class B : A { }
        class C : B { }
        class D : C, I { }

        class NotAnA { }
    }
}
