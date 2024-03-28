﻿using DataLayer;
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
            return this.connection.getLevels();
        }

        // method to insert students
        public void insertLevel(char nivelAcademico, string seccion, int grado, string tutor,string observaciones, int aulaId)
        {
            this.connection.insertLevel(nivelAcademico, seccion, grado, tutor,observaciones, aulaId);
        }

        // method to update students
        public void updateLevel(int id, char nivelAcademico, string seccion, int grado, string tutor, string observaciones, int aulaId)
        {
            this.connection.updateLevel(id, nivelAcademico, seccion, grado, tutor, observaciones, aulaId);
        }

        // method to select level by ID
        public List<string> getLevelById(int levelId)
        {
            return this.connection.getLevelById(levelId);
        }

        // method to delete students
        public void deleteLevel(int id)
        {
            this.connection.deleteLevel(id);
        }

        
    }
}
