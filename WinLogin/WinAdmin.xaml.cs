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

namespace WinLogin
{
    /// <summary>
    /// Логика взаимодействия для WinAdmin.xaml
    /// </summary>
    public partial class WinAdmin : Window
    {
        public WinAdmin()
        {
            InitializeComponent();

            labelFIO_tech.Visibility = Visibility.Collapsed;
            labelLogin_teach.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelTeachers.Visibility = Visibility.Collapsed;
            labelAddTeacher.Visibility = Visibility.Collapsed;

            textBoxFIO.Visibility = Visibility.Collapsed;
            textBoxLogin.Visibility = Visibility.Collapsed;
            textBoxPassword.Visibility = Visibility.Collapsed;
            textBoxAddSubject.Visibility = Visibility.Collapsed;      
            comboBoxTechers.Visibility = Visibility.Collapsed;

        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            labelFIO_tech.Visibility = Visibility.Visible;
            labelLogin_teach.Visibility = Visibility.Visible;
            labelPasswordTech.Visibility = Visibility.Visible;
            labelTeachers.Visibility = Visibility.Visible;
            labelAddTeacher.Visibility = Visibility.Visible;

            textBoxFIO.Visibility = Visibility.Visible;
            textBoxLogin.Visibility = Visibility.Visible;
            textBoxPassword.Visibility = Visibility.Visible;
            textBoxAddSubject.Visibility = Visibility.Visible;
            comboBoxTechers.Visibility = Visibility.Collapsed;
        }

        private void buttonEditing_Click(object sender, RoutedEventArgs e)
        {
            labelFIO_tech.Visibility = Visibility.Collapsed;
            labelLogin_teach.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelTeachers.Visibility = Visibility.Collapsed;
            labelAddTeacher.Visibility = Visibility.Collapsed;

            textBoxFIO.Visibility = Visibility.Collapsed;
            textBoxLogin.Visibility = Visibility.Collapsed;
            textBoxPassword.Visibility = Visibility.Collapsed;
            textBoxAddSubject.Visibility = Visibility.Collapsed;
            comboBoxTechers.Visibility = Visibility.Collapsed;
        }

        private void buttonDeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            labelFIO_tech.Visibility = Visibility.Collapsed;
            labelLogin_teach.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelTeachers.Visibility = Visibility.Collapsed;
            labelAddTeacher.Visibility = Visibility.Collapsed;

            textBoxFIO.Visibility = Visibility.Collapsed;
            textBoxLogin.Visibility = Visibility.Collapsed;
            textBoxPassword.Visibility = Visibility.Collapsed;
            textBoxAddSubject.Visibility = Visibility.Collapsed;
            comboBoxTechers.Visibility = Visibility.Visible;
        }
    }
}
