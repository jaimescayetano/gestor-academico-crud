using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class StudentModel
    {
        public Connection connection;

        public StudentModel()
        {
            // obtaining connection instance
            this.connection = Connection.getInstance();
        }

        // method to obtain students
        public List<List<string>> getStudents()
        {
            return this.connection.getStudents();
        }


        // method to insert students
        public void insertStudent(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, int nivelId)
        {

            // Llamar al método insertStudent de la instancia connection sin esperar un retorno.
            this.connection.insertStudent(primerNombre, segundoNombre, primerApellido, segundoApellido,
                                          telefono, celular, direccion, gmail, fechaNacimiento,
                                          observaciones, nivelId);
        }


        // method to eliminate students
        // Método para eliminar un estudiante
        public void deleteStudent(int studentId)
        {
            // Llama al método deleteStudent de la instancia connection para eliminar al estudiante con el ID proporcionado
           // this.connection.deleteStudent(studentId);
        }

        // Método para actualizar los datos de un estudiante
        public void updateStudent(int studentId, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                  string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                  string observaciones, int nivelId)
        {
            // Llama al método updateStudent de la instancia connection para actualizar los datos del estudiante con el ID proporcionado
            //this.connection.updateStudent(studentId, primerNombre, segundoNombre, primerApellido, segundoApellido,
                // telefono, celular, direccion, gmail, fechaNacimiento,
                 //observaciones, nivelId);
        }
    }
}
