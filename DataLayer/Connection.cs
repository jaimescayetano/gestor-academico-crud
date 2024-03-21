using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLayer
{
    public class Connection
    {
        private static Connection _instance;
        private string hostname = "Data Source=DESKTOP-JOPHT0L\\SQLEXPRESS;Initial Catalog=colegio; Integrated Security= True";
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

        public List<List<string>> getLevels()
        {
            List<List<string>> levels = new List<List<string>>();
            SqlCommand query = new SqlCommand("SELECT n.*, a.numero, a.capacidad FROM niveles n" +
                                               " INNER JOIN aulas a ON n.aula_id = a.id", this.connection);
            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {
                levels.Add(new List<string>() {
                    data["id"].ToString(),
                    data["nivel_academico"].ToString(),
                    data["seccion"].ToString(),
                    data["grado"].ToString(),
                    data["tutor"].ToString(),
                    data["observaciones"].ToString(),
                    data["aula_id"].ToString(),
                    data["numero"].ToString(),
                    data["capacidad"].ToString()});
            }
            data.Close();
            return levels;
        }



        // Ingresar estudiantes
        public void insertStudent(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, int nivelId)
        {
            SqlCommand query = new SqlCommand("INSERT INTO estudiantes (primer_nombre, segundo_nombre, " +
                                              "primer_apellido, segundo_apellido, telefono, celular, " +
                                              "direccion, gmail, fecha_nacimiento, observaciones, nivel_id) " +
                                              "VALUES (@primerNombre, @segundoNombre, @primerApellido, @segundoApellido, " +
                                              "@telefono, @celular, @direccion, @gmail, @fechaNacimiento, @observaciones, @nivelId)", this.connection);

            // Agregar los parámetros al comando para evitar SQL Injection


            query.Parameters.AddWithValue("@primerNombre", primerNombre);
            query.Parameters.AddWithValue("@segundoNombre", segundoNombre);
            query.Parameters.AddWithValue("@primerApellido", primerApellido);
            query.Parameters.AddWithValue("@segundoApellido", segundoApellido);
            query.Parameters.AddWithValue("@telefono", telefono);
            query.Parameters.AddWithValue("@celular", celular);
            query.Parameters.AddWithValue("@direccion", direccion);
            query.Parameters.AddWithValue("@gmail", gmail);
            query.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
            query.Parameters.AddWithValue("@observaciones", observaciones);
            query.Parameters.AddWithValue("@nivelId", nivelId);



            // Ejecutar el comando
            int result = query.ExecuteNonQuery();

            // Cerrar la conexión
            this.connection.Close();

        }


        // Obtener a loes estudiantes

        public List<List<string>> getStudents()
        {
            List<List<string>> levels = new List<List<string>>();
            SqlCommand query = new SqlCommand("SELECT * FROM estudiantes", this.connection);
            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {

                levels.Add(new List<string>() {
                    data["id"].ToString(),
                    data["primer_nombre"].ToString(),
                    data["segundo_nombre"].ToString(),
                    data["primer_apellido"].ToString(),
                    data["segundo_apellido"].ToString(),
                    data["telefono"].ToString(),
                    data["celular"].ToString(),
                    data["direccion"].ToString(),
                    data["gmail"].ToString(),
                    data["fecha_nacimiento"].ToString(),
                    data["observaciones"].ToString(),
                    data["nivel_id"].ToString()});
            }
            data.Close();
            return levels;
        }

    }
}