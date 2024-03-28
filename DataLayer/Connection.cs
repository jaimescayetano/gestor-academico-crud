﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Collections.Specialized.BitVector32;
using System.Windows.Forms;

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

        public List<Dictionary<string, string>> getLevesOptions()
        {
            List<Dictionary<string, string>> levesOptions = new List<Dictionary<string, string>>();
            SqlCommand query = new SqlCommand(@"SELECT id, 
                                                CONCAT(grado, seccion,   
                                                CASE 
                                                    WHEN nivel_academico = 'P' THEN ' Primaria'
                                                    WHEN nivel_academico = 'I' THEN ' Inicial'
                                                    WHEN nivel_academico = 'S' THEN ' Secundaria'
                                                    ELSE ' No encontrado' 
                                                END) AS nivel_academico
                                               FROM niveles;", this.connection);
            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {

                levesOptions.Add(new Dictionary<string, string>() { { data["id"].ToString(), data["nivel_academico"].ToString() } });
            }
            data.Close();
            return levesOptions;
        }

        // validate user password
        public bool ValidateUser(string gmail, string contraseña)
        {
            SqlCommand query = new SqlCommand("SELECT COUNT(*) FROM administradores WHERE gmail = @Email AND contraseña = @Password", this.connection);
            query.Parameters.AddWithValue("@Email", gmail);
            query.Parameters.AddWithValue("@Password", contraseña);

            int count = (int)query.ExecuteScalar();

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

        // get level by Id
        public List<string> getLevelById(int levelId)
        {
            SqlCommand query = new SqlCommand("SELECT * FROM niveles WHERE id = @levelId", this.connection);

            query.Parameters.AddWithValue("@levelId", levelId);

            SqlDataReader data = query.ExecuteReader();

            if (data.Read())
            {
                List<string> level = new List<string>() {
                    data["id"].ToString(),
                    data["nivel_academico"].ToString(),
                    data["seccion"].ToString(),
                    data["grado"].ToString(),
                    data["tutor"].ToString(),
                    data["observaciones"].ToString(),
                    data["aula_id"].ToString()
        };
                data.Close();
                return level;
            }
            else
            {
                data.Close();
                return null;
            }
        }

        // Ingresar Niveles
        public void insertLevel(char nivelAcademico, string seccion, int grado, string tutor,
                          string observaciones, int aulaId)
        {
            SqlCommand query = new SqlCommand("INSERT INTO niveles (nivel_academico, seccion, " +
                                              "grado, tutor, observaciones, aula_id) " +
                                              "VALUES (@nivelAcademico, @seccion, @grado, @tutor, " +
                                              "@observaciones, @aulaId)", this.connection);

            query.Parameters.AddWithValue("@nivelAcademico", nivelAcademico);
            query.Parameters.AddWithValue("@seccion", seccion);
            query.Parameters.AddWithValue("@grado", grado);
            query.Parameters.AddWithValue("@tutor", tutor);
            query.Parameters.AddWithValue("@observaciones", observaciones);
            query.Parameters.AddWithValue("@aulaId", aulaId);

            // Ejecutar el comando
            int result = query.ExecuteNonQuery();

        }

        // Actualizar Niveles
        public void updateLevel(int id, char nivelAcademico, string seccion, int grado, string tutor,
                        string observaciones, int aulaId)
        {
            SqlCommand query = new SqlCommand("UPDATE niveles SET nivel_academico = @nivelAcademico, " +
                                              "seccion = @seccion, grado = @grado, tutor = @tutor, " +
                                              "observaciones = @observaciones, aula_id = @aulaId " +
                                              "WHERE id = @id", this.connection);

            query.Parameters.AddWithValue("@id", id);
            query.Parameters.AddWithValue("@nivelAcademico", nivelAcademico);
            query.Parameters.AddWithValue("@seccion", seccion);
            query.Parameters.AddWithValue("@grado", grado);
            query.Parameters.AddWithValue("@tutor", tutor);
            query.Parameters.AddWithValue("@observaciones", observaciones);
            query.Parameters.AddWithValue("@aulaId", aulaId);

            int result = query.ExecuteNonQuery();

        }

        // Borrar Nivel
        public void deleteLevel(int id)
        {
            SqlCommand query = new SqlCommand("DELETE FROM niveles WHERE id = @id", this.connection);
            query.Parameters.AddWithValue("@id", id);
            int result = query.ExecuteNonQuery();
        }


        // Ingresar estudiantes
        public void insertStudent(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, string nivelId)
        {
            SqlCommand query = new SqlCommand("INSERT INTO estudiantes (primer_nombre, segundo_nombre, " +
                                              "primer_apellido, segundo_apellido, telefono, celular, " +
                                              "direccion, gmail, fecha_nacimiento, observaciones, nivel_id) " +
                                              "VALUES (@primerNombre, @segundoNombre, @primerApellido, @segundoApellido, " +
                                              "@telefono, @celular, @direccion, @gmail, @fechaNacimiento, @observaciones, @nivelId)", this.connection);

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

            int result = query.ExecuteNonQuery();

        }


        // Obtener a los estudiantes
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

        public List<string> getStudentById(int studentId)
        {
            SqlCommand query = new SqlCommand("SELECT * FROM estudiantes WHERE id = @studentId", this.connection);

            query.Parameters.AddWithValue("@studentId", studentId);

            SqlDataReader data = query.ExecuteReader();

            if (data.Read())
            {
                List<string> level = new List<string>() {
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
                    data["nivel_id"].ToString()
            };
                data.Close();
                return level;
            }
            else
            {
                data.Close();
                return null;
            }
        }

        // Actualizar Estudiante
        public void updateStudent(int id, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, int nivelId)
        {
            SqlCommand query = new SqlCommand("UPDATE estudiantes SET primer_nombre = @primerNombre, " +
                                              "segundo_nombre = @segundoNombre, primer_apellido = @primerApellido, segundo_apellido = @segundoApellido, " +
                                              "telefono = @telefono, celular = @celular, direccion = @direccion, gmail = @gmail, " +
                                              "fecha_nacimiento = @fechaNacimiento, observaciones = @observaciones, nivel_id = @nivelId " + // Asegúrate de tener un espacio antes de WHERE
                                              "WHERE id = @id", this.connection);

            query.Parameters.AddWithValue("@id", id);
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

            int result = query.ExecuteNonQuery();
        }

        // Borrar Estudiante
        public void deleteStudent(int id)
        {
            SqlCommand query = new SqlCommand("DELETE FROM estudiantes WHERE id = @id", this.connection);
            query.Parameters.AddWithValue("@id", id);
            int result = query.ExecuteNonQuery();
        }








    }
}