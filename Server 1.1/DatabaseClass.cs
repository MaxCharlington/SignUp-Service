using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private static async Task<List<object>> ExecuteAsync(string commandString) 
        {
            List<object> rez = new List<object>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(commandString, connection);
                NpgsqlDataReader dataReader = (NpgsqlDataReader) await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
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

        private static async Task ExecuteNonQuery(string commandString) {
            NpgsqlCommand command = new NpgsqlCommand(commandString, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}