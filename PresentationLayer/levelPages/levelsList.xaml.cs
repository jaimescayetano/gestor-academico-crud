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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.levelPages
{
    /// <summary>
    /// Lógica de interacción para levelsList.xaml
    /// </summary>
    public partial class levelsList : Page
    {
        public LevelModel levelModel;

        public levelsList()
        {
            InitializeComponent();
            levelModel = new LevelModel();
            renderLeves(loadLevels());
        }

        public List<List<string>> loadLevels()
        {
            return levelModel.getLevels();
        }

        public void renderLeves(List<List<string>> leves)
        {
            dgLeves.Items.Clear();
            foreach (var level in leves)
            {
                LevelItem row = new LevelItem
                {
                    id = int.Parse(level[0]),
                    nivelAcademico = level[1],
                    seccion = level[2],
                    grado = level[3],
                    tutor = level[4],
                    observaciones = level[5],
                    aulaId = level[6],
                    numero = level[7],
                    capacidad = level[8]
                };
                dgLeves.Items.Add(row);
            }
        }

        private void editLevel(object sender, RoutedEventArgs e)
        {
            if (dgLeves.SelectedItem != null)
            {
                LevelItem item = (LevelItem) dgLeves.SelectedItem;
                updateLevel updateLevel = new updateLevel(item.id, this);
                updateLevel.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un nivel");
            }
        }

        private void removeLevel(object sender, RoutedEventArgs e)
        {
            if (dgLeves.SelectedItems != null)
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar a los niveles seleccionados?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (LevelItem item in dgLeves.SelectedItems)
                    {
                        levelModel.deleteLevel(item.id);
                    }
                    MessageBox.Show("La operación se realizó con éxito.", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);
                    renderLeves(loadLevels());
                }
            }
            else
            {
                MessageBox.Show("Seleccione un nivel para eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void addLevel(object sender, RoutedEventArgs e)
        {
            addLevel addLevel = new addLevel(this);
            addLevel.ShowDialog();
        }
    }

    public class LevelItem
    {
        public int id { get; set; }
        public string nivelAcademico { get; set; }
        public string seccion { get; set; }
        public string grado { get; set; }
        public string tutor { get; set; }
        public string observaciones { get; set; }
        public string aulaId { get; set; }
        public string numero { get; set; }
        public string gmail { get; set; }
        public string capacidad { get; set; }
    }
}
