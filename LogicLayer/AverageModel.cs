using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AverageModel
    {
        public Connection connection;

        public AverageModel()
        {
            this.connection = Connection.getInstance();
        }

        public List<List<string>> getAverages()
        {
            return connection.getAverages();
        }
    }
}
