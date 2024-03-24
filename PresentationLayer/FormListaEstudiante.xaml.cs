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
        //Declara la variable estudianteSeleccionado
        private StudentModel studentModel;


        public FormListaEstudiante()
        {
            InitializeComponent();
            studentModel = new StudentModel();
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

                // Suscribirse al evento SelectionChanged del DataGrid
                dgEstudiantes.Items.Add(row);
            }
        }

        private void btnNuevoEstudiante_Click(object sender, RoutedEventArgs e)
        {
            FormNuevoEstudiante nuevoEstudiante = new FormNuevoEstudiante();
            nuevoEstudiante.Show();
            this.Hide();
        }private void btnEditarEstudiante_Click(object sender, RoutedEventArgs e)
            {
            // Verificar si hay un estudiante seleccionado
            if (dgEstudiantes.SelectedItem != null)
            {
                // Obtener el estudiante seleccionado
                var estudianteSeleccionado = dgEstudiantes.SelectedItem;
                MessageBox.Show(estudianteSeleccionado.ToString());

                // Abrir el formulario de edición y pasar el estudiante seleccionado
                //FormEditarEstudiante editarEstudiante = new FormEditarEstudiante(estudianteSeleccionado);
                //editarEstudiante.ShowDialog();

                // Actualizar la vista del DataGrid para reflejar los cambios
                dgEstudiantes.Items.Refresh();
            }
        }
        private void btnEliminarEstudiant_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay un estudiante seleccionado en la tabla
            if (dgEstudiantes.SelectedItem != null)
            {
                // Obtener el ID del estudiante seleccionado
                int estudianteId = ObtenerIdEstudianteSeleccionado();

                // Llamar al método deleteStudent de la clase StudentModel para eliminar el estudiante de la base de datos
                studentModel.deleteStudent(estudianteId);

                // Eliminar el estudiante seleccionado de la tabla
                dgEstudiantes.Items.Remove(dgEstudiantes.SelectedItem);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un estudiante para eliminar.");
            }
        }

        private int ObtenerIdEstudianteSeleccionado()
        {
            // Obtener el ID del estudiante seleccionado en la tabla
            int estudianteId = 0;
            if (dgEstudiantes.SelectedItem != null)
            {
                // Seleccionar la fila y obtener el ID desde la propiedad correspondiente
                var selectedItem = (dynamic)dgEstudiantes.SelectedItem;
                estudianteId = selectedItem.Id;
            }
            return estudianteId;
        }

        private void dgEstudiantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }

}
