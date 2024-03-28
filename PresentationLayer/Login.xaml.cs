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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private AdminModel adminModel;
        public Login()
        {
            InitializeComponent();
            adminModel = new AdminModel();
        }

        // button that activates validation 
        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string gmail = txtCorreoElectronico.Text;
            string contraseña = txtContraseña.Password;

            if (gmail.Length > 0 && contraseña.Length > 0)
            {
                if (adminModel.ValidateUserCredentials(gmail, contraseña))
                {
                    this.Hide();
                    txtCorreoElectronico.Clear();
                    txtContraseña.Clear();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Datos ingresados son invalidos", "Validación de datos");
                    txtCorreoElectronico.Clear();
                    txtContraseña.Clear();
                }
            }
            else
            {
                MessageBox.Show("Ingresar los datos solicitados", "Validación de datos");
                txtCorreoElectronico.Clear();
                txtContraseña.Clear();
            }
        }
    }
}
