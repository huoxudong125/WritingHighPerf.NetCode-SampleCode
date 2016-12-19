using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerfCountersTypingSpeed
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer = new Timer();
        private PerformanceCounter cpuPerformanceCounter;
        private PerformanceCounter bytesAllocPerformanceCounter;

        private PerformanceCounter wpmCounter;

        const string CategoryName = "PerfCountersTypingSpeed";

        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
            var process = Process.GetCurrentProcess();

            cpuPerformanceCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            bytesAllocPerformanceCounter = new PerformanceCounter(".NET CLR Memory", "Allocated Bytes/sec", process.ProcessName);
            
            CreateCustomCategories();            
            
            wpmCounter = new PerformanceCounter(CategoryName, "WPM", false);
            wpmCounter.RawValue = 0;
        }

        private void CreateCustomCategories()
        {
            if (PerformanceCounterCategory.Exists(CategoryName))
            {
                return;
            }
            else
            {
                // Create and inform user to exit
                var counterDataCollection = new CounterCreationDataCollection();

                // Add the counter.
                CounterCreationData wpmCounter = new CounterCreationData();
                wpmCounter.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                wpmCounter.CounterName = "WPM";
                counterDataCollection.Add(wpmCounter);

                try
                {
                    // Create the category.
                    PerformanceCounterCategory.Create(CategoryName, "Demo category to show how to create and consume counters",
                        PerformanceCounterCategoryType.SingleInstance, counterDataCollection);
                    MessageBox.Show("Perf Counters created. Please exit and restart the program to consume them.");
                }
                catch (SecurityException )
                {
                    MessageBox.Show("Could not create Perf Counter. Please run the process as admin. Exiting");
                    Environment.Exit(1);
                }                
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.textBoxCpu.Text = string.Format("{0:F2}%", cpuPerformanceCounter.NextValue());
            this.textBoxBytesPerSec.Text = string.Format("{0:F2} bytes/sec", bytesAllocPerformanceCounter.NextValue());

            if (wpmCounter != null)
            {
                this.textBoxWpm.Text = string.Format("{0:F2} wpm", wpmCounter.NextValue() * 60);
            }
            else
            {
                this.textBoxWpm.Text = "N/A";
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.timer.Dispose();
            base.OnClosing(e);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                this.wpmCounter.Increment();
            }
        }

        
    }
}
