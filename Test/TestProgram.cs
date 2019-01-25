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
        public static void PostgreSQLtest()
        {
            try
            {
                string connstring = "Server=192.168.0.77; Port=5432; User Id=postgres; Password=maxcharlington;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                Console.WriteLine("Connected");
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Main() {
            PostgreSQLtest();
            Console.ReadKey();
        }

        /*public List<string> PostgreSQLtest2()
        {
            try
            {
                string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=pgdlg123; Database=my_geo_database;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM bike_route.points WHERE point_names > '005' AND point_names < '010'", connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                for (int i = 0; dataReader.Read(); i++)
                {
                    dataItems.Add(dataReader[0].ToString() + "," + dataReader[1].ToString() + "," + dataReader[2].ToString() + "\r\n");
                }
                connection.Close();
                return dataItems;
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }*/
    }
}