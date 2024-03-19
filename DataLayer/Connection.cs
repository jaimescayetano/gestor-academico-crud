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
        private string hostname = "Data Source=DESKTOP-KKA5IBN\\SQLEXPRESS;Initial Catalog=colegio; User Id=sa; Password=1234";
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
        public bool ValidateUser(string gmail, string contraseña)
        {
            SqlCommand query = new SqlCommand("SELECT COUNT(*) FROM administradores WHERE gmail = @Email AND contraseña = @Password", this.connection);
            query.Parameters.AddWithValue("@Email", gmail);
            query.Parameters.AddWithValue("@Password", contraseña);

            int count = (int)query.ExecuteScalar();

            // Si count es mayor que 0, significa que se encontró una coincidencia
            return count > 0;
        }
    }
}