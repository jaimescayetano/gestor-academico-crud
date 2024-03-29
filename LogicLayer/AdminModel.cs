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
            this.connection = Connection.getInstance();
        }

        public bool ValidateUserCredentials(string gmail, string contraseña)
        {
            return connection.ValidateUser(gmail, contraseña);
        }
    }
}
