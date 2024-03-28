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
    /// Lógica de interacción para FormEditLevel.xaml
    /// </summary>
    public partial class FormEditLevel : Window
    {

        private int levelId;

        public FormEditLevel(int id)
        {
            InitializeComponent();
            levelId = id;
            LoadLevelData();
        }

        private void LoadLevelData()
        {
            LevelModel levelModel = new LevelModel();
            var data = levelModel.getLevelById(levelId);

            if (data != null)
            {
                    tbNivelAcademico.Text = data[1];
                    tbSeccion.Text = data[2];
                    tbGrado.Text = data[3];
                    tbTutor.Text = data[4]; 
                    tbObservaciones.Text = data[5]; 
                    tbAulaId.Text = data[6];
            }
            else
            {
                MessageBox.Show("No se encontró el nivel.");
            }
        }

        private void btnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                char nivelAcademico = !string.IsNullOrEmpty(tbNivelAcademico.Text) ? tbNivelAcademico.Text[0] : char.MinValue;
                string seccion = tbSeccion.Text;
                int grado = int.Parse(tbGrado.Text);
                string tutor = tbTutor.Text;
                string observaciones = tbObservaciones.Text;
                int aulaId = int.Parse(tbAulaId.Text);

                LevelModel levelModel = new LevelModel();
                levelModel.updateLevel(levelId, nivelAcademico, seccion, grado, tutor, observaciones, aulaId);

                MessageBox.Show("Nivel actualizado con éxito.");

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

        private void btnMostrarAulas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            FormLevels listStudent = new FormLevels();
            listStudent.Show();
            this.Close();
        }




        //


    }
}
