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
        string current_name_category = "";
        string current_textQuestion = "";
        string current_picture = "";

        // Переменная для очистки comboBox_name_of_category без исключения.
        // 0 - при очистки комбобокса событие не вызывается.
        // 1 - при очистки комбобокса событие вызывается.
        int tempVar_comboBox_name_of_category = 1;

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
            try
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

                // Загрузка данных в dataGrid_Subjects_form_teacher
                Load_subjects();

                // Установка значений в comboBoxQuantityQuestions в диапазоне 1-4
                comboBoxQuantityQuestions.Items.Add(1);
                comboBoxQuantityQuestions.Items.Add(2);
                comboBoxQuantityQuestions.Items.Add(3);
                comboBoxQuantityQuestions.Items.Add(4);
              
                // Загрузка списка предметов в 2 комбобокса для вкладки "Категории" и "Вопросы"
                Load_subjects_to_comboBox();
            }
            catch (Exception e6)
            {
                MessageBox.Show(e6.Message, "Window_Loaded_Form_Teacher", MessageBoxButton.OK, MessageBoxImage.Error);
            }            

        }

        private void Load_subjects_to_comboBox()
        {
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
                    comboBox_name_of_subject_TabCatogory.Items.Add(s.name_subject);
                    comboBox_name_of_subject_questions.Items.Add(s.name_subject);

                    current_id_subject = s.id_subject;

                    // Загрузка в словарь id и названия предметов авторизированого преподавателя
                    dict_listSubjects.Add(s.name_subject, s.id_subject);
                }
            }
            comboBox_name_of_subject_TabCatogory.SelectedIndex = 0;
            comboBox_name_of_subject_questions.SelectedIndex = 0;
        }


        private void Load_subjects()
        {
            using (ExamTicket_dbEntities db7 = new ExamTicket_dbEntities())
            {
                var subjectsOfTeacher = from s in db7.Subjects.AsEnumerable()
                                        from s_of_t in db7.SubjectOfTeacher
                                        where current_id_teacher_gl == s_of_t.id_teacher_sot
                                        where s_of_t.id_subject_sot == s.id_subject
                                        select new { Id = s.id_subject, Name = s.name_subject, Quantity = s.quantity_questions_of_tiket };

                dataGrid_Subjects_form_teacher.ItemsSource = subjectsOfTeacher;
            }
        }

        private void ReLoad_categories_in_comboBox()
        {
            try
            {
                // Условие проверяет, есть ли вообще элементы в comboBox_name_of_category. Если есть, то их всех удаляет.
                if (comboBox_name_of_category.HasItems)
                {
                    tempVar_comboBox_name_of_category = 0;
                    //comboBox_name_of_category.ItemsSource = null;
                    //comboBox_name_of_category.SelectedIndex = -1;
                    comboBox_name_of_category.Items.Clear();
                    comboBox_name_of_category.Text = "";
                }                

                comboBox_name_of_category.Items.Add("      Все категории");

                using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                {
                    var var_name_of_category = from t in db.Categories
                                               from w in db.Subjects
                                               where comboBox_name_of_subject_questions.SelectedItem == w.name_subject
                                               //where temp_str == w.name_subject
                                               where t.id_subject_cat == w.id_subject
                                               select t;                    

                    foreach (var p in var_name_of_category)
                    {
                        comboBox_name_of_category.Items.Add(p.name_category);
                        //MessageBox.Show(comboBox_name_of_subject_questions.SelectedItem.ToString(), "");
                    }
                }
                comboBox_name_of_category.SelectedIndex = 0;
                tempVar_comboBox_name_of_category = 1;
                current_name_subject = comboBox_name_of_subject_questions.SelectedItem.ToString();
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "ReLoad_categories_in_comboBox", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Load_questions_to_dataGrid()
        {
            if(tempVar_comboBox_name_of_category == 1)
            {
                try
                {
                    // Получить коллекцию ключей
                    ICollection<string> keys = dict_listSubjects.Keys;

                    foreach (var j in keys)
                    {
                        if (j == comboBox_name_of_subject_questions.SelectedItem.ToString())
                        {
                            current_id_subject = dict_listSubjects[j];
                            break;
                        }
                    }
                    /////
                    //MessageBox.Show(comboBox_name_of_category.SelectedItem.ToString(), "");
                    //MessageBox.Show(current_id_subject.ToString(), "");

                    using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                    {
                        if (comboBox_name_of_category.SelectedItem.ToString() == "      Все категории")
                        {
                            var listQuestions = from q in db.Questions.AsEnumerable()
                                                where q.id_subject_ques == current_id_subject
                                                select new { text_question = q.text_quesation, pathToPicture = q.pathToPicture };

                            dataGridTeacher_list_questions.ItemsSource = listQuestions;
                        }
                        else
                        {
                            var listQuestions = from q in db.Questions.AsEnumerable()
                                                where q.id_subject_ques == current_id_subject
                                                where q.id_category_ques == current_id_category
                                                select new { text_question = q.text_quesation, pathToPicture = q.pathToPicture };

                            dataGridTeacher_list_questions.ItemsSource = listQuestions;
                        }
                    }
                }
                catch (Exception e5)
                {
                    MessageBox.Show(e5.Message, "Load_questions_to_dataGrid", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }


        private void dataGrid_Subjects_form_teacher_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Проверка на то, что клик мышки будет на колонке с данными
                if(dataGrid_Subjects_form_teacher.CurrentCell.Column != null)
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
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(e2.Message, "dataGrid_Subjects_form_teacher_MouseUp", MessageBoxButton.OK, MessageBoxImage.Error);
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

            ReLoad_categories_in_comboBox();
            Load_questions_to_dataGrid();
        }

        private void comboBox_name_of_subject_TabCatogory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCategoriesToDataGrid();
            textBox_name_catogory.Text = "";
        }

        public void LoadCategoriesToDataGrid()
        {
            try
            {
                // Получить коллекцию ключей
                ICollection<string> keys = dict_listSubjects.Keys;

                foreach (var j in keys)
                {
                    if (j == (string)comboBox_name_of_subject_TabCatogory.SelectedItem)
                    {
                        current_id_subject = dict_listSubjects[j];
                        break;
                    }                        
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
                MessageBox.Show(e2.Message, "LoadCategoriesToDataGrid", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dataGridCatogory_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Проверка на то, что клик мышки будет на колонке с данными
                if (dataGridCatogory.CurrentCell.Column != null)
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

            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "dataGridCatogory_MouseUp", MessageBoxButton.OK, MessageBoxImage.Error);
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
            try
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
                    }
                    db.SaveChanges();
                }
                LoadCategoriesToDataGrid();
                MessageBox.Show("Записи успешно обновлены", "Информация");
                var_status_operation_catigory = "";  
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "tabTeacher_Save_Category_Click", MessageBoxButton.OK, MessageBoxImage.Error);
            }    
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
                LoadCategoriesToDataGrid();
            }
        }

        private void comboBox_name_of_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_name_of_category.SelectedIndex != -1)
                current_id_category = GetIdCurrentCategory(comboBox_name_of_category.SelectedItem.ToString());
            //ReLoad_categories_in_comboBox();
            Load_questions_to_dataGrid();
        }

        private int GetIdCurrentCategory(string nameCategory)
        {
            int temp = 0;
            using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
            {
                var idCategory = from c in db.Categories
                                 where c.name_category == nameCategory
                                 select c;
                foreach(var aa in idCategory)
                {
                    temp = aa.id_category;
                }
            }
            return temp;
        }


        private void TextBoxFindQuestions_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
            {
                //Поиск
                if (textBoxFindQuestions.Text != "")
                {
                    //dataGridTeacher_list_questions.ItemsSource = db.Questions
                    //    .Where(t => t.text_quesation.Contains(textBoxFindQuestions.Text))
                    //    .ToList();
                    var searchText = from q in db.Questions.AsEnumerable()
                                     where q.id_subject_ques == current_id_subject
                                     //where q.id_category_ques == current_id_category
                                     where q.text_quesation.Contains(textBoxFindQuestions.Text)
                                     select new { text_question = q.text_quesation, pathToPicture = q.pathToPicture};

                    dataGridTeacher_list_questions.ItemsSource = searchText;
                }
                else
                    Load_questions_to_dataGrid();
                    //dataGridTeacher_list_questions.ItemsSource = db.Questions.Local.ToBindingList();
                //Поиск
            }


        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            WinAddEditQuestion winAddEditQuestion = new WinAddEditQuestion();

            // Эта логин преподавателя, который будет предаватся в окно "Преподаватель" для его идентификации
            winAddEditQuestion.TeacherLogin = this.TeacherLogin;

            // Передача текущего предмета и категории в форму
            winAddEditQuestion.Id_subject = current_id_subject;
            winAddEditQuestion.Name_subject = current_name_subject;
            winAddEditQuestion.Id_category = current_id_category;
            //winAddEditQuestion.Name_category = current_name_category;

            winAddEditQuestion.ShowDialog();

            Load_questions_to_dataGrid();

            // очистка image_Questions
            image_Questions.Source = null;
        }

        private void buttonDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(current_textQuestion == "")
            {
                MessageBox.Show("Сначала выберите вопрос", "Предупреждение");
            }
            else
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить вопрос: \n\n" + current_textQuestion, "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                    {
                        db.Questions.RemoveRange(db.Questions.Where(x => x.text_quesation == current_textQuestion));
                        db.SaveChanges();
                    }
                    Load_questions_to_dataGrid();
                }
            }
        }

        private void dataGridTeacher_list_questions_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Проверка на то, что клик мышки будет на колонке с данными
                if (dataGridTeacher_list_questions.CurrentCell.Column != null)
                {
                    int selectedColumn = dataGridTeacher_list_questions.CurrentCell.Column.DisplayIndex;
                    if (selectedColumn == 0)
                    {
                        var selectedCell = dataGridTeacher_list_questions.SelectedCells[selectedColumn];
                        var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                        var textQuestion = (cellContent as TextBlock).Text;
                        selectedColumn = 1;
                        selectedCell = dataGridTeacher_list_questions.SelectedCells[selectedColumn];
                        cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                        var pathToPicture = (cellContent as TextBlock).Text;
                        current_picture = pathToPicture;

                        //MessageBox.Show("id: " + textQuestion + " name: " + pathToPicture, "");
                        current_textQuestion = textQuestion;
                        //current_id_category = Convert.ToInt32(id_sub);
                        //textBox_name_catogory.Text = name_sub;
                        if (pathToPicture != "")
                            image_Questions.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + pathToPicture, UriKind.Absolute));
                        else
                            image_Questions.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\no_image.jpg", UriKind.Absolute));
                    }
                    else if (selectedColumn == 1)
                    {
                        var selectedCell = dataGridTeacher_list_questions.SelectedCells[selectedColumn];
                        var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                        var pathToPicture = (cellContent as TextBlock).Text;
                        current_picture = pathToPicture;
                        selectedColumn = 0;
                        selectedCell = dataGridTeacher_list_questions.SelectedCells[selectedColumn];
                        cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                        var textQuestion = (cellContent as TextBlock).Text;

                        //MessageBox.Show("textQuestion: " + textQuestion + " pathToPicture: " + pathToPicture, "");
                        current_textQuestion = textQuestion;
                        //current_id_category = Convert.ToInt32(id_sub);
                        //textBox_name_catogory.Text = name_sub;
                        if (pathToPicture != "")
                            image_Questions.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + pathToPicture, UriKind.Absolute));
                        else
                            image_Questions.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\no_image.jpg", UriKind.Absolute));
                    }
                }              
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message, "dataGridCatogory_MouseUp", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonEditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (current_textQuestion == "")
            {
                MessageBox.Show("Сначала выберите вопрос", "Предупреждение");
            }
            else
            {
                WinEditQuestion winEditQuestion = new WinEditQuestion();

                // Эта логин преподавателя, который будет передаватся в окно "Преподаватель" для его идентификации
                winEditQuestion.TeacherLogin = this.TeacherLogin;

                // Передача текущего предмета и категории в форму
                winEditQuestion.Id_subject = current_id_subject;
                winEditQuestion.Name_subject = current_name_subject;
                winEditQuestion.Id_category = current_id_category;
                winEditQuestion.Name_category = current_name_category;
                winEditQuestion.Сurrent_textQuestion = current_textQuestion;
                winEditQuestion.Сurrent_picture = current_picture;

                winEditQuestion.ShowDialog();
                Load_questions_to_dataGrid();

                // очистка image_Questions
                image_Questions.Source = null;
            }
        }

        private void buttonUpdateQuestion_Click(object sender, RoutedEventArgs e)
        {
            Load_questions_to_dataGrid();
        }
    }
}
