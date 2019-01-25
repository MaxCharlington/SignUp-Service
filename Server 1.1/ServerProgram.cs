using ToolLibrary;

using System;
using System.Threading.Tasks;

namespace Server
{
    class ServerInstance
    {
        private static bool finished = false;
        
        private static void Main()
        {
            Console.Title = "Server - SignUp Service";
            Server.StartServer();
            HandleServerRequests();
            AdminCommandHandler();
        }
        
        private static async Task HandleServerRequests()
        {
            ToolClass.Print("Server is open for requests", ConsoleColor.Green);
            while (!finished)
            {
                try
                {
                    Server.RespondRequest(await Server.GetRequest());
                }
                catch
                {
                    continue;
                }
            }
            ToolClass.Print("Server closed for HTTP requests", ConsoleColor.Red);
        }
        
        private static void AdminCommandHandler()
        {            
            while (!finished)
            {
                string internalCommand = Console.ReadLine();
                switch (internalCommand)
                {
                    case "stop":
                        finished = true;
                        Server.StopServer();
                        break;
                    case "cpu":
                        ToolClass.Print($"Current CPU usage is {Hardware.CPUUsage()}%\nCount of server threads is {Hardware.ThreadCount()}", ConsoleColor.Yellow);
                        break;
                    case "req":
                        ToolClass.Print($"{Server.sessionRequestCount} requests are handled in the session", ConsoleColor.Yellow);
                        break;
                    default:
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}