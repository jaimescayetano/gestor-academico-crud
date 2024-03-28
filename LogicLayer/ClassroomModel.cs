using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ClassroomModel
    {
        public Connection connection;

        public ClassroomModel() 
        {
            this.connection = Connection.getInstance();
        }

        public string updateClassroom(int id, List<string> classroom)
        {
            return this.connection.updateClassroom(id, classroom);
        }

        public List<List<string>> getClassrooms()
        {
            return this.connection.getClassrooms();
        }

        public string insertClassroom(int numero, int capacidad, string observaciones)
        {
            return this.connection.insertClassroom(numero, capacidad, observaciones);
        }

        public List<string> getClassroomById(int id)
        {
            return this.connection.getClassroomById(id);
        }

        public string deleteClassroom(int id)
        {
            return this.connection.deleteClassroom(id);
        }

        public List<Dictionary<string, string>> getClassroomsOptions()
        {
            return connection.getClassroomsOptions();
        }
    }
}
