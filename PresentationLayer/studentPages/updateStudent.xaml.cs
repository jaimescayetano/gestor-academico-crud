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
    /// Lógica de interacción para updateStudent.xaml
    /// </summary>
    public partial class updateStudent : Window
    {
        private int studentId;
        studentsList studentList;

        public updateStudent(int studentId, studentsList window)
        {
            InitializeComponent();
            this.studentId = studentId;
            studentList = window;
            loadStudentData();
            loadLevesOptions();
        }

        public void loadLevesOptions()
        {
            LevelModel levelModel = new LevelModel();
            cbNiveles.ItemsSource = levelModel.getLevesOptions();
        }

        private void loadStudentData()
        {
            var data = studentList.studentModel.getStudentById(studentId);
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
            }
            else
            {
                MessageBox.Show("No se encontró el estudiante.", "Advertencia");
            }
        }

        private void saveChanges(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                try
                {
                    DateTime fechaNacimiento = dpNacimiento.SelectedDate.HasValue ? dpNacimiento.SelectedDate.Value : DateTime.Now;
                    
                    if (cbNiveles.SelectedValue == null)
                    {
                        studentList.studentModel.updateStudent(studentId, tbPrimerNombre.Text, tbSegundoNombre.Text, tbApellidoPaterno.Text,
                        tbApellidoMaterno.Text, tbTelefono.Text, tbCelular.Text, tbDireccion.Text, tbEmail.Text, fechaNacimiento,
                        tbObservaciones.Text);
                    } else
                    {
                        string nivelId = cbNiveles.SelectedValue.ToString();
                        studentList.studentModel.updateStudent(studentId, tbPrimerNombre.Text, tbSegundoNombre.Text, tbApellidoPaterno.Text,
                        tbApellidoMaterno.Text, tbTelefono.Text, tbCelular.Text, tbDireccion.Text, tbEmail.Text, fechaNacimiento,
                        tbObservaciones.Text, nivelId);
                    }
                    
                    MessageBox.Show("Estudiante actualizado con éxito.");
                    studentList.renderStudents(studentList.loadStudents());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Se produjo un error al actualizar el estudiante: {ex.Message}");
                }
            } else
            {
                MessageBox.Show("Complete todos los campos requeridos para una correcta actualización", "Validación de datos");
            }
        }

        private void returnWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool validateFields()
        {
            return tbPrimerNombre.Text.Count() > 0 &&
                tbSegundoNombre.Text.Count() > 0 &&
                tbApellidoPaterno.Text.Count() > 0 &&
                tbApellidoMaterno.Text.Count() > 0 &&
                tbCelular.Text.Count() > 0 &&
                tbEmail.Text.Count() > 0 &&
                tbDireccion.Text.Count() > 0;
        }
    }
}
