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
    }
}
