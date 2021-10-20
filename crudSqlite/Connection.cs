using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace crudSqlite
{
    class Connection
    {
        private static ConnectionInstance instance = null;
        
        //singleton
        public static SQLiteConnection objDB()
        {
            if(instance == null)
            {
                instance = new ConnectionInstance();
            }
            return instance.connection;
        }
        public static DataTable query(string sql)
        {
            DataTable dataTable = new DataTable();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sql, objDB());
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
    class ConnectionInstance
    {
        public SQLiteConnection connection = null;
        public ConnectionInstance()
        {
            string connectionString = "Data Source=prueba.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }
         ~ConnectionInstance()
        {
            connection.Close();
        }
    }
        
}
