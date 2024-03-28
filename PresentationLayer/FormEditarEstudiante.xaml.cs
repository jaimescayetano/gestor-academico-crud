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
    /// Lógica de interacción para FormEditarEstudiante.xaml
    /// </summary>
    public partial class FormEditarEstudiante : Window
    {
        private int studentId;
        public FormEditarEstudiante(int id)
        {
            InitializeComponent();
            studentId = id;
            LoadLevelData();
        }

        private void LoadLevelData()
        {
            StudentModel studentModel = new StudentModel();
            var data = studentModel.getStudentById(studentId);

            if (data != null)
            {
                tbPrimerNombre.Text = data[1]; 
                tbSegundoNombre.Text = data[2]; 
                tbApellidoPaterno.Text = data[3];
                tbApellidoMaterno.Text = data[4]; 
                tbTelefono.Text = data[5];
                tbCelular.Text = data[6];
                tbDireccion.Text = data[7];
                tbEmail.Text = data[8];
                DateTime fechaNacimiento;

                if (DateTime.TryParse(data[9], out fechaNacimiento))
                {
                    dpNacimiento.SelectedDate = fechaNacimiento;
                }
                else
                {
                    dpNacimiento.SelectedDate = null;
                }
                tbObservaciones.Text = data[10];
                tbIdNivel.Text = data[11];

            }
            else
            {
                MessageBox.Show("No se encontró el estudiante.");
            }
        }

        private void btnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            try
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
                int nivelId = int.Parse(tbIdNivel.Text);


                // Creación del modelo y actualización del estudiante
                StudentModel modeloEstudiante = new StudentModel();
                modeloEstudiante.updateStudent(studentId, primerNombre, segundoNombre, apellidoPaterno, apellidoMaterno,
                                               telefono, celular, direccion, email, fechaNacimiento, observaciones, nivelId);

                MessageBox.Show("Estudiante actualizado con éxito.");

            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, asegúrate de que todos los campos numéricos están correctamente formateados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al actualizar el estudiante: {ex.Message}");
            }
        }
        private void btnMostrarNiveles_Click(object sender, RoutedEventArgs e)
        {
            FormLevels listLevel = new FormLevels();
            listLevel.Show();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            /*
            FormListaEstudiante  = new FormListaEstudiante();
            listStudent.Show();
            */
            this.Close();
        }
    }
}
