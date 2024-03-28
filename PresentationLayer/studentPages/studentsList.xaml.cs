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
                StudentItem row = new StudentItem
                {
                    id = int.Parse(student[0]),
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
            if (dgEstudiantes.SelectedItem != null)
            {
                var selectedRow = (dynamic)dgEstudiantes.SelectedItem;
                int selectedLevelId = int.Parse(selectedRow.Id);

                FormEditarEstudiante editStudentForm = new FormEditarEstudiante(selectedLevelId);
                editStudentForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante");
            }
        }

        private void removeStudent(object sender, RoutedEventArgs e)
        {
            if (dgEstudiantes.SelectedItems != null)
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar a los estudiantes seleccionados?", 
                    "Confirmar eliminación", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (StudentItem item in dgEstudiantes.SelectedItems)
                    {
                        studentModel.deleteStudent(item.id);
                    }
                    MessageBox.Show("La operación se realizó con éxito.", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);
                    renderStudents(loadStudents());
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void addStudent(object sender, RoutedEventArgs e)
        {
            addStudent addStudent = new addStudent(this);
            addStudent.ShowDialog();
        }
    }

    public class StudentItem
    {
        public int id { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string direccion { get; set; }
        public string gmail { get; set; }
        public string observaciones { get; set; }
        public string fechaNacimiento { get; set; }
        public string nivelAcademico { get; set; }
        public string gradoSeccion { get; set; }
    }
}
