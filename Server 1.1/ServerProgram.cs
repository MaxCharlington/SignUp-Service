using System;
using System.Threading.Tasks;

using ToolLibrary;

namespace Server
{
    class ServerInstance
    {
        private static bool finished = false;
        private static Server server = (Server)Server.GetInstance();
        
        private static void Main()
        {
            Console.Title = "Server - SignUp Service";
            server.StartServer();
            HandleServerRequests();
            AdminCommandHandler();
        }
        
        private static async Task HandleServerRequests()
        {
            while (!finished)
            {
                try
                {
                    server.RespondRequestAsync(await server.GetRequestAsync());
                }
                catch
                {
                    continue;
                }
            }
        }
        
        private static void AdminCommandHandler()
        {
            ToolClass.Print("Server is open for requests", ConsoleColor.Green);
            while (!finished)
            {
                string internalCommand = Console.ReadLine();
                switch (internalCommand)
                {
                    case "stop":
                        finished = true;
                        server.StopServer();
                        break;
                    case "cpu":
                        ToolClass.Print($"Current CPU usage is {Hardware.CPUUsage()}%\nCount of server threads is {Hardware.ThreadCount()}", ConsoleColor.Yellow);
                        break;
                    case "req":
                        ToolClass.Print($"{server.SessionRequestCount} requests are handled in the session", ConsoleColor.Yellow);
                        break;
                    default:
                        break;
                }
            }
            ToolClass.Print("Server closed for HTTP requests", ConsoleColor.Red);
            Console.ReadKey();
        }
    }
}