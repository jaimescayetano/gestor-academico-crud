using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    public class AdminModel
    {
        public Connection connection;

        public AdminModel()
        {
            // obtaining connection instance
            this.connection = Connection.getInstance();
        }

        public List<List<string>> getAdmins()
        {
            //
            return this.connection.getAdministrators();
        }
    }
}
