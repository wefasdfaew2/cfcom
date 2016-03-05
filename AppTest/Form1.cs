using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace AppTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var i = TimeDataflowComputations(10, 100000);
        }

        static TimeSpan TimeDataflowComputations(int maxDegreeOfParallelism,
   int messageCount)
        {
            // Create an ActionBlock<int> that performs some work. 
            var workerBlock = new ActionBlock<int>(
                // Simulate work by suspending the current thread.
               millisecondsTimeout => Thread.Sleep(millisecondsTimeout),
                // Specify a maximum degree of parallelism. 
               new ExecutionDataflowBlockOptions
               {
                   MaxDegreeOfParallelism = maxDegreeOfParallelism
               });

            // Compute the time that it takes for several messages to  
            // flow through the dataflow block.

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < messageCount; i++)
            {
                workerBlock.Post(1000);
            }
            workerBlock.Complete();

            // Wait for all messages to propagate through the network.
            workerBlock.Completion.Wait();

            // Stop the timer and return the elapsed number of milliseconds.
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
