using System;
using System.Threading.Tasks;

namespace ToolLibrary
{
    public class ToolClass
    {
        //Prints with color
        private static object MessageLock = new object();
        public static async Task Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            await Task.Run(() =>
            {
                lock (MessageLock)
                {
                    if (consoleColor == Console.ForegroundColor)
                    {
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.WriteLine(message);
                        Console.ResetColor();
                    }
                }
            });
        }
    }
}