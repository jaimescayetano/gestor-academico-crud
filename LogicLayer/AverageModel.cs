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

        public string deleteAverage(int id)
        {
            return this.connection.deleteAverage(id);
        }

        public string addAverage(int levelId, int studentId, double average)
        {
            return this.connection.insertAverage(levelId, studentId, average);
        }

        public List<List<string>> getAverages()
        {
            return connection.getAverages();
        }
    }
}
