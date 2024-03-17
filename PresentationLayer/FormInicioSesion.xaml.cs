using LogicLayer;
using PresentationLayer;
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

namespace Login.View
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        // button that activates validation 
        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e) { }

        private void btShowExample_Click(object sender, RoutedEventArgs e)
        {
            Window1 exampleWindow = new Window1();
            exampleWindow.Show();
        }
    }
}
