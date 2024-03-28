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
    /// Lógica de interacción para FormLevels.xaml
    /// </summary>
    public partial class FormLevels : Window
    {
        public FormLevels()
        {
            InitializeComponent();
            ShowList();
        }

        public void ShowList()
        {
            LevelModel levelModel = new LevelModel();
            foreach (var item in levelModel.getLevels())
            {
                var row = new
                {
                    Id = item[0],
                    Nivel_Academico = item[1],
                    Seccion = item[2],
                    Grado = item[3],
                    Tutor = item[4],
                    Observaciones = item[5],
                    Aula_ID = item[6],
                    Numero = item[7],
                    Capacidad = item[8]
                };

                tbDatos.Items.Add(row);
            }
        }

        private void btnNuevoNivel_Click(object sender, RoutedEventArgs e)
        {
            FormNewLevel level = new FormNewLevel();
            level.ShowDialog();
            this.Close();
        }

        private void btnEditarNivel_Click(object sender, RoutedEventArgs e)
        {
            if (tbDatos.SelectedItem != null)
            {
                var selectedRow = (dynamic)tbDatos.SelectedItem;
                int selectedLevelId = int.Parse(selectedRow.Id);

                FormEditLevel editLevelForm = new FormEditLevel(selectedLevelId);
                editLevelForm.ShowDialog();

                ShowList();
            }
            else
            {
                MessageBox.Show("Seleccione un nivel");
            }

            this.Close();
        }

        private void btnEliminarNivel_Click(object sender, RoutedEventArgs e)
        {
            if (tbDatos.SelectedItem != null)
            {
                var selectedRow = (dynamic)tbDatos.SelectedItem;
                int selectedLevelId = int.Parse(selectedRow.Id);

                var result = MessageBox.Show("¿Está seguro de que desea eliminar el nivel seleccionado?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    LevelModel levelModel = new LevelModel();
                    levelModel.deleteLevel(selectedLevelId);

                    MessageBox.Show("Nivel eliminado con éxito.", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);


                    tbDatos.Items.Clear();
                    ShowList(); 
                }
            }
            else
            {
                MessageBox.Show("Seleccione un nivel para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
       

    }
}
