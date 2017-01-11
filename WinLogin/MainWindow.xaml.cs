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
        

        System.Windows.Media.Brush defaultColor_buttonStudent;  // Сохратить фон кнопки buttonStudent по умолчанию
        System.Windows.Media.Brush defaultColor_buttonTeacher;  // Сохратить фон кнопки buttonTeacher по умолчанию
        System.Windows.Media.Brush defaultColor_buttonAdmin;    // Сохратить фон кнопки buttonAdmin по умолчанию
                   
        enum selectionOfUser : byte   // Перечисление, которое фиксирует кем входит в программу пользователь 
        {
            Student = 1,     
            Teacher = 2,     
            Admin = 3       
        }
        selectionOfUser selUser = 0;

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
            passwordBoxTech.Visibility = Visibility.Collapsed;
            passwordBoxAdm.Visibility = Visibility.Collapsed;
            //buttonLogin.Visibility = Visibility.Collapsed;

            defaultColor_buttonStudent = buttonStudent.Background;
            defaultColor_buttonTeacher = buttonTeacher.Background;
            defaultColor_buttonAdmin = buttonAdmin.Background;
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
            passwordBoxTech.Visibility = Visibility.Collapsed;
            passwordBoxAdm.Visibility = Visibility.Collapsed;
            buttonLogin.Visibility = Visibility.Visible;

            buttonStudent.Background = new LinearGradientBrush(Colors.Gold, Colors.Azure, 90);
            buttonTeacher.Background = defaultColor_buttonTeacher;
            buttonAdmin.Background = defaultColor_buttonAdmin;

            ClearFields();
            selUser = selectionOfUser.Student;

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
            passwordBoxTech.Visibility = Visibility.Visible;
            passwordBoxAdm.Visibility = Visibility.Collapsed;
            buttonLogin.Visibility = Visibility.Visible;

            buttonStudent.Background = defaultColor_buttonStudent;
            buttonTeacher.Background = new LinearGradientBrush(Colors.Gold, Colors.Azure, 90); 
            buttonAdmin.Background = defaultColor_buttonAdmin;

            ClearFields();
            selUser = selectionOfUser.Teacher;

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
            passwordBoxTech.Visibility = Visibility.Collapsed;
            passwordBoxAdm.Visibility = Visibility.Visible;
            buttonLogin.Visibility = Visibility.Visible;

            buttonStudent.Background = defaultColor_buttonStudent;
            buttonTeacher.Background = defaultColor_buttonTeacher;
            buttonAdmin.Background = new LinearGradientBrush(Colors.Gold, Colors.Azure, 90);

            ClearFields();
            selUser = selectionOfUser.Admin;

        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            switch (selUser)
            {
                case selectionOfUser.Student:
                    //MessageBox.Show("Student");
                    WinStudent NewWinStudent = new WinStudent();                    
                    NewWinStudent.ShowDialog();

                    break;
                case selectionOfUser.Teacher:
                    //MessageBox.Show("Teacher");
                    WinTeacher NewWinTeacher = new WinTeacher();
                    NewWinTeacher.ShowDialog();

                    break;
                case selectionOfUser.Admin:
                    //MessageBox.Show("Admin");

                    

                    break;
                default:
                    MessageBox.Show("Вы не выбрали роль!");
                    break;
            }                         
        }

        private void ClearFields()  
        {
            textBoxFIO.Text = "";
            textBoxGroup.Text = "";
            textBoxLogin.Text = "";
            passwordBoxTech.Password = "";
            passwordBoxAdm.Password = "";
        }
    }
}
