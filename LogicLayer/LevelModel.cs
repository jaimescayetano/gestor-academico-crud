using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class LevelModel
    {
        public Connection connection;

        public LevelModel()
        {
            // obtaining connection instance
            this.connection = Connection.getInstance();
        }


        // method to obatin levels
        public List<List<string>> getLevels()
        {
            //
            return this.connection.getLevels();
        }

        public List<Dictionary<string, string>> getLevesOptions()
        {
            return connection.getLevesOptions();
        }
    }
}
