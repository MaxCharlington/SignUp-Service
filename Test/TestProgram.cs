﻿using System;
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
using Server;

namespace PgSql
{
    public class PostGreSQL
    {
        public static void Main() {
            var database = DatabaseClass.GetInstance();
            database.OpenConnection();
            database.CloseConnection();
            Console.ReadKey();
        }
    }
}