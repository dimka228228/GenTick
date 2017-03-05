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
    /// Логика взаимодействия для WinTeacher.xaml
    /// </summary>
    public partial class WinTeacher : Window
    {
        // Свойство для получения значение логина преподавателя с главной формы
        public string TeacherLogin { get; set; }

        // Переменные для хранения id_subject и name_subject текущего веделленого предмета в dataGrid_Subjects_form_teacher
        int current_id_subject = 0;
        int current_id_teacher_gl = 0;
        string current_name_subject = "";
        int current_id_category = 0;
        // Переменная для переключения значений добавить или редактировать название категории
        string var_status_operation_catigory = "";

        // Словарь для хранения id и названия предметов авторизированого преподавателя
        Dictionary<string, int> dict_listSubjects = new Dictionary<string,int>();
        
        public WinTeacher()
        {
            InitializeComponent();

            
        }

        private void Window_Loaded_Form_Teacher(object sender, RoutedEventArgs e)
        {
            
            // Загрузка ФИО в textBoxFIO
          
            using (ExamTicket_dbEntities db6 = new ExamTicket_dbEntities())
            {
                var var_FIO = from p in db6.Teachers
                              where p.login_tch == TeacherLogin
                              select p;
                foreach (var s in var_FIO)
                {
                    textBoxFIO.Content = s.name_tch;
                    current_id_teacher_gl = s.id_teacher;
                }                                                
            }
            //MessageBox.Show(textBoxFIO.Content.ToString(),current_id_teacher_gl.ToString());

            // Загрузка данных в dataGrid_Subjects_form_teacher
            Load_subjects();

            // Установка значений в comboBoxQuantityQuestions в диапазоне 1-4
            comboBoxQuantityQuestions.Items.Add(1);
            comboBoxQuantityQuestions.Items.Add(2);
            comboBoxQuantityQuestions.Items.Add(3);
            comboBoxQuantityQuestions.Items.Add(4);

            // Загрузка начальных значений для вкладки "Категории" и "Вопросы" 
            using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
            {
                var var_name_of_subject = from p in db.Subjects
                                          from t in db.Teachers
                                          from r in db.SubjectOfTeacher
                                          where t.login_tch == TeacherLogin
                                          where r.id_teacher_sot == t.id_teacher
                                          where p.id_subject == r.id_subject_sot
                                          select p;

                foreach (var s in var_name_of_subject)
                {
                    comboBox_name_of_subject_catogory.Items.Add(s.name_subject);
                    comboBox_name_of_subject_questions.Items.Add(s.name_subject);

                    current_id_subject = s.id_subject;

                    // Загрузка в словарь id и названия предметов авторизированого преподавателя
                    dict_listSubjects.Add(s.name_subject, s.id_subject);
                }

            }
            comboBox_name_of_subject_catogory.SelectedIndex = 0;
            comboBox_name_of_subject_questions.SelectedIndex = 0;

            // 



            // // // Загрузка начальных значений для вкладки "Вопросы" 

            // Загрузка категорий в зависимости от выбора предмета преподавателя
            Load_categories_in_comboBox();


            //comboBox_name_of_category
        }

        private void Load_subjects()
        {
            //using (ExamTicket_dbEntities db_t = new ExamTicket_dbEntities())
            //{
            //    db_t.Subjects.Load();
            //    dataGrid_Subjects_form_teacher.ItemsSource = db_t.Subjects.Local.ToBindingList();
            //}

            using (ExamTicket_dbEntities db7 = new ExamTicket_dbEntities())
            {
                //var subjectsOfTeacher = (db7.Subjects.AsEnumerable()
                //    .Where(h => h.quantity_questions_of_tiket == 2)
                //    .Select(g => new { Id = g.id_subject, Name = g.name_subject, Quantity = g.quantity_questions_of_tiket }));

                //dataGrid_Subjects_form_teacher.ItemsSource = subjectsOfTeacher;

                var subjectsOfTeacher = from s in db7.Subjects.AsEnumerable()
                                        from s_of_t in db7.SubjectOfTeacher
                                        where current_id_teacher_gl == s_of_t.id_teacher_sot
                                        where s_of_t.id_subject_sot == s.id_subject
                                        select new { Id = s.id_subject, Name = s.name_subject, Quantity = s.quantity_questions_of_tiket };

                dataGrid_Subjects_form_teacher.ItemsSource = subjectsOfTeacher;
            }
        }

        private void Load_categories_in_comboBox()
        {
            //string temp_str = comboBox_name_of_subject_questions.SelectedItem.ToString();
            comboBox_name_of_category.Items.Clear();
            comboBox_name_of_category.Items.Add("      Все категории");
            try
            {
                using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                {
                    var var_name_of_category = from t in db.Categories
                                               from w in db.Subjects
                                               where comboBox_name_of_subject_questions.SelectedItem == w.name_subject
                                               where t.id_subject_cat == w.id_subject
                                               select t;

                    foreach (var p in var_name_of_category)
                    {
                        comboBox_name_of_category.Items.Add(p.name_category);
                        //MessageBox.Show(comboBox_name_of_subject_questions.SelectedItem.ToString(), "");
                    }
                }
                comboBox_name_of_category.SelectedIndex = 0;
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void dataGrid_Subjects_form_teacher_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int selectedColumn = dataGrid_Subjects_form_teacher.CurrentCell.Column.DisplayIndex;
            if (selectedColumn == 0)
            {
                var selectedCell = dataGrid_Subjects_form_teacher.SelectedCells[selectedColumn];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var id_sub = (cellContent as TextBlock).Text;
                selectedColumn = 1;
                selectedCell = dataGrid_Subjects_form_teacher.SelectedCells[selectedColumn];
                cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var name_sub = (cellContent as TextBlock).Text;
                //
                //MessageBox.Show("id: " + id_sub + " name: " + name_sub, "");
                int id_sub_INT = Convert.ToInt32(id_sub);
                //Select_qq_of_tiket(id_sub_INT);                      
            }
            else if (selectedColumn == 1)
            {
                var selectedCell = dataGrid_Subjects_form_teacher.SelectedCells[selectedColumn];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var name_sub = (cellContent as TextBlock).Text;
                selectedColumn = 0;
                selectedCell = dataGrid_Subjects_form_teacher.SelectedCells[selectedColumn];
                cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var id_sub = (cellContent as TextBlock).Text;
                //
                //MessageBox.Show("id: " + id_sub + " name: " + name_sub, "");
                int id_sub_INT = Convert.ToInt32(id_sub);
                Select_qq_of_tiket(id_sub_INT); 
            }
            else if (selectedColumn == 2)
            {
                var selectedCell = dataGrid_Subjects_form_teacher.SelectedCells[selectedColumn];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var name_sub = (cellContent as TextBlock).Text;
                selectedColumn = 0;
                selectedCell = dataGrid_Subjects_form_teacher.SelectedCells[selectedColumn];
                cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                var id_sub = (cellContent as TextBlock).Text;
                //
                //MessageBox.Show("id: " + id_sub + " name: " + name_sub, "");
                int id_sub_INT = Convert.ToInt32(id_sub);
                Select_qq_of_tiket(id_sub_INT);
            }
        }

        public void Select_qq_of_tiket(int id_sub_INT)
        {
            try
            {
                using (ExamTicket_dbEntities db1 = new ExamTicket_dbEntities())
                {
                    var list1 = from a in db1.Subjects
                                where a.id_subject == id_sub_INT
                                select a;

                    foreach (var s in list1)
                    {
                        current_id_subject = s.id_subject;
                        current_name_subject = s.name_subject;

                        if (s.id_subject == id_sub_INT)
                            comboBoxQuantityQuestions.SelectedIndex = (int)s.quantity_questions_of_tiket - 1;
                    }
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для обновления количества вопросов билета для каждого предмета
        private void button_Save_New_Value_qq_of_tiket_Click(object sender, RoutedEventArgs e)
        {
            int new_value_comboBoxQuantityQuestions = Convert.ToInt32(comboBoxQuantityQuestions.SelectedValue.ToString());
            try
            {
                using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                {
                    var var2 = db.Subjects
                        .Where(c => c.id_subject == current_id_subject)
                        .FirstOrDefault();

                    var2.quantity_questions_of_tiket = new_value_comboBoxQuantityQuestions;

                    db.SaveChanges();
                    Load_subjects();
                }
                MessageBox.Show("Записи успешно обновлены", "Информация");                     
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void comboBox_name_of_subject_questions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_categories_in_comboBox();
        }

        private void comboBox_name_of_subject_catogory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCategories();

        }

        public void LoadCategories()
        {
            try
            {
                // Получить коллекцию ключей
                ICollection<string> keys = dict_listSubjects.Keys;

                foreach (var j in keys)
                {
                    if (j == comboBox_name_of_subject_catogory.SelectedItem)
                        current_id_subject = dict_listSubjects[j];
                }
                //MessageBox.Show(current_id_subject.ToString(), "");

                using (ExamTicket_dbEntities db8 = new ExamTicket_dbEntities())
                {
                    var listCatogories = from s in db8.Categories.AsEnumerable()
                                         where current_id_subject == s.id_subject_cat
                                         select new { id_cat_for_dg = s.id_category, name_cat_for_dg = s.name_category };

                    dataGridCatogory.ItemsSource = listCatogories;
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dataGridCatogory_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int selectedColumn = dataGridCatogory.CurrentCell.Column.DisplayIndex;
                if (selectedColumn == 0)
                {
                    var selectedCell = dataGridCatogory.SelectedCells[selectedColumn];
                    var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                    var id_sub = (cellContent as TextBlock).Text;
                    selectedColumn = 1;
                    selectedCell = dataGridCatogory.SelectedCells[selectedColumn];
                    cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                    var name_sub = (cellContent as TextBlock).Text;

                    //MessageBox.Show("id: " + id_sub + " name: " + name_sub, "");
                    current_id_category = Convert.ToInt32(id_sub);
                    textBox_name_catogory.Text = name_sub;
                }
                else if (selectedColumn == 1)
                {
                    var selectedCell = dataGridCatogory.SelectedCells[selectedColumn];
                    var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                    var name_sub = (cellContent as TextBlock).Text;
                    selectedColumn = 0;
                    selectedCell = dataGridCatogory.SelectedCells[selectedColumn];
                    cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                    var id_sub = (cellContent as TextBlock).Text;

                    //MessageBox.Show("id: " + id_sub + " name: " + name_sub, "");
                    current_id_category = Convert.ToInt32(id_sub);
                    textBox_name_catogory.Text = name_sub;
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tabTeacher_Add_Category_Click(object sender, RoutedEventArgs e)
        {

            //WinAddCategory winAddCategory = new WinAddCategory();
            //winAddCategory.IdCurrentSubject = current_id_subject;
            //winAddCategory.ShowDialog();

            var_status_operation_catigory = "add";
            textBox_name_catogory.Text = "";
            textBox_name_catogory.Focus();           
        }

        private void tabTeacher_Save_Category_Click(object sender, RoutedEventArgs e)
        {
            Categories cat = new Categories();
            using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
            {
                if (var_status_operation_catigory == "add")
                {
                    cat.name_category = textBox_name_catogory.Text;
                    cat.id_subject_cat = current_id_subject;
                    db.Categories.Add(cat);

                }
                else
                {
                    var category = db.Categories
                        .Where(c => c.id_category == current_id_category)
                        .FirstOrDefault();

                    category.name_category = textBox_name_catogory.Text;

                    //cat.id_category = current_id_category;
                    //cat.name_category = textBox_name_catogory.Text;
                    //cat.id_subject_cat = current_id_subject;                    
                }
                
                db.SaveChanges();
            }
            LoadCategories();
            MessageBox.Show("Записи успешно обновлены", "Информация");
            var_status_operation_catigory = "";      
        }

        private void tabTeacher_Delete_Category_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                {
                    db.Categories.RemoveRange(db.Categories.Where(x => x.id_category == current_id_category));
                    db.SaveChanges();
                }
                LoadCategories();
            }
        }
    }
}
