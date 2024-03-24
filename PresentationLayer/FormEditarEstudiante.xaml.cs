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
using LogicLayer;

namespace PresentationLayer{
    public partial class FormEditarEstudiante : Window
    {
        
       

        public FormEditarEstudiante()
        {
            InitializeComponent();

        }
        
        private void btnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos editados por el usuario
            string nuevoPrimerNombre = tbPrimerNombre.Text;
            string nuevoSegundoNombre = tbSegundoNombre.Text;
            string nuevoApellidoPaterno = tbApellidoPaterno.Text;
            string nuevoApellidoMaterno = tbApellidoMaterno.Text;
            string nuevoTelefono = tbTelefono.Text;
            string nuevoCelular = tbCelular.Text;
            string nuevoEmail = tbEmail.Text;
            DateTime nuevoFechaNacimiento = dpNacimiento.SelectedDate ?? DateTime.MinValue;
            string nuevaDireccion = tbDireccion.Text;
            string nuevasObservaciones = tbObservaciones.Text;
            int nuevoIdNivel = int.Parse(tbIdNivel.Text);

            // Actualizar los datos del estudiante en la base de datos
            //studentModel.updateStudent(estudianteSeleccionado.Id, nuevoPrimerNombre, nuevoSegundoNombre, nuevoApellidoPaterno, nuevoApellidoMaterno,
                                      // nuevoTelefono, nuevoCelular, nuevaDireccion, nuevoEmail, nuevoFechaNacimiento,
                                       //nuevasObservaciones, nuevoIdNivel);

            MessageBox.Show("Los cambios se guardaron correctamente.");

            // Cerrar la ventana de edición
            this.Close();
        }

        private void btnMostrarNiveles_Click(object sender, RoutedEventArgs e)
        {
            FormLevels listLevel = new FormLevels();
            listLevel.Show();
        }
    }}