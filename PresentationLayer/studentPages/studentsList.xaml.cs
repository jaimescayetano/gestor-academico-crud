using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.studentPages
{
    /// <summary>
    /// Lógica de interacción para studentsList.xaml
    /// </summary>
    public partial class studentsList : Page
    {
        public StudentModel studentModel;

        public studentsList()
        {
            InitializeComponent();
            studentModel = new StudentModel();
            renderStudents(loadStudents());
        }

        public List<List<string>> loadStudents()
        {
            return studentModel.getStudents();
        }

        public void renderStudents(List<List<string>> students)
        {
            dgEstudiantes.Items.Clear();
            foreach (var student in students)
            {
                var row = new
                {
                    id = student[0],
                    primerNombre = student[1],
                    segundoNombre = student[2],
                    primerApellido = student[3],
                    segundoApellido = student[4],
                    telefono = student[5],
                    celular = student[6],
                    direccion = student[7],
                    gmail = student[8],
                    fechaNacimiento = student[9],
                    observaciones = student[10],
                    nivelAcademico = student[11],
                    gradoSeccion = student[12]
                };
                dgEstudiantes.Items.Add(row);
            }
        }

        private void editStudent(object sender, RoutedEventArgs e)
        {
            
        }

        private void removeStudent(object sender, RoutedEventArgs e)
        {

        }

        private void addStudent(object sender, RoutedEventArgs e)
        {
            addStudent addStudent = new addStudent(this);
            addStudent.ShowDialog();
        }
    }
}
