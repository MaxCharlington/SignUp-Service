using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

using ToolLibrary;
using ClassLibrary;

namespace Database
{
    public class DatabaseClass
    {
        private static string connstring = "Server=192.168.0.77; Port=5432; User Id=ServerSignUp; Password=postgresqlDB; Database=SignUp;";
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

        public async Task<List<object>> ExecuteAsync(string commandString) 
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

        public async Task ExecuteNonQueryAsync(string commandString) {
            NpgsqlCommand command = new NpgsqlCommand(commandString, connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task CreateSession(string sessionId, int userId = -1) {
            await ExecuteNonQueryAsync($"INSERT INTO Sessions (SessionId, UserId) VALUES ('{sessionId}', '{userId}')");
        }

        public async Task CreateSession(SessionClass session)
        {
            await ExecuteNonQueryAsync($"INSERT INTO Sessions (SessionId, UserId) VALUES ('{session.SessionId}', '{session.UserId}')");
        }
    }
}