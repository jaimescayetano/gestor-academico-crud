using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DataLayer
{
    public class Connection
    {
        private static Connection _instance;
        private string hostname = "Data Source=DESKTOP-HN5MUAI\\SQLEXPRESS;Initial Catalog=colegio; Integrated Security= True";
        private SqlConnection connection = new SqlConnection();

        // singleton pattern
        public static Connection getInstance()
        {
            if (_instance == null)
            {
                _instance = new Connection();
            }
            return _instance;
        }

        private Connection()
        {
            // make connection
            connection.ConnectionString = hostname;
            connection.Open();
        }

        public void closeConnection()
        {
            // close connection
            this.connection.Close();
        }

        // example of select with administrators table
        public List<List<string>> getAdministrators()
        {
            List<List<string>> admins = new List<List<string>>();
            SqlCommand query = new SqlCommand("SELECT * FROM administradores", this.connection);
            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {
                admins.Add(new List<string>() { data["id"].ToString(), data["usuario"].ToString(), data["gmail"].ToString(), data["contraseña"].ToString() });
            }
            data.Close();
            return admins;
        }

        // validate user password
        public void validateUser() { }
    }
}