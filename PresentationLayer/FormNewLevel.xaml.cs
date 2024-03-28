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
    /// Lógica de interacción para FormNewLevel.xaml
    /// </summary>
    public partial class FormNewLevel : Window
    {
        public FormNewLevel()
        {
            InitializeComponent();
        }

        private void btnMostrarAulas_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnRegistrarNivel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNivelAcademico.Text))
            {
                MessageBox.Show("Por favor, ingresa un nivel académico.");
                return;
            }
            char nivelAcademico = tbNivelAcademico.Text[0];
            string seccion = tbSeccion.Text;
            if (!int.TryParse(tbGrado.Text, out int grado))
            {
                MessageBox.Show("Por favor, ingresa un grado válido.");
                return;
            }
            string tutor = tbTutor.Text;
            string observaciones = tbObservaciones.Text;
            if (!int.TryParse(tbAulaId.Text, out int aulaId))
            {
                MessageBox.Show("Por favor, ingresa un ID de aula válido.");
                return;
            }

            LevelModel modeloNivel = new LevelModel();

            try
            {
                modeloNivel.insertLevel(nivelAcademico, seccion, grado, tutor, observaciones, aulaId);
                MessageBox.Show("Nivel registrado con éxito.");
                tbNivelAcademico.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el Nivel: {ex.Message}");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            FormLevels listStudent = new FormLevels();
            listStudent.Show();
            this.Close();
        }
    }
}
