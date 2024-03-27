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
    /// Lógica de interacción para FormListaEstudiante.xaml
    /// </summary>
    public partial class FormListaEstudiante : Page
    {
        public FormListaEstudiante()
        {
            InitializeComponent();
            StudentList();
            
        }

        public void StudentList()
        {
            dgEstudiantes.Items.Clear();
            StudentModel studentModel = new StudentModel();
            foreach (var item in studentModel.getStudents())
            {
                var row = new
                {
                    Id = item[0],
                    Primer_Nombre = item[1],
                    Segundo_Nombre = item[2],
                    Primer_Apellido = item[3],
                    Segundo_Apellido = item[4],
                    Telefono = item[5],
                    Celular = item[6],
                    Direccion = item[7],
                    Gmail = item[8],
                    Fecha_Nacimiento = item[9],
                    Observaciones = item[10],
                    nivel_academico = item[11],
                    grado_seccion = item[12]
                };

                dgEstudiantes.Items.Add(row);
            }
        }

        private void btnNuevoEstudiante_Click(object sender, RoutedEventArgs e)
        {
            FormNuevoEstudiante nuevoEstudiante = new FormNuevoEstudiante();
            nuevoEstudiante.Show();
        }

       

        private void btnEditarEstudiante_Click(object sender, RoutedEventArgs e)
        {
            //FormEditarEstudiante editarEstudiante = new FormEditarEstudiante();
            //editarEstudiante.Show();
            //this.Hide();
        }

        private void btnEliminarEstudiant_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void dgEstudiantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
}
