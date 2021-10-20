using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;

namespace wsProducto
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public DataSet getData()
        {

            string connectionString = "server=localhost;uid=root;pwd=;database=prueba;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM producto", connection);
            DataSet dataSet = new DataSet("producto");
            mySqlDataAdapter.FillSchema(dataSet, SchemaType.Source, "producto");
            mySqlDataAdapter.Fill(dataSet, "producto");
            connection.Close();
            return dataSet;
        }

        [WebMethod]
        public bool store(string name, float price)
        {
            bool response = true;
            string connectionString = "server=localhost;uid=root;pwd=;database=prueba;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "INSERT INTO Producto(Nombre, Precio) VALUES(@name, @price)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(new MySqlParameter("@name", name));
            command.Parameters.Add(new MySqlParameter("@price", price));
            if (command.ExecuteNonQuery() > 0)
            {
                response = true;
                connection.Close();
            }
            else response = false;

            return response;
        }
        [WebMethod]
        public bool update(int id, string name, float price)
        {
            bool response = true;
            string connectionString = "server=localhost;uid=root;pwd=;database=prueba;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE Producto SET Nombre=@name, Precio=@price WHERE id_producto=@id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@id", id));
            command.Parameters.Add(new MySqlParameter("@name", name));
            command.Parameters.Add(new MySqlParameter("@price", price));
            if (command.ExecuteNonQuery() > 0)
            {
                connection.Close();
                response = true;
            }
            else response = false;


            return response;
        }
        [WebMethod]
        public bool delete(int id)
        {
            bool response = true;
            string connectionString = "server=localhost;uid=root;pwd=;database=prueba;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "DELETE FROM Producto WHERE id_producto=@id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(new MySqlParameter("@id", id));
            if (command.ExecuteNonQuery() > 0)
            {
                connection.Close();
                response = true;
            }
            else response = false;


            return response;

        }
    }
}
