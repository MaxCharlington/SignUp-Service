using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.IO;
using System.Data.OleDb;
using Client;
using ToolLibrary;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web;
using System.Runtime.Serialization.Json;
using ClassLibrary;
using System.Data.SqlClient;
using Database;

namespace TestClient
{
    class TestProgram
    {
        class Program
        {
            static void Main()
            {
            //    foreach (var item in DatabaseClass.Execute("SELECT [Id], [Login] FROM [Users] WHERE [Login] = 'a'"))
            //    {
            //        Console.WriteLine(item);
            //    }
            //    DatabaseClass.ExecuteNonQuery("UPDATE [Users] SET [Login] = 'maxcharlington' WHERE [ID] = 0");
                Console.ReadKey();
            }
        }
    }
}