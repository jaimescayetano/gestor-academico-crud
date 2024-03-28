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
    /// Lógica de interacción para FormNuevoEstudiante.xaml
    /// </summary>
    public partial class FormNuevoEstudiante : Window
    {
        public FormNuevoEstudiante()
        {
            InitializeComponent();
            tbPrimerNombre.Focus();
        }
        private void btnMostrarNiveles_Click(object sender, RoutedEventArgs e)
        {
            FormLevels listLevel = new FormLevels();
            listLevel.Show();
        }


        private void btnIRegistrarEstudiante_Click(object sender, RoutedEventArgs e)
        {
            string primerNombre = tbPrimerNombre.Text;
            string segundoNombre = tbSegundoNombre.Text;
            string apellidoPaterno = tbApellidoPaterno.Text;
            string apellidoMaterno = tbApellidoMaterno.Text;
            string telefono = tbTelefono.Text;
            string celular = tbCelular.Text;
            string email = tbEmail.Text;
            DateTime fechaNacimiento = dpNacimiento.SelectedDate.HasValue ? dpNacimiento.SelectedDate.Value : DateTime.Now;
            string direccion = tbDireccion.Text;
            string observaciones = tbObservaciones.Text;
            int nivelId = 0;

            /*
            bool conversionExitosa = int.TryParse(tbIdNivel.Text, out nivelId);
            if (!conversionExitosa)
            {
                MessageBox.Show("Por favor, ingresa un ID de nivel válido.");
                return;
            }

            StudentModel modeloEstudiante = new StudentModel();

            try
            {
                modeloEstudiante.insertStudent(primerNombre, segundoNombre, apellidoPaterno, apellidoMaterno,
                                                telefono, celular, direccion, email, fechaNacimiento,
                                                observaciones, nivelId);
                MessageBox.Show("Estudiante registrado con éxito.");
                tbPrimerNombre.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el estudiante: {ex.Message}");
            }
            */

            
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            FormListaEstudiante listStudent = new FormListaEstudiante();
            this.Close();
        }
    }
}
