using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GCNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ArrSize = 1024;
            List<byte[]> arrays = new List<byte[]>();

            GC.RegisterForFullGCNotification(25, 25);

            // Start a separate thread to wait for GC notifications
            Task.Run(()=>WaitForGCThread(null));

            Console.WriteLine("Press any key to exit");
            while (!Console.KeyAvailable)
            {
                try
                {
                    arrays.Add(new byte[ArrSize]);
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("OutOfMemoryException!");
                    arrays.Clear();
                }
            }

            GC.CancelFullGCNotification();
        }

        private static void WaitForGCThread(object arg)
        {
            const int MaxWaitMs = 10000;
            while (true)
            {
                // There is also an overload of WaitForFullGCApproach that waits indefinitely
                GCNotificationStatus status = GC.WaitForFullGCApproach(MaxWaitMs);
                bool didCollect = false;
                switch (status)
                {
                    case GCNotificationStatus.Succeeded:
                        Console.WriteLine("GC approaching!");
                        Console.WriteLine("-- redirect processing to another machine -- ");
                        didCollect = true;
                        GC.Collect();
                        break;
                    case GCNotificationStatus.Canceled:
                        Console.WriteLine("GC Notification was canceled");
                        break;
                    case GCNotificationStatus.Timeout:
                        Console.WriteLine("GC notification timed out");
                        break;
                }

                if (didCollect)
                {
                    do
                    {
                        status = GC.WaitForFullGCComplete(MaxWaitMs);
                        switch (status)
                        {
                            case GCNotificationStatus.Succeeded:
                                Console.WriteLine("GC completed");
                                Console.WriteLine("-- accept processing on this machine again --");
                                break;
                            case GCNotificationStatus.Canceled:
                                Console.WriteLine("GC Notification was canceled");
                                break;
                            case GCNotificationStatus.Timeout:
                                Console.WriteLine("GC completion notification timed out");
                                break;
                        }
                        // Looping isn't necessary, but it's useful if you want
                        // to check other state before waiting again.
                    } while (status == GCNotificationStatus.Timeout);
                }
            }
        }
    }
}
