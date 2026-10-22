using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace felmero
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Every Trace.WriteLine(...) in the program goes into trace.txt (next to the exe).
            // The listener appends, so older log lines are kept between runs.
            Trace.Listeners.Add(new TextWriterTraceListener("trace.txt"));
            // Write each line to the file immediately -> the file is complete even after closing the app
            Trace.AutoFlush = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
