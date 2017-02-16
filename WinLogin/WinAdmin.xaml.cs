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
            dataGrid_Subjects.ItemsSource = db.Teachers.Local.ToBindingList();
            dataGrid_teachers_subjects.ItemsSource = db.Teachers.Local.ToBindingList();
            
///*
            var subjectList = from tech in db.Teachers
                              where tech.id_teacher > 2
                              select new 
                              {
                                  id_teacher = tech.id_teacher,
                                  name_tch = tech.name_tch
                              };
//*/
           // dataGrid_SubjectsOfTeachers.ItemsSource = subjectList;
           //foreach(var item in subjectList)
           //    dataGrid_Subjects.ItemsSource 
           //     MessageBox.Show(item.id_teacher, item.name_tch);
            
        }
        private void Save_tch()
        {
            try
            {
                db.SaveChanges();
                Load_tch();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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


/*
use ExamTicket_db
select Teachers.name_tch, Subjects.name_subject 
from Subjects, Teachers, SubjectOfTeacher
where Teachers.id_teacher = 3 and Teachers.id_teacher = id_teacher_sot and id_subject_sot = id_subject
*/