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

namespace WinLogin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            labelFIO.Visibility = Visibility.Collapsed;
            labelGroup.Visibility = Visibility.Collapsed;
            labelLogin.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelPasswordAdm.Visibility = Visibility.Collapsed;
            textBoxFIO.Visibility = Visibility.Collapsed;
            textBoxGroup.Visibility = Visibility.Collapsed;
            textBoxLogin.Visibility = Visibility.Collapsed;
            textBoxPasswordTech.Visibility = Visibility.Collapsed;
            textBoxPasswordAdm.Visibility = Visibility.Collapsed;
            buttonLogin.Visibility = Visibility.Collapsed;

        }

        private void buttonStudent_Click(object sender, RoutedEventArgs e)
        {

            labelFIO.Visibility = Visibility.Visible;
            labelGroup.Visibility = Visibility.Visible;
            labelLogin.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelPasswordAdm.Visibility = Visibility.Collapsed;
            textBoxFIO.Visibility = Visibility.Visible;
            textBoxGroup.Visibility = Visibility.Visible;
            textBoxLogin.Visibility = Visibility.Collapsed;
            textBoxPasswordTech.Visibility = Visibility.Collapsed;
            textBoxPasswordAdm.Visibility = Visibility.Collapsed;
            buttonLogin.Visibility = Visibility.Visible;

        }

        private void buttonTeacher_Click(object sender, RoutedEventArgs e)
        {
            labelFIO.Visibility = Visibility.Collapsed;
            labelGroup.Visibility = Visibility.Collapsed;
            labelLogin.Visibility = Visibility.Visible;
            labelPasswordTech.Visibility = Visibility.Visible;
            labelPasswordAdm.Visibility = Visibility.Collapsed;
            textBoxFIO.Visibility = Visibility.Collapsed;
            textBoxGroup.Visibility = Visibility.Collapsed;
            textBoxLogin.Visibility = Visibility.Visible;
            textBoxPasswordTech.Visibility = Visibility.Visible;
            textBoxPasswordAdm.Visibility = Visibility.Collapsed;
            buttonLogin.Visibility = Visibility.Visible;

        }

        private void buttonAdmin_Click(object sender, RoutedEventArgs e)
        {
            labelFIO.Visibility = Visibility.Collapsed;
            labelGroup.Visibility = Visibility.Collapsed;
            labelLogin.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelPasswordAdm.Visibility = Visibility.Visible;
            textBoxFIO.Visibility = Visibility.Collapsed;
            textBoxGroup.Visibility = Visibility.Collapsed;
            textBoxLogin.Visibility = Visibility.Collapsed;
            textBoxPasswordTech.Visibility = Visibility.Collapsed;
            textBoxPasswordAdm.Visibility = Visibility.Visible;
            buttonLogin.Visibility = Visibility.Visible;

        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("It is work!");
            


        }
    }
}
