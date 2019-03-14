using HelixToolkit.Wpf;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TransformasiGGWP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void translation_bt_Click(object sender, RoutedEventArgs e)
        {
            Window1 trans = new Window1();
            trans.Show();
        }

        private void scaling_Click(object sender, RoutedEventArgs e)
        {
            Scaling scale = new Scaling();
            scale.Show();
        }

        private void rotating_Click(object sender, RoutedEventArgs e)
        {
            Rotating rotate = new Rotating();
            rotate.Show();
        }

        private void sharing_Click(object sender, RoutedEventArgs e)
        {
            Shearing Shear = new Shearing();
            Shear.Show();
        }
    }
}
