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

        public List<List<string>> getClassrooms()
        {
            return this.connection.getClassrooms();
        }

        public string deleteClassroom(int id) 
        {
            return this.connection.deleteClassroom(id);
        }
    }
}
