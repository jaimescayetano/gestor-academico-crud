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
            this.connection = Connection.getInstance();
        }

        public List<List<string>> getStudents()
        {
            return this.connection.getStudents();
        }


        public void insertStudent(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, string nivelId)
        {

            this.connection.insertStudent(primerNombre, segundoNombre, primerApellido, segundoApellido,
                                          telefono, celular, direccion, gmail, fechaNacimiento,
                                          observaciones, nivelId);
        }

        public void deleteStudent() { }

        public void updateStudent() { }
    }
}
