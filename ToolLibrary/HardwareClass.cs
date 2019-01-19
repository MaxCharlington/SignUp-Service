using System.Diagnostics;
using System.Threading;

namespace ToolLibrary
{
    public class Hardware
    {
        public static float CPUUsage()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            Thread.Sleep(50);
            return cpuCounter.NextValue();
        }

        public static float RAMRemains()
        {
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            ramCounter.NextValue();
            Thread.Sleep(100);
            return ramCounter.NextValue();
        }

        public static int ThreadCount()
        {
            return Process.GetCurrentProcess().Threads.Count;
        }
    }
}