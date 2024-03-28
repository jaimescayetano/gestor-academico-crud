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

namespace PresentationLayer.studentPages
{
    /// <summary>
    /// Lógica de interacción para addStudent.xaml
    /// </summary>
    public partial class addStudent : Window
    {
        studentsList studentList;

        public addStudent(studentsList window)
        {
            studentList = window;
            InitializeComponent();
            loadLevesOptions();
        }

        public void loadLevesOptions()
        {
            LevelModel levelModel = new LevelModel();
            cbNiveles.ItemsSource = levelModel.getLevesOptions();
        }

        private void returnWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void registerStudent(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                DateTime fechaNacimiento = dpNacimiento.SelectedDate.HasValue ? dpNacimiento.SelectedDate.Value : DateTime.Now;
                string nivelId = cbNiveles.SelectedValue.ToString();

                try
                {
                    studentList.studentModel.insertStudent(tbPrimerNombre.Text, tbSegundoNombre.Text, tbApellidoPaterno.Text,
                        tbApellidoMaterno.Text, tbTelefono.Text, tbCelular.Text, tbDireccion.Text, tbEmail.Text, fechaNacimiento,
                        tbObservaciones.Text, nivelId);
                    MessageBox.Show("Estudiante registrado con éxito.");
                    tbPrimerNombre.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al registrar el estudiante: {ex.Message}");
                }
                clearFields();
                studentList.renderStudents(studentList.loadStudents());
            } else
            {
                MessageBox.Show("Complete los campos para un correcto registro del estudiante", "Validación de datos");
            }
        }

        public bool validateFields()
        {
            bool isValid = tbPrimerNombre.Text.Count() > 0 &&
                tbSegundoNombre.Text.Count() > 0 &&
                tbApellidoPaterno.Text.Count() > 0 &&
                tbApellidoMaterno.Text.Count() > 0 &&
                tbCelular.Text.Count() > 0 &&
                tbEmail.Text.Count() > 0 &&
                tbDireccion.Text.Count() > 0;
            if (cbNiveles.SelectedValue == null) { return false; }
            return isValid;
        }

        public void clearFields()
        {
            tbPrimerNombre.Text = "";
            tbSegundoNombre.Text = "";
            tbApellidoPaterno.Text = "";
            tbApellidoMaterno.Text = "";
            tbCelular.Text = "";
            tbTelefono.Text = "";
            tbEmail.Text = "";
            tbDireccion.Text = "";
        }
    }
}
