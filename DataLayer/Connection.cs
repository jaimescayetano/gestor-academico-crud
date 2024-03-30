using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Collections.Specialized.BitVector32;
using System.Windows.Forms;
using System.Reflection;

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
            connection.ConnectionString = hostname;
            connection.Open();
        }

        // create -> student
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

        // read -> students
        public List<List<string>> getStudents()
        {
            List<List<string>> levels = new List<List<string>>();
            SqlCommand query = new SqlCommand(@"SELECT e.id, e.primer_nombre, e.segundo_nombre, e.primer_apellido, e.segundo_apellido, e.telefono, e.celular,
                                                e.direccion, e.gmail, e.fecha_nacimiento, e.observaciones, 
                                                CASE n.nivel_academico 
                                                    WHEN 'I' THEN 'Inicial' 
                                                    WHEN 'P' THEN 'Primaria' 
                                                    WHEN 'S' THEN 'Secundaria' 
                                                END AS nivel_academico,
                                                CONCAT(n.grado,' ', n.seccion) AS grado_seccion 
                                                FROM estudiantes e JOIN niveles n ON e.nivel_id = n.id ", this.connection);
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

        // read options -> students
        public List<Dictionary<string, string>> getStudentsOptions()
        {
            List<Dictionary<string, string>> studentsOptions = new List<Dictionary<string, string>>();
            SqlCommand query = new SqlCommand(@"SELECT id, CONCAT(primer_nombre, ' ', segundo_nombre, ' ', primer_apellido, ' ', segundo_apellido) 
                                                AS estudiante FROM estudiantes;", this.connection);
            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {
                studentsOptions.Add(new Dictionary<string, string>() { { data["id"].ToString(), data["estudiante"].ToString() } });
            }
            data.Close();
            return studentsOptions;
        }

        // read by id -> students
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

        // update -> students
        public void updateStudent(int id, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, string nivelId)
        {
            string stringQuery = "UPDATE estudiantes SET primer_nombre = @primerNombre, " +
                                              "segundo_nombre = @segundoNombre, primer_apellido = @primerApellido, segundo_apellido = @segundoApellido, " +
                                              "telefono = @telefono, celular = @celular, direccion = @direccion, gmail = @gmail, " +
                                              "fecha_nacimiento = @fechaNacimiento, observaciones = @observaciones";

            if (nivelId.Count() > 0)
            {
                stringQuery += ", nivel_id = @nivelId ";
            }

            stringQuery += " WHERE id = @id";
            SqlCommand query = new SqlCommand(stringQuery, this.connection);

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
            if (nivelId.Count() > 0)
            {
                query.Parameters.AddWithValue("@nivelId", nivelId);
            }
            int result = query.ExecuteNonQuery();
        }

        // delete -> students
        public void deleteStudent(int id)
        {
            SqlCommand query = new SqlCommand("DELETE FROM estudiantes WHERE id = @id", this.connection);
            query.Parameters.AddWithValue("@id", id);
            int result = query.ExecuteNonQuery();
        }

        // create -> leves
        public void insertLevel(char nivelAcademico, string seccion, int grado, string tutor,
                          string observaciones, int aulaId)
        {
            SqlCommand query = new SqlCommand(@"INSERT INTO niveles (nivel_academico, seccion, grado, tutor, observaciones, aula_id) 
                                              VALUES (@nivelAcademico, @seccion, @grado, @tutor, @observaciones, @aulaId)", this.connection);
            query.Parameters.AddWithValue("@nivelAcademico", nivelAcademico);
            query.Parameters.AddWithValue("@seccion", seccion);
            query.Parameters.AddWithValue("@grado", grado);
            query.Parameters.AddWithValue("@tutor", tutor);
            query.Parameters.AddWithValue("@observaciones", observaciones);
            query.Parameters.AddWithValue("@aulaId", aulaId);
            int result = query.ExecuteNonQuery();
        }

        // read -> leves
        public List<List<string>> getLevels()
        {
            List<List<string>> levels = new List<List<string>>();
            SqlCommand query = new SqlCommand("SELECT n.*, a.numero, a.capacidad FROM niveles n INNER JOIN aulas a ON n.aula_id = a.id", this.connection);
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

        // read by student -> level
        public List<string> getLevelByStudent(int id)
        {
            SqlCommand query = new SqlCommand(@"SELECT n.id AS id, CONCAT(grado, seccion, ' - ',
                                                CASE
                                                    WHEN nivel_academico = 'P' THEN ' Primaria'
                                                    WHEN nivel_academico = 'I' THEN ' Inicial'
                                                    WHEN nivel_academico = 'S' THEN ' Secundaria'
                                                    ELSE ' No encontrado' 
                                                END) AS nivel_academico
                                                FROM estudiantes e
                                                INNER JOIN niveles n ON e.nivel_id = n.id
                                                WHERE e.id = @id;", this.connection);
            query.Parameters.AddWithValue("@id", id);
            SqlDataReader data = query.ExecuteReader();
            if (data.Read())
            {
                List<string> level = new List<string>() { data["id"].ToString(), data["nivel_academico"].ToString() };
                data.Close();
                return level;
            } else
            {
                data.Close();
                return null;
            }
            
        }

        // read options -> leves
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

        // read by id -> leves
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

        // update -> leves
        public void updateLevel(int id, char nivelAcademico, string seccion, int grado, string tutor,
                        string observaciones, string aulaId)
        {
            string stringQuery = "UPDATE niveles SET nivel_academico = @nivelAcademico, seccion = @seccion, grado = @grado, tutor = @tutor, observaciones = @observaciones";

            if (aulaId.Count() > 0) stringQuery += ", aula_id = @aulaId ";
            stringQuery += " WHERE id = @id";

            SqlCommand query = new SqlCommand(stringQuery, this.connection);
            query.Parameters.AddWithValue("@id", id);
            query.Parameters.AddWithValue("@nivelAcademico", nivelAcademico);
            query.Parameters.AddWithValue("@seccion", seccion);
            query.Parameters.AddWithValue("@grado", grado);
            query.Parameters.AddWithValue("@tutor", tutor);
            query.Parameters.AddWithValue("@observaciones", observaciones);
            if (aulaId.Count() > 0) query.Parameters.AddWithValue("@aulaId", aulaId);
            int result = query.ExecuteNonQuery();
        }

        // delete -> leves
        public void deleteLevel(int id)
        {
            SqlCommand query = new SqlCommand("DELETE FROM niveles WHERE id = @id", this.connection);
            query.Parameters.AddWithValue("@id", id);
            int result = query.ExecuteNonQuery();
        }

        // create -> classroom
        public string insertClassroom(int numero, int capacidad, string observaciones)
        {
            try
            {
                SqlCommand query = new SqlCommand("INSERT INTO aulas (numero, capacidad, observaciones) VALUES (@numero, @capacidad, @observaciones)", this.connection);
                query.Parameters.AddWithValue("@numero", numero);
                query.Parameters.AddWithValue("@capacidad", capacidad);
                query.Parameters.AddWithValue("@observaciones", observaciones);
                int result = query.ExecuteNonQuery();
                if (result == 1)
                    return "El aula fue registrada correctamente.";
                else
                    return "No se pudo registrar el aula, intentelo nuevamente.";
            }
            catch (Exception e)
            {
                return $"Ocurrió un error inesperado: {e.Message}";
            }
        }

        // read -> classroom
        public List<List<string>> getClassrooms()
        {
            List<List<string>> classrooms = new List<List<string>>();
            SqlCommand query = new SqlCommand("SELECT * FROM aulas", this.connection);
            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {
                classrooms.Add(new List<string>() { data["id"].ToString(), data["numero"].ToString(), data["capacidad"].ToString(), data["observaciones"].ToString() });
            }
            data.Close();
            return classrooms;
        }

        // read by id -> classroom
        public List<string> getClassroomById(int classroomId)
        {
            SqlCommand query = new SqlCommand("SELECT * FROM aulas WHERE id = @classroomId", this.connection);
            query.Parameters.AddWithValue("@classroomId", classroomId);
            SqlDataReader data = query.ExecuteReader();

            if (data.Read())
            {
                List<string> classroom = new List<string>() {
                    data["id"].ToString(),
                    data["numero"].ToString(),
                    data["capacidad"].ToString(),
                    data["observaciones"].ToString()
                };
                data.Close();
                return classroom;
            }
            else
            {
                data.Close();
                return null;
            }
        }

        // read options -> classroom
        public List<Dictionary<string, string>> getClassroomsOptions()
        {
            List<Dictionary<string, string>> classroomsOptions = new List<Dictionary<string, string>>();
            SqlCommand query = new SqlCommand("SELECT id, numero FROM aulas;", this.connection);
            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {
                classroomsOptions.Add(new Dictionary<string, string>() {{data["id"].ToString(), data["numero"].ToString()}});
            }
            data.Close();
            return classroomsOptions;
        }

        // update -> classroom
        public string updateClassroom(int id, List<string> classroom)
        {
            try
            {
                SqlCommand query = new SqlCommand("UPDATE aulas SET numero = @numero, capacidad = @capacidad, observaciones = @observaciones WHERE id = @id;",
                    this.connection);
                query.Parameters.AddWithValue("@numero", classroom[0]);
                query.Parameters.AddWithValue("@capacidad", classroom[1]);
                query.Parameters.AddWithValue("@observaciones", classroom[2]);
                query.Parameters.AddWithValue("@id", id);
                int result = query.ExecuteNonQuery();
                if (result == 1)
                    return "El aula se actualizo correctamente.";
                return "No se pudo actualizar el aula.";
            }
            catch (Exception e)
            {
                return $"Ocurrió un error inesperado: {e.Message}";
            }
        }

        // delete -> classroom
        public string deleteClassroom(int id)
        {
            try
            {
                SqlCommand query = new SqlCommand("DELETE FROM aulas WHERE id = @id", this.connection);
                query.Parameters.AddWithValue("@id", id);
                int rowsAffected = query.ExecuteNonQuery();
                return rowsAffected > 0 ? "El aula se eliminó correctamente" : "No se eliminó el aula";
            }
            catch (Exception e)
            {
                return $"Ocurrió un error inesperado: {e.Message}";
            }
        }

        // create -> average
        public string insertAverage(int levelId, int studentId, double average)
        {
            try
            {
                SqlCommand query = new SqlCommand("INSERT INTO promedios (promedio, estudiante_id, nivel_id) VALUES (@promedio, @estudiante_id, @nivel_id)", this.connection);
                query.Parameters.AddWithValue("@promedio", average);
                query.Parameters.AddWithValue("@estudiante_id", studentId);
                query.Parameters.AddWithValue("@nivel_id", levelId);
                int result = query.ExecuteNonQuery();
                if (result == 1)
                    return "El promedio fue registrado correctamente.";
                else
                    return "No se pudo registrar el promedio, intentelo nuevamente.";
            }
            catch (Exception e)
            {
                return $"Ocurrió un error inesperado: {e.Message}";
            }
        }

        // read -> average
        public List<List<string>> getAverages()
        {
            List<List<string>> averages = new List<List<string>>();
            SqlCommand query = new SqlCommand(@"SELECT p.id AS id,
                                        CONCAT(e.primer_nombre, ' ', e.segundo_nombre, ' ',
                                        e.primer_apellido, ' ', e.segundo_apellido) AS estudiante,
                                        n.tutor AS tutor,
                                        p.promedio AS promedio,
                                        CONCAT(n.grado, n.seccion,
                                               CASE
                                                   WHEN n.nivel_academico = 'P' THEN ' Primaria'
                                                   WHEN n.nivel_academico = 'S' THEN ' Secundaria'
                                                   WHEN n.nivel_academico = 'I' THEN ' Inicial'
                                               END) AS nivel_academico
                                        FROM promedios p
                                        INNER JOIN estudiantes e ON p.estudiante_id = e.id
                                        INNER JOIN niveles n ON p.nivel_id = n.id", this.connection);

            SqlDataReader data = query.ExecuteReader();
            while (data.Read())
            {
                averages.Add(new List<string>()
                {
                    data["id"].ToString(),
                    data["estudiante"].ToString(),
                    data["tutor"].ToString(),
                    data["promedio"].ToString(),
                    data["nivel_academico"].ToString()
                });
            }
            data.Close();
            return averages;
        }

        // delete -> average
        public string deleteAverage(int id)
        {
            try
            {
                SqlCommand query = new SqlCommand("DELETE FROM promedios WHERE id = @id", this.connection);
                query.Parameters.AddWithValue("@id", id);
                int rowsAffected = query.ExecuteNonQuery();
                return rowsAffected > 0 ? "El promedio se eliminó correctamente" : "No se eliminó el promedio";
            }
            catch (Exception e)
            {
                return $"Ocurrió un error inesperado: {e.Message}";
            }
        }

        // validate -> administrator
        public bool ValidateUser(string gmail, string contraseña)
        {
            SqlCommand query = new SqlCommand("SELECT COUNT(*) FROM administradores WHERE gmail = @Email AND contraseña = @Password", this.connection);
            query.Parameters.AddWithValue("@Email", gmail);
            query.Parameters.AddWithValue("@Password", contraseña);
            int count = (int)query.ExecuteScalar();
            return count > 0;
        }
    }
}