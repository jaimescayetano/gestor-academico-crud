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
        private string hostname = "Data Source=DESKTOP-KKA5IBN\\SQLEXPRESS;Initial Catalog=colegio;User ID=sa;Password=1234";
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
            SqlCommand query = new SqlCommand("SELECT " +
                "e.id, " +
                "e.primer_nombre, " +
                "e.segundo_nombre, " +
                "e.primer_apellido, " +
                "e.segundo_apellido, " +
                "e.telefono, e.celular, " +
                "e.direccion, " +
                "e.gmail, " +
                "e.fecha_nacimiento, " +
                "e.observaciones, " +
                "CASE n.nivel_academico WHEN 'I' THEN 'Inicial' WHEN 'P' THEN 'Primaria' WHEN 'S' THEN 'Secundaria' END AS nivel_academico," +
                " CONCAT(n.grado,' ', n.seccion) AS grado_seccion FROM estudiantes e JOIN niveles n ON e.nivel_id = n.id " , this.connection);
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
                    data["nivel_academico"].ToString(),
                    data["grado_seccion"].ToString()
            });
        }
            data.Close();
            return levels;
        }
    }
}