using System;
using System.Diagnostics;

namespace ToolLibrary
{
    public class Timer
    {
        private static Stopwatch swatch = new Stopwatch();

        public static void Start()
        {
            swatch.Start();
        }

        public static double Time(bool print = false)
        {
            var time = swatch.Elapsed;
            if (print) ToolClass.Print("CURRENT TIME" + time, ConsoleColor.Yellow);
            return time.TotalMilliseconds;
        }

        public static void Stop()
        {
            swatch.Reset();
        }

        public static void Restart()
        {
            swatch.Restart();
        }
    }
}
