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

namespace PresentationLayer.levelPages
{
    /// <summary>
    /// Lógica de interacción para addLevel.xaml
    /// </summary>
    public partial class addLevel : Window
    {
        levelsList levesList;

        public addLevel(levelsList window)
        {
            InitializeComponent();
            this.levesList = window;
            loadClassroomsOptions();
        }

        public void loadClassroomsOptions()
        {
            ClassroomModel classroomModel = new ClassroomModel();
            cbClassrooms.ItemsSource = classroomModel.getClassroomsOptions();
        }

        private void registerLevel(object sender, RoutedEventArgs e)
        {
            if (validateFields())
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
                string aulaId = cbClassrooms.SelectedValue.ToString();
                if (!int.TryParse(aulaId, out int id))
                {
                    MessageBox.Show("Por favor, ingresa un ID de aula válido.");
                    return;
                }
                
                LevelModel modeloNivel = new LevelModel();
                try
                {
                    levesList.levelModel.insertLevel(nivelAcademico, seccion, grado, tutor, observaciones, int.Parse(aulaId));
                    MessageBox.Show("Nivel registrado con éxito.");
                    tbNivelAcademico.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al registrar el Nivel: {ex.Message}");
                }
                clearFields();
                levesList.renderLeves(levesList.loadLevels());
            } else
            {
                MessageBox.Show("Complete los campos para un correcto registro del nivel", "Validación de datos");
            }
        }

        public void clearFields()
        {
            tbNivelAcademico.Text = "";
            tbGrado.Text = "";
            tbSeccion.Text = "";
            tbTutor.Text = "";
            tbObservaciones.Text = "";
        }

        public bool validateFields()
        {
            bool isValid = tbNivelAcademico.Text.Count() > 0 &&
                tbGrado.Text.Count() > 0 &&
                tbSeccion.Text.Count() > 0 &&
                tbTutor.Text.Count() > 0;
            if (cbClassrooms.SelectedValue == null) { return false; }
            return isValid;
        }

        private void returnWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
