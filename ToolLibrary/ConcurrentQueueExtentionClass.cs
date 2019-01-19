using System.Collections.Concurrent;

namespace ToolLibrary
{
    public static class ConcurrentQueueExtention
    {
        public delegate void Manager();
        public static event Manager Addition;

        public static void EnqueueExtention(this ConcurrentQueue<int> queue, int element)
        {
            queue.Enqueue(element);
            Addition();
        }
    }
}
