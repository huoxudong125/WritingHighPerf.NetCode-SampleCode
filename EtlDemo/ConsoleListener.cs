using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtlDemo
{
    class ConsoleListener : BaseListener
    {
        public ConsoleListener(IEnumerable<SourceConfig> sources)
            :base(sources)
        {
        }

        protected override void WriteEvent(System.Diagnostics.Tracing.EventWrittenEventArgs eventData)
        {
            string outputString;
            switch (eventData.EventId)
            {
                case Events.ProcessingStartId:
                    outputString = string.Format("ProcessingStart ({0})", eventData.EventId);
                    break;
                case Events.ProcessingFinishId:
                    outputString = string.Format("ProcessingFinish ({0})", eventData.EventId);
                    break;
                case Events.FoundPrimeId:
                    outputString = string.Format("FoundPrime ({0}): {1}", eventData.EventId, (long)eventData.Payload[0]);
                    break;
                default:
                    throw new InvalidOperationException("Unknown event");
            }
            Console.WriteLine(outputString);
        }
    }
}
