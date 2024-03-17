using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class EstudentModel
    {
        public Connection connection;

        public EstudentModel()
        {
            // obtaining connection instance
            this.connection = Connection.getInstance();
        }

        // method to obtain students
        public void getStudents() { }

        // method to insert students
        public void insertStudent() { }

        // method to eliminate students
        public void deleteStudent() { }

        // method to update students
        public void updateStudent() { }
    }
}
