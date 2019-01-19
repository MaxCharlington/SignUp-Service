using System;
using System.Collections.Generic;
using System.Data.OleDb;

using ToolLibrary;

namespace Database
{
    public static class DatabaseClass
    {
        private static OleDbConnection database = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb;");
        
        private static List<object> Execute(string command) 
        {
            List<object> rez = new List<object>();
            try
            {
                database.Open();
                OleDbCommand commandDB = new OleDbCommand(command, database);
                OleDbDataReader reader = commandDB.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rez.Add(reader.GetValue(i));

                    }
                }
            }
            finally
            {
                database.Close();
            }
            return rez;
        }

        private static void ExecuteNonQuery(string command) {
            database.Open();
            OleDbCommand commandDB = new OleDbCommand(command, database);
            try {
                commandDB.ExecuteNonQuery();
            }
            finally {
                database.Close();
            }
        }
    }
}