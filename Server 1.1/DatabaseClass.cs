using System;
using System.Collections.Generic;
using Npgsql;

using ToolLibrary;

namespace Database
{
    public class DatabaseClass
    {
        private static string connstring = "Server=192.168.0.77; Port=5432; User Id=postgres; Password=maxcharlington;";
        private static DatabaseClass instance;
        private static NpgsqlConnection connection;

        private DatabaseClass() { }        

        public static DatabaseClass GetInstance()
        {
            if (instance == null)
                instance = new DatabaseClass();
            return instance;
        }

        public void OpenConnection() {
            connection = new NpgsqlConnection(connstring);
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        private static List<object> Execute(string commandString) 
        {
            List<object> rez = new List<object>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(commandString, connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                    {
                        rez.Add(dataReader.GetValue(i));
                    }
                }
            }
            catch (Exception e)
            {
                ToolClass.Print(e.Message, ConsoleColor.Red);
            }
            return rez;
        }

        private static void ExecuteNonQuery(string commandString) {
            NpgsqlCommand command = new NpgsqlCommand(commandString, connection);
            command.ExecuteNonQueryAsync();
        }
    }
}