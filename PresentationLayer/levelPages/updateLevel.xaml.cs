using LogicLayer;
using PresentationLayer.studentPages;
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

namespace PresentationLayer.levelPages
{
    /// <summary>
    /// Lógica de interacción para updateStudent.xaml
    /// </summary>
    public partial class updateLevel : Window
    {
        private int levelId;
        levelsList levelsList;

        public updateLevel(int levelId, levelsList window)
        {
            InitializeComponent();
            this.levelId = levelId;
            levelsList = window;
            loadLevelData();
            loadClassroomsOptions();
        }

        public void loadClassroomsOptions()
        {
            ClassroomModel classroomModel = new ClassroomModel();
            cbClassrooms.ItemsSource = classroomModel.getClassroomsOptions();
        }

        private void loadLevelData()
        {
            var data = levelsList.levelModel.getLevelById(levelId);
            if (data != null)
            {
                tbNivelAcademico.Text = data[1];
                tbSeccion.Text = data[2];
                tbGrado.Text = data[3];
                tbTutor.Text = data[4];
                tbObservaciones.Text = data[5];
            }
            else
            {
                MessageBox.Show("No se encontró el nivel.");
            }
        }

        private void returnWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                char nivelAcademico = !string.IsNullOrEmpty(tbNivelAcademico.Text) ? tbNivelAcademico.Text[0] : char.MinValue;
                string seccion = tbSeccion.Text;
                int grado = int.Parse(tbGrado.Text);
                string tutor = tbTutor.Text;
                string observaciones = tbObservaciones.Text;

                string classroomId = cbClassrooms.SelectedValue == null ? "" : cbClassrooms.SelectedValue.ToString();
                levelsList.levelModel.updateLevel(levelId, nivelAcademico, seccion, grado, tutor, observaciones, classroomId);

                MessageBox.Show("Nivel actualizado con éxito.");
                levelsList.renderLeves(levelsList.loadLevels());
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, asegúrate de que todos los campos numéricos están correctamente formateados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al actualizar el nivel: {ex.Message}");
            }
        }
    }
}
