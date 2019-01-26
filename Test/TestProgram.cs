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
using Npgsql;

namespace PgSql
{
    public class PostGreSQL
    {
        public static void Main() {
            string str = "{ \"Login\":\"\",\"PasswordHash\":\"lkhjgfhdg\"}";
            UserClass user = new UserClass();
            user.InitializeWithJSON(str);
            Console.WriteLine(user.ToJSON());
            Console.ReadKey();
        }
    }
}