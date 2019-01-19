using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ToolLibrary;

namespace Threads
{
    /// <summary>
    /// Singleton class that handles user Actions according to the CPU usage
    /// </summary>
    public class ThreadManager
    {
        private static ThreadManager instance = null;
        private ThreadManager()
        {
            Task.Run(() => CpuUsageUpdate());
        }
        public static ThreadManager GetInstance()
        {
            if (instance == null)
                instance = new ThreadManager();
            return instance;
        }

        private static float CPUUsage;
        private static bool CPUReady = true;
        private static void CpuUsageUpdate()
        {
            while (true)
            {
                CPUUsage = Hardware.CPUUsage();
                if (CPUReady && CPUUsage >= 95) CPUReady = false;
                else if (!CPUReady && CPUUsage < 95)
                {
                    CPUReady = true;
                    RetryRunning();
                }
            }
        }

        private static Queue<Action> postponedActions = new Queue<Action>();
        private static async Task RetryRunning()
        {
            await Task.Run(() =>
            {
                while (CPUReady && postponedActions.Count > 0)
                {
                    GetInstance().RunAction(postponedActions.Dequeue());
                }
            });
        }

        public async Task RunAction(Action action)
        {
            if (CPUReady)
            {
                await Task.Run(action);
            }
            else
            {
                postponedActions.Enqueue(action);
            }
        }
    }
}