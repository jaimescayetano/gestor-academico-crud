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

namespace PresentationLayer.classroomPages
{
    /// <summary>
    /// Lógica de interacción para ClassroomsList.xaml
    /// </summary>
    public partial class classroomsList : Page
    {
        ClassroomModel classroomModel;
        private int? recordId = null;

        public classroomsList()
        {
            InitializeComponent();
            classroomModel = new ClassroomModel();
            dgAulas.Items.Clear();
            loadClassrooms();
        }

        public void loadClassrooms()
        {
            dgAulas.Items.Clear();
            foreach (var classroom in classroomModel.getClassrooms())
            {
                dgAulas.Items.Add(new ClassroomItem { id = int.Parse(classroom[0]), numero = classroom[1], capacidad = classroom[2], observaciones = classroom[3] });
            }
        }

        private void addClassroom(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                string message = "";
                if (recordId == null)
                {
                    message = classroomModel.insertClassroom(int.Parse(tbNumero.Text), int.Parse(tbCapacidad.Text), tbObservaciones.Text);
                } else 
                {
                    List<string> classroom = new List<string>() { tbNumero.Text, tbCapacidad.Text, tbObservaciones.Text };
                    message = classroomModel.updateClassroom((int) recordId, classroom);
                    recordId = null;
                }
                MessageBox.Show(message, "Mensaje");
                this.loadClassrooms();
                clearFields();
            } else
            {
                MessageBox.Show("Ingrese todos los datos necesarios para registrar el aula.", "Validación de datos");
            }
        }

        public void clearFields()
        {
            tbNumero.Text = "";
            tbCapacidad.Text = "";
            tbObservaciones.Text = "";
        }

        public bool validateFields()
        {
            int capacidad;
            return int.TryParse(tbCapacidad.Text, out capacidad) && int.TryParse(tbNumero.Text, out capacidad);
        }

        private void editClassroom(object sender, RoutedEventArgs e)
        {
            if (dgAulas.SelectedItem != null)
            {
                ClassroomItem item = (ClassroomItem) dgAulas.SelectedItem;
                loadClassroom(item.id);
            }
            else
            {
                MessageBox.Show("Seleccione un aula");
            }
        }

        public void loadClassroom(int id)
        {
            var data = classroomModel.getClassroomById(id);
            recordId = int.Parse(data[0]);
            tbNumero.Text = data[1];
            tbCapacidad.Text = data[2];
            tbObservaciones.Text = data[3];
        }

        private void removeClassroom(object sender, RoutedEventArgs e)
        {
            if (dgAulas.SelectedItems != null)
            {
                foreach (ClassroomItem item in dgAulas.SelectedItems)
                {
                    string message = classroomModel.deleteClassroom(item.id);
                    MessageBox.Show(message, "Eliminación de datos");
                }
                this.loadClassrooms();
            } else
            {
                MessageBox.Show("Seleccione las aulas que desea eliminar", "Advertencia");
            }
        }
    }

    public class ClassroomItem
    {
        public int id { get; set; }
        public string numero { get; set; }
        public string capacidad { get; set; }
        public string observaciones { get; set; }
    }
}
