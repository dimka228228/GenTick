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
using System.Data.SqlClient;

namespace WinLogin
{
    /// <summary>
    /// Логика взаимодействия для WinAdmin.xaml
    /// </summary>
    public class SubjectOfTeacherFromDB
    {
        public int id { get; set; }
        public string name { get; set; }
        // другие необходимые свойства
    }
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
            textBox_subject.Focus();
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

        // Метод, который при нажатии мышки на dataGrid_teachers_subjects выводит
        // премтеты, которые закреплены за выведеным преподавателнм
        private void DataGrid_teachers_subjects_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int selectedColumn = dataGrid_teachers_subjects.CurrentCell.Column.DisplayIndex;
            if(selectedColumn == 0)
            {
                var selectedCell = dataGrid_teachers_subjects.SelectedCells[selectedColumn];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var id_sub = (cellContent as TextBlock).Text;
                selectedColumn = 1;
                selectedCell = dataGrid_teachers_subjects.SelectedCells[selectedColumn];
                cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var name_sub = (cellContent as TextBlock).Text;

                //MessageBox.Show("id: " + id_sub + " name: " + name_sub, "");
                QuarySubject(Convert.ToInt32(id_sub));
            }
            else if(selectedColumn == 1)
            {
                var selectedCell = dataGrid_teachers_subjects.SelectedCells[selectedColumn];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var name_sub = (cellContent as TextBlock).Text;
                selectedColumn = 0;
                selectedCell = dataGrid_teachers_subjects.SelectedCells[selectedColumn];
                cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var id_sub = (cellContent as TextBlock).Text;

                //MessageBox.Show("id: " + id_sub + " name: " + name_sub, "");
                QuarySubject(Convert.ToInt32(id_sub));
            }

            // Загрузка данными listBox_subject_of_teacher из БД

 
            
/*            {
                var query = (from p1 in db1.Teachers
                             join p2 in db1.SubjectOfTeacher

                            where p.id_teacher = 
            }
*/

            //db = new ExamTicket_dbEntities();

            //var subjectList = from tech in db.Teachers
            //                  where db.SubjectOfTeacher.id
            //                  select new
            //                  {
            //                      id_teacher = tech.id_teacher,
            //                      name_tch = tech.name_tch
            //                  };


        }
        public void QuarySubject(int int_var)
        {
            listBox_subject_of_teacher.Items.Clear();
            try
            {
                using (ExamTicket_dbEntities db1 = new ExamTicket_dbEntities())
                {
                    //int var_id = 3;

                    //var studentGrades = db1.output_subject_of_teacher("var_id");
                    //foreach (var student in studentGrades) {
                    //    MessageBox.Show(student, "");

                    //System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@var_id", 3);
                    //var query = db1.Database.SqlQuery<SubjectOfTeacherFromDB>("output_subject_of_teacher @var_id", param);
                    //foreach (var p in query)
                    //    //listBox_subject_of_teacher.Items.Add(p.Name);
                    //    MessageBox.Show(p.name, p.id.ToString());
                    ////Console.WriteLine("{0} - {1}", p.Name, p.Price);

                    IEnumerable<output_subject_of_teacher_Result> query = db1.output_subject_of_teacher(int_var);
                    foreach (output_subject_of_teacher_Result p in query)
                        listBox_subject_of_teacher.Items.Add(p.name_subject);


                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Exception stored procedure", MessageBoxButton.OK, MessageBoxImage.Error);
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