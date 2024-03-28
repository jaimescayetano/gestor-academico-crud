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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Lógica de interacción para ListaAulas.xaml
    /// </summary>
    public partial class ListaAulas : Window
    {
        ClassroomModel classroomModel;

        public ListaAulas()
        {
            InitializeComponent();
            classroomModel = new ClassroomModel();
            dgAulas.Items.Clear();
            loadClassrooms();
        }

        public void loadClassrooms()
        {
            foreach (var classroom in classroomModel.getClassrooms())
            {
                dgAulas.Items.Add(new ClassroomItem { id = classroom[0], capacidad = classroom[1], observaciones = classroom[2] });
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            foreach (ClassroomItem item in dgAulas.SelectedItems)
            {
                string message = classroomModel.deleteClassroom(int.Parse(item.id));
                MessageBox.Show(message, "Eliminación de datos");
            }
        }
    }

    public class ClassroomItem
    {
        public string id { get; set; }
        public string capacidad { get; set; }
        public string observaciones { get; set; }
    }
}


