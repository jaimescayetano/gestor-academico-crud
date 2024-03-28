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
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Lógica de interacción para FormListaEstudiante.xaml
    /// </summary>
    public partial class FormListaEstudiante : Window
    {
        public FormListaEstudiante()
        {
            InitializeComponent();
            StudentList();
            
        }

        public void StudentList()
        {
            dgEstudiantes.Items.Clear();
            StudentModel studentModel = new StudentModel();
            foreach (var item in studentModel.getStudents())
            {
                var row = new
                {
                    Id = item[0],
                    Primer_Nombre = item[1],
                    Segundo_Nombre = item[2],
                    Primer_Apellido = item[3],
                    Segundo_Apellido = item[4],
                    Telefono = item[5],
                    Celular = item[6],
                    Direccion = item[7],
                    Gmail = item[8],
                    Fecha_Nacimiento = item[9],
                    Observaciones = item[10],
                    Id_Nivel = item[11]
                };

                dgEstudiantes.Items.Add(row);
            }
        }

        private void btnNuevoEstudiante_Click(object sender, RoutedEventArgs e)
        {
            FormNuevoEstudiante nuevoEstudiante = new FormNuevoEstudiante();
            nuevoEstudiante.Show();
            this.Close();
        }

       

        private void btnEditarEstudiante_Click(object sender, RoutedEventArgs e)
        {
            if (dgEstudiantes.SelectedItem != null)
            {
                var selectedRow = (dynamic)dgEstudiantes.SelectedItem;
                int selectedLevelId = int.Parse(selectedRow.Id);

                FormEditarEstudiante editStudentForm = new FormEditarEstudiante(selectedLevelId);
                editStudentForm.ShowDialog();

                StudentList();
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante");
            }

            this.Close();
        }

        private void btnEliminarEstudiant_Click(object sender, RoutedEventArgs e)
        {
            if (dgEstudiantes.SelectedItem != null)
            {
                var selectedRow = (dynamic)dgEstudiantes.SelectedItem;
                int selectedLevelId = int.Parse(selectedRow.Id);

                var result = MessageBox.Show("¿Está seguro de que desea eliminar al estudiante seleccionado?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.deleteStudent(selectedLevelId);

                    MessageBox.Show("Estudiante eliminado con éxito.", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);


                    dgEstudiantes.Items.Clear();
                    StudentList();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

       
    }
    
}
