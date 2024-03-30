using LogicLayer;
using PresentationLayer.classroomPages;
using PresentationLayer.studentPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresentationLayer.averagePages
{
    /// <summary>
    /// Lógica de interacción para averagesList.xaml
    /// </summary>
    public partial class averagesList : Page
    {
        AverageModel averageModel;
        private int idLevel = 0;

        public averagesList()
        {
            InitializeComponent();
            averageModel = new AverageModel();
            renderAverages(loadAverages());
            loadStudentOptions();
        }

        public void loadStudentOptions()
        {
            StudentModel studentModel = new StudentModel();
            cbEstudiante.ItemsSource = studentModel.getStudentsOptions();
        }

        private void showLevel(object sender, EventArgs e)
        {
            var studentId = cbEstudiante.SelectedValue.ToString();
            if (studentId != null)
            {
                LevelModel levelModel = new LevelModel();
                List<string> levelByStudent = levelModel.getLevelByStudentId(int.Parse(studentId));
                this.idLevel = int.Parse(levelByStudent[0]);
                tbNivel.Text = levelByStudent[1];
            }
        }

        public List<List<string>> loadAverages()
        {
            return averageModel.getAverages();
        }

        public void renderAverages(List<List<string>> averages)
        {
            dgPromedios.Items.Clear();
            foreach (var average in averages)
            {
                AverageItem row = new AverageItem
                {
                    id = int.Parse(average[0]),
                    estudiante = average[1],
                    tutor = average[2],
                    promedio = double.Parse(average[3]),
                    nivelAcademico = average[4]
                };
                dgPromedios.Items.Add(row);
            }
        }

        private void removePromedio(object sender, RoutedEventArgs e)
        {
            if (dgPromedios.SelectedItems != null)
            {
                string message = "";
                foreach (AverageItem item in dgPromedios.SelectedItems)
                {
                    message = averageModel.deleteAverage(item.id);
                }
                MessageBox.Show(message, "Eliminación de datos");
                renderAverages(loadAverages());
            } else
            {
                MessageBox.Show("Seleccione los registros que desea eliminar.", "Advertencia");
            }
        }

        private void addPromedio(object sender, RoutedEventArgs e)
        {
            if (validateFields() && this.idLevel != 0)
            {
                double average = double.Parse(tbNota.Text);
                int studentId = int.Parse(cbEstudiante.SelectedValue.ToString());
                string message = averageModel.addAverage(this.idLevel, studentId, average);
                MessageBox.Show(message, "Mensaje");
                renderAverages(loadAverages());
            } else
            {
                MessageBox.Show("Ingrese los campos con valores validos para registrar la promedio correctamente.", "Validación de datos");
            }
        }

        public bool validateFields()
        {
            double average;
            if (cbEstudiante.SelectedValue == null) return false;
            if (!double.TryParse(tbNota.Text, out average)) return false;
            if (average < 0 || average > 20) return false;
            return true;
        }
    }

    public class AverageItem
    {
        public int id { get; set; }
        public string estudiante { get; set; }
        public string tutor { get; set; }
        public double promedio { get; set; }
        public string nivelAcademico { get; set; }
    }
}
