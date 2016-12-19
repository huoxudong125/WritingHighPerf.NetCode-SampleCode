using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FxCopViolator
{
    public class Violator
    {
        private Thread myThread;

        static string myStaticData = "This isn't allowed";
        static readonly string myReadOnlyData = "This is allowed";
        const string myConstData = "This is also allowed";

        public string DoAnOperation(string input)
        {
            return input.ToUpper();
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public void StartThread()
        {
            this.myThread = new Thread(() => { while (true);});
        }
    }
}
