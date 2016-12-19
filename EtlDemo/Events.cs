using System.Diagnostics.Tracing;

namespace EtlDemo
{
    [EventSource(Name="EtlDemo")]
    internal sealed class Events : EventSource
    {
        public static readonly Events Write = new Events();

        public class Keywords
        {
            public const EventKeywords General = (EventKeywords)1;
            public const EventKeywords PrimeOutput = (EventKeywords)2;
        }

        internal const int ProcessingStartId = 1;
        internal const int ProcessingFinishId = 2;
        internal const int FoundPrimeId = 3;

        [Event(ProcessingStartId, Level = EventLevel.Informational, Keywords = Keywords.General)]
        public void ProcessingStart()
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(ProcessingStartId);
            }
        }

        [Event(ProcessingFinishId, Level = EventLevel.Informational, Keywords = Keywords.General)]
        public void ProcessingFinish()
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(ProcessingFinishId);
            }
        }

        [Event(FoundPrimeId, Level = EventLevel.Informational, Keywords = Keywords.PrimeOutput)]
        public void FoundPrime(long primeNumber)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(FoundPrimeId, primeNumber);
            }
        }                        
    }
}
