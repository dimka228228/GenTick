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
    /// 

    
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
            labelSubject.Visibility = Visibility.Collapsed;
            comboBoxSubject.Visibility = Visibility.Collapsed;
            labelLoginTeacher.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelPasswordAdm.Visibility = Visibility.Collapsed;
            textBoxFIO.Visibility = Visibility.Collapsed;
            comboBoxGroup.Visibility = Visibility.Collapsed;
            textBoxLoginTeacher.Visibility = Visibility.Collapsed;
            passwordBoxTeacher.Visibility = Visibility.Collapsed;
            passwordBoxAdmin.Visibility = Visibility.Collapsed;
            labelLoginAmd.Visibility = Visibility.Collapsed;
            textBoxLoginAdmin.Visibility = Visibility.Collapsed;

            defaultColor_buttonStudent = buttonStudent.Background;
            defaultColor_buttonTeacher = buttonTeacher.Background;
            defaultColor_buttonAdmin = buttonAdmin.Background;

            LoginsForDebugging();
            buttonLogin.Visibility = Visibility.Collapsed;

            try
            {
                using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                {
                    // Загрузка списка групп в комбобокс
                    var users = db.Groups;
                    foreach (Groups u in users)
                        comboBoxGroup.Items.Add(u.name_group);

                    // Загрузка списка предметов в комбобокс
                        var sub = db.Subjects;
                        foreach (Subjects u in sub)
                            comboBoxSubject.Items.Add(u.name_subject);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonStudent_Click(object sender, RoutedEventArgs e)
        {

            labelFIO.Visibility = Visibility.Visible;
            labelGroup.Visibility = Visibility.Visible;
            labelLoginTeacher.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelPasswordAdm.Visibility = Visibility.Collapsed;
            textBoxFIO.Visibility = Visibility.Visible;
            comboBoxGroup.Visibility = Visibility.Visible;
            textBoxLoginTeacher.Visibility = Visibility.Collapsed;
            passwordBoxTeacher.Visibility = Visibility.Collapsed;
            passwordBoxAdmin.Visibility = Visibility.Collapsed;
            buttonLogin.Visibility = Visibility.Visible;
            labelLoginAmd.Visibility = Visibility.Collapsed;
            textBoxLoginAdmin.Visibility = Visibility.Collapsed;
            labelSubject.Visibility = Visibility.Visible;
            comboBoxSubject.Visibility = Visibility.Visible;

            buttonStudent.Background = new LinearGradientBrush(Colors.Gold, Colors.Azure, 90);
            buttonTeacher.Background = defaultColor_buttonTeacher;            
            buttonAdmin.Background = defaultColor_buttonAdmin;            

            buttonStudent.Opacity = 1.0;
            buttonTeacher.Opacity = 0.5;
            buttonAdmin.Opacity = 0.5;
            buttonLogin.Visibility = Visibility.Visible;
            buttonLogin.Width = 140;
            buttonLogin.Content = "Сгенерировать билет";
            
            selUser = selectionOfUser.Student;
        }

        private void buttonTeacher_Click(object sender, RoutedEventArgs e)
        {
            labelFIO.Visibility = Visibility.Collapsed;
            labelGroup.Visibility = Visibility.Collapsed;
            labelLoginTeacher.Visibility = Visibility.Visible;
            labelPasswordTech.Visibility = Visibility.Visible;
            labelPasswordAdm.Visibility = Visibility.Collapsed;
            textBoxFIO.Visibility = Visibility.Collapsed;
            comboBoxGroup.Visibility = Visibility.Collapsed;
            textBoxLoginTeacher.Visibility = Visibility.Visible;
            passwordBoxTeacher.Visibility = Visibility.Visible;
            passwordBoxAdmin.Visibility = Visibility.Collapsed;
            buttonLogin.Visibility = Visibility.Visible;
            labelLoginAmd.Visibility = Visibility.Collapsed;
            textBoxLoginAdmin.Visibility = Visibility.Collapsed;
            labelSubject.Visibility = Visibility.Collapsed;
            comboBoxSubject.Visibility = Visibility.Collapsed;

            buttonStudent.Background = defaultColor_buttonStudent;
            buttonTeacher.Background = new LinearGradientBrush(Colors.Gold, Colors.Azure, 90); 
            buttonAdmin.Background = defaultColor_buttonAdmin;

            buttonStudent.Opacity = 0.5;
            buttonTeacher.Opacity = 1.0;
            buttonAdmin.Opacity = 0.5;

            buttonLogin.Visibility = Visibility.Visible;
            buttonLogin.Width = 102;
            buttonLogin.Content = "Вход";

            selUser = selectionOfUser.Teacher;

        }

        private void buttonAdmin_Click(object sender, RoutedEventArgs e)
        {
            labelFIO.Visibility = Visibility.Collapsed;
            labelGroup.Visibility = Visibility.Collapsed;
            labelLoginTeacher.Visibility = Visibility.Collapsed;
            labelPasswordTech.Visibility = Visibility.Collapsed;
            labelPasswordAdm.Visibility = Visibility.Visible;
            textBoxFIO.Visibility = Visibility.Collapsed;
            comboBoxGroup.Visibility = Visibility.Collapsed;
            textBoxLoginTeacher.Visibility = Visibility.Collapsed;
            passwordBoxTeacher.Visibility = Visibility.Collapsed;
            passwordBoxAdmin.Visibility = Visibility.Visible;
            buttonLogin.Visibility = Visibility.Visible;
            labelLoginAmd.Visibility = Visibility.Visible;
            textBoxLoginAdmin.Visibility = Visibility.Visible;
            labelSubject.Visibility = Visibility.Collapsed;
            comboBoxSubject.Visibility = Visibility.Collapsed;

            buttonStudent.Background = defaultColor_buttonStudent;
            buttonTeacher.Background = defaultColor_buttonTeacher;
            buttonAdmin.Background = new LinearGradientBrush(Colors.Gold, Colors.Azure, 90);

            buttonStudent.Opacity = 0.5;
            buttonTeacher.Opacity = 0.5;
            buttonAdmin.Opacity = 1.0;

            buttonLogin.Visibility = Visibility.Visible;
            buttonLogin.Width = 102;
            buttonLogin.Content = "Вход";

            selUser = selectionOfUser.Admin;

        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {

            // Проверка на введеные значений в поля каждой из ролей
                switch (selUser)
                {
                    case selectionOfUser.Student:
                        if (textBoxFIO.Text == "" || comboBoxGroup.SelectedIndex == -1 || comboBoxSubject.SelectedIndex == -1)
                            MessageBox.Show("Вы не заполнили и (или) не выбрали все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            WinStudent NewWinStudent = new WinStudent();
                            NewWinStudent.labelOutput_FIO.Content = this.textBoxFIO.Text;
                            NewWinStudent.labelOutput_Group.Content = (string)this.comboBoxGroup.SelectedValue;
                            NewWinStudent.labelSubject.Content = (string)this.comboBoxSubject.SelectedValue;
                            NewWinStudent.ShowDialog();
                        }
                        break;

                    case selectionOfUser.Teacher:
                        // Выборка из базы списка логинов и паролей преподавателей
                        //
                        using (ExamTicket_dbEntities db7 = new ExamTicket_dbEntities())
                        {
                            bool loginOk = false;

                            var loginPassword = from a in db7.Teachers
                                                where a.login_tch == textBoxLoginTeacher.Text
                                                where a.password_tch == passwordBoxTeacher.Password
                                                select a;
                            
                            foreach (var s in loginPassword)
                            {
                                if ((String.Compare(s.login_tch, textBoxLoginTeacher.Text) == 0) && (String.Compare(s.password_tch, passwordBoxTeacher.Password) == 0))
                                {
                                    WinTeacher NewWinTeacher = new WinTeacher();

                                    // Эта логин преподавателя, который будет предаватся в окно "Преподаватель" для его идентификации
                                    NewWinTeacher.TeacherLogin = textBoxLoginTeacher.Text;

                                    NewWinTeacher.ShowDialog();
                                    loginOk = true;
                                }
                                else
                                    loginOk = false;
                            }
                            if(loginOk == false)
                                MessageBox.Show("Не верный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case selectionOfUser.Admin:
                        if (textBoxLoginAdmin.Text == "admin" && passwordBoxAdmin.Password == "admin")
                        {
                            WinAdmin NewWinAdmin = new WinAdmin();
                            NewWinAdmin.ShowDialog();
                        }
                        else
                            MessageBox.Show("Не верный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    default:
                        MessageBox.Show("Вы не выбрали роль!", "Ошибка!");
                        break;
                }                                              
        }

        // Очищает логины и пароли при начатии кнопки других ролей
        private void ClearFields()  
        {
            textBoxFIO.Text = "";
            textBoxLoginTeacher.Text = "";
            passwordBoxTeacher.Password = "";
            passwordBoxAdmin.Password = "";
            textBoxLoginAdmin.Text = "";
        }

        // Только для тестирования: 
        // логины и пароли созданные для тестирования и отладки
        private void LoginsForDebugging()
        {
            textBoxFIO.Text = "Test_Student";
            comboBoxGroup.SelectedIndex = 0;
            comboBoxSubject.SelectedValue = "Химия";
            textBoxLoginTeacher.Text = "ivanov";
            passwordBoxTeacher.Password = "123";
            //textBoxLoginTeacher.Text = "petrov";
            //passwordBoxTeacher.Password = "345";
            textBoxLoginAdmin.Text = "admin";
            passwordBoxAdmin.Password = "admin";
        }
        private void comboBoxSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
