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
                          string observaciones, string nivelId)
        {

            // Llamar al método insertStudent de la instancia connection sin esperar un retorno.
            this.connection.insertStudent(primerNombre, segundoNombre, primerApellido, segundoApellido,
                                          telefono, celular, direccion, gmail, fechaNacimiento,
                                          observaciones, nivelId);
        }


        // method to eliminate students
        public void deleteStudent() { }

        // method to update students
        public void updateStudent() { }
    }
}
