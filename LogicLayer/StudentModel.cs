﻿using DataLayer;
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

        public List<Dictionary<string, string>> getStudentsOptions()
        {
            return this.connection.getStudentsOptions();
        }

        public void insertStudent(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, string nivelId)
        {
            this.connection.insertStudent(primerNombre, segundoNombre, primerApellido, segundoApellido,
                                          telefono, celular, direccion, gmail, fechaNacimiento,
                                          observaciones, nivelId);
        }

        public void updateStudent(int id, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido,
                          string telefono, string celular, string direccion, string gmail, DateTime fechaNacimiento,
                          string observaciones, string nivelId = "")
        {
            this.connection.updateStudent(id, primerNombre, segundoNombre, primerApellido, segundoApellido,
                                          telefono, celular, direccion, gmail, fechaNacimiento,
                                          observaciones, nivelId);
        }

        public List<string> getStudentById(int studentId)
        {
            return this.connection.getStudentById(studentId);
        }

        public void deleteStudent(int id)
        {
            this.connection.deleteStudent(id);
        }
    }
}
