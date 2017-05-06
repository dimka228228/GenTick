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
        Subjects subj = new Subjects();

        
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

            // Загрузка dataGrid_teachers_subjects данными из базы
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
                try
                {
                    db.Teachers.Remove((Teachers)dataGrid_teachers.SelectedItem);
                    Save_tch();
                }
                catch (Exception e5)
                {
                    MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //Поиск
            if (Search_cat.Text != "")
            {
                dataGrid_teachers.ItemsSource = db.Teachers
                    .Where(t => t.name_tch.Contains(Search_cat.Text))
                    //.Where(t => t.login_tch.Contains(Search_cat.Text))
                    .ToList();
            }                
            else
                dataGrid_teachers.ItemsSource = db.Teachers.Local.ToBindingList();
            //Поиск
        }


        ///*******************************************************************************************************
        private void Load_sub()
        {
            try
            {
                db.Subjects.Load();
                //dataGrid_Subjects.ItemsSource = db.Teachers.Local.ToBindingList();
                dataGrid_Subjects.ItemsSource = db.Subjects.Local.ToBindingList();

                // Очистка listBox_all_subjects и загрузка данными из базы
                listBox_all_subjects.Items.Clear();
                using (ExamTicket_dbEntities db2 = new ExamTicket_dbEntities())
                {
                    var listOfAllSubjects = from p in db2.Subjects
                                            select p;
                    foreach (var s in listOfAllSubjects)
                        listBox_all_subjects.Items.Add(s.name_subject);
                }
            }
            catch (Exception e5)
            {
                MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Save_sub()
        {
            try
            {
                db.SaveChanges();
                Load_sub();
            }
            catch (Exception e5)
            {
                MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
                try
                {
                    db.Subjects.Remove((Subjects)dataGrid_Subjects.SelectedItem);
                    Save_sub();
                }
                catch (Exception e5)
                {
                    MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
            }
        }

        // Метод, который при нажатии мышки на dataGrid_teachers_subjects выводит
        // предметы, которые закреплены за выведеным преподавателнм
        private void DataGrid_teachers_subjects_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int selectedColumn = dataGrid_teachers_subjects.CurrentCell.Column.DisplayIndex;
                if (selectedColumn == 0)
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
                else if (selectedColumn == 1)
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
            }
            catch (Exception e5)
            {
                MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void QuarySubject(int int_var)
        {            
            try
            {
                listBox_subject_of_teacher.Items.Clear();
                using (ExamTicket_dbEntities db1 = new ExamTicket_dbEntities())
                {
                    var list = from a in db1.Subjects
                               from b in db1.Teachers
                               from c in db1.SubjectOfTeacher
                               where b.id_teacher == int_var
                               where b.id_teacher == c.id_teacher_sot
                               where c.id_subject_sot == a.id_subject
                               select a;

                    foreach (var s in list)
                        listBox_subject_of_teacher.Items.Add(s.name_subject);

                    // Ниже абсолютно рабочий код, он реализован просто через хранимую процедуру. 

                    ////IEnumerable<output_subject_of_teacher_Result> query = db1.output_subject_of_teacher(int_var);
                    ////foreach (output_subject_of_teacher_Result p in query)
                    ////    listBox_subject_of_teacher.Items.Add(p.name_subject);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Exception stored procedure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_ToSubjectOfTeacher(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listBox_all_subjects.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали предмет для добавления из списка предметов", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (listBox_subject_of_teacher.Items.IndexOf(listBox_all_subjects.SelectedItem) == -1)
                        listBox_subject_of_teacher.Items.Add(listBox_all_subjects.SelectedItem);
                }
                listBox_all_subjects.SelectedIndex = -1;

                // Сохранение изменений в базе
                Save_ToSubjectOfTeacher();
            }
            catch (Exception e5)
            {
                MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_ToSubjectOfTeacher(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listBox_subject_of_teacher.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали предмет для удаления из списка предметов", "Предупреждение", MessageBoxButton.OK);
                }
                else
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить выбраный предмет", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        listBox_subject_of_teacher.Items.Remove(listBox_subject_of_teacher.SelectedItem);
                }
                listBox_subject_of_teacher.SelectedIndex = -1;
                // Сохранение изменений в базе
                Save_ToSubjectOfTeacher();
            }
            catch (Exception e5)
            {
                MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_ToSubjectOfTeacher()
        {
            try
            {
                // Список для id предметов из listBox_subject_of_teacher
                List<int> list_id = new List<int>();
                int temp2 = 0;

                using (ExamTicket_dbEntities db3 = new ExamTicket_dbEntities())
                {//
                    for (int i = 0; i < listBox_subject_of_teacher.Items.Count; i++)
                    {
                        string temp = listBox_subject_of_teacher.Items[i].ToString();

                        var list_id_subject = from a in db3.Subjects
                                              where a.name_subject == temp
                                              select a;

                        foreach (var s in list_id_subject)
                        {
                            int temp1 = Convert.ToInt32(s.id_subject.ToString());
                            list_id.Add(temp1);
                        }
                    }
                }//

                // Это Id преподавателя которому нужно добавить предметы из listBox_subject_of_teacher
                temp2 = Convert.ToInt32(dataGrid_teachers_subjects_Id.Text);

                // Вызов функции для работы з БД
                RecordSubjectOfTeacher(list_id, temp2);

                // Очистка списка
                list_id.Clear();
            }
            catch (Exception e5)
            {
                MessageBox.Show(e5.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод, котрый записывает предметы из listBox_subject_of_teacher в базу данных
        public void RecordSubjectOfTeacher(List<int> list_id_teach, int id_teach)
        {
            try
            {
                list_id_teach.Sort();

                // Логическая переменная - "флажок" для удаления записей в БД 1 раз
                bool deleteRecord = false;

                using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                {
                    SubjectOfTeacher sot = new SubjectOfTeacher();

                    // Задача удалить все записи для преподавателя id_teach
                    if(list_id_teach.Count == 0)
                    {
                        db.SubjectOfTeacher.RemoveRange(db.SubjectOfTeacher.Where(x => x.id_teacher_sot == id_teach));
                        db.SaveChanges();            
                    }

                    // Проверка на то, есть ли у преподавателя id_teach вообще записи в таблице SubjectOfTeacher
                    var id_teach_sot = from a in db.SubjectOfTeacher
                                       where a.id_teacher_sot == id_teach
                                       //orderby a.id_subject_sot ascending
                                       select a;

                        foreach (var s in id_teach_sot)
                        {
                            // Сначала удалим все существующие записи данного преподавателя,
                            // а потом создадим после следующем блоке операторов
                            if(deleteRecord == false)
                            {
                                db.SubjectOfTeacher.RemoveRange(db.SubjectOfTeacher.Where(x => x.id_teacher_sot == id_teach));
                                db.SaveChanges();
                                deleteRecord = true;
                            }                            
                        }
                    
                    // 1. Создание записей, у тех преподавателей, у которых записей не было изначально.
                    // 2. Создание записей, у тех преподавателей, у которых они были изначально, но были удалены
                    // в предедущем блоке кода.

                        for(int i = 0; i < list_id_teach.Count; i++)
                        {
                            sot.id_teacher_sot = id_teach;
                            sot.id_subject_sot = list_id_teach[i];
                            sot.active = true;
                            db.SubjectOfTeacher.Add(sot);
                            db.SaveChanges();
                        }                       
                    MessageBox.Show("Записи успешно обновлены", "Информация");                     
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Exception stored procedure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}





