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
using System.Data.Entity;

namespace WinLogin
{
    /// <summary>
    /// Логика взаимодействия для WinAdmin.xaml
    /// </summary>
    public partial class WinAdmin : Window
    {
        ExamTicket_dbEntities db;

        Teachers tch = new Teachers();

        SubjectOfTeacher sot = new SubjectOfTeacher();
        public WinAdmin()
        {
            InitializeComponent();

            textBox_id_teacher.IsReadOnly = true;
            textBox_id_subject.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new ExamTicket_dbEntities();
            Load_tch();
            Load_sub();
        }
        private void Load_tch()
        {
            db.Teachers.Load();
            dataGrid_teachers.ItemsSource = db.Teachers.Local.ToBindingList();



            //if (tch.id_teacher == sot.id_teacher_sot)
            //{
            //    listBox_Subjects_Of_Techers.Items[0] = sot.id_subject_sot;
            //}
                
        }
        private void Save_tch()
        {
            db.SaveChanges();
            Load_tch();
        }
        private void Add_Teacher_Click(object sender, RoutedEventArgs e)
        {
            
            //Teachers tch = new Teachers();
            int id = db.Teachers.Select(t => t.id_teacher).Max() + 1;
            tch.id_teacher = id;
            db.Teachers.Add(tch);
            dataGrid_teachers.SelectedIndex = dataGrid_teachers.Items.Count - 1;

        }

        private void Save_Teacher_Click(object sender, RoutedEventArgs e)
        {
            Save_tch();
        }

        private void Del_Teacher_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                db.Teachers.Remove((Teachers)dataGrid_teachers.SelectedItem);
                Save_tch();
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //Поиск
            if (Search_cat.Text != "")
            {
                dataGrid_teachers.ItemsSource = db.Teachers.Where(t => t.name_tch.Contains(Search_cat.Text)).ToList();
                //dataGrid_teachers.ItemsSource = db.Teachers.Where(t => t.login_tch.Contains(Search_cat.Text)).ToList();
                //dataGrid_teachers.ItemsSource = db.Teachers.Where(t => t.password_tch.Contains(Search_cat.Text)).ToList();
            }                
            else
                dataGrid_teachers.ItemsSource = db.Teachers.Local.ToBindingList();
            //Поиск
        }


        ///*******************************************************************************************************
        private void Load_sub()
        {
            db.Subjects.Load();
            dataGrid_Subjects.ItemsSource = db.Subjects.Local.ToBindingList();
        }
        private void Save_sub()
        {
            db.SaveChanges();
            Load_sub();
        }
        
        private void Add_Subject_Click(object sender, RoutedEventArgs e)
        {
            Subjects sub = new Subjects();
            int id = db.Subjects.Select(t => t.id_subject).Max() + 1;
            sub.id_subject = id;
            db.Subjects.Add(sub);
            dataGrid_Subjects.SelectedIndex = dataGrid_Subjects.Items.Count - 1;
        }

        private void Save_Subject_Click(object sender, RoutedEventArgs e)
        {
            Save_sub();
        }

        private void Del_Subject_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                db.Subjects.Remove((Subjects)dataGrid_Subjects.SelectedItem);
                Save_sub();
            }
        }
    }
}
