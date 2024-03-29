using LogicLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.averagePages
{
    /// <summary>
    /// Lógica de interacción para averagesList.xaml
    /// </summary>
    public partial class averagesList : Page
    {
        AverageModel averageModel;

        public averagesList()
        {
            InitializeComponent();
            averageModel = new AverageModel();
            renderAverages(loadAverages());
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
                    nivel_academico = average[4]
                };
                dgPromedios.Items.Add(row);
            }
        }

        private void editPromedio(object sender, RoutedEventArgs e)
        {

        }

        private void removePromedio(object sender, RoutedEventArgs e)
        {

        }

        private void addPromedio(object sender, RoutedEventArgs e)
        {

        }
    }

    public class AverageItem
    {
        public int id;
        public string estudiante;
        public string tutor;
        public double promedio;
        public string nivel_academico;
    }
}
