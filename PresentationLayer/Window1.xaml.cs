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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            AdminModel adminModel = new AdminModel();
            foreach (var item in adminModel.getAdmins())
            {
                var row = new
                {
                    Id = item[0],
                    Usuario = item[1],
                    Gmail = item[2],
                    Contraseña = item[3]
                };
                tbDatos.Items.Add(row);
                //tbDatos.Items.Add(item[1]);
            }
        }
    }
}
