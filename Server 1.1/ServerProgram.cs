using System;
using System.Threading.Tasks;

using ToolLibrary;

namespace Server
{
    public class ServerInstance
    {
        private static Server serverInstance = Server.GetInstance();

        public static bool running = true;
        public static Task ReqestHandler;
        public static Task UI;

        private static void Main()
        {
            Console.Title = "Server - SignUp Service";
            serverInstance.StartServer();
            ReqestHandler = HandleServerRequests();
            UI = HandleConsoleCommands();
            UI.Wait();
        }
        
        private static async Task HandleServerRequests()
        {
            while (running)
            {
                try
                {
                    serverInstance.RespondRequestAsync(await serverInstance.GetRequestAsync());
                }
                catch
                {
                    continue;
                }
            }
        }
        
        private static async Task HandleConsoleCommands()
        {
            await Task.Run(() =>
            {
                ToolClass.Print("Server is open for requests", ConsoleColor.Green);
                while (running)
                {
                    string internalCommand = Console.ReadLine();
                    switch (internalCommand)
                    {
                        case "stop":
                            serverInstance.StopServer();
                            running = false;
                            break;
                        case "cpu":
                            ToolClass.Print($"Current CPU usage is {Hardware.CPUUsage()}%\nCount of server threads is {Hardware.ThreadCount()}", ConsoleColor.Yellow);
                            break;
                        case "req":
                            ToolClass.Print($"{serverInstance.SessionRequestCount} requests are handled in the session", ConsoleColor.Yellow);
                            break;
                        default:
                            break;
                    }
                }
                ReqestHandler.Wait();
                ToolClass.Print("Server closed for HTTP requests", ConsoleColor.Red);
                Console.ReadKey();
            });
        }
    }
}