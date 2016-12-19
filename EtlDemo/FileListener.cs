using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtlDemo
{
    class FileListener : BaseListener
    {
        private StreamWriter writer;

        public FileListener(IEnumerable<SourceConfig> sources, string outputFile)
            :base(sources)
        {
            writer = new StreamWriter(outputFile);
        }

        protected override void WriteEvent(System.Diagnostics.Tracing.EventWrittenEventArgs eventData)
        {
            StringBuilder output = new StringBuilder();
            DateTime time = DateTime.Now;
            output.AppendFormat("{0:yyyy-MM-dd-HH:mm:ss.fff} - {1} - ", time, eventData.Level);
            switch (eventData.EventId)
            {
                case Events.ProcessingStartId:
                    output.Append("ProcessingStart");
                    break;
                case Events.ProcessingFinishId:
                    output.Append("ProcessingFinish");
                    break;
                case Events.FoundPrimeId:
                    output.AppendFormat("FoundPrime - {0:N0}", eventData.Payload[0]);
                    break;
                default:
                    throw new InvalidOperationException("Unknown event");
            }
            this.writer.WriteLine(output.ToString());
        }

        public override void Dispose()
        {
            this.writer.Close();

            base.Dispose();
        }
    }
}
