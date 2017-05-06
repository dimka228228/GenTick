using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для WinEditQuestion.xaml
    /// </summary>
    public partial class WinEditQuestion : Window
    {
        // Свойство для получения значение логина преподавателя с главной формы
        public string TeacherLogin { get; set; }
        // Свойство для для хранения id_subject текущего веделленого предмета
        public int Id_subject { get; set; }
        // Свойство для для хранения id_subject текущего выделенного предмета
        public string Name_subject { get; set; }
        // Свойство для для хранения id_subject текущего веделленого предмета
        public int Id_category { get; set; }
        // Свойство для для хранения id_subject текущего выделенного предмета
        public string Name_category { get; set; }
        // Свойство для хранения текущего текста вопроса (ОПЕРАЦИЯ РЕДАКТИРОВАНИЯ)
        public string Сurrent_textQuestion { get; set; }
        // Свойство для хранения текущего имени файла рисунка (ОПЕРАЦИЯ РЕДАКТИРОВАНИЯ)
        public string Сurrent_picture { get; set; }

        string old_textQuestion = "";
        string currentpathToPicture = "";

        public WinEditQuestion()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Сurrent_picture, "");

            // Запоминаем первоначальный текст вопроса (это нужно для удаления записи)
            old_textQuestion = Сurrent_textQuestion;

            // Загрузка предметов в labels
            label_name_of_subject_formWinAddEditQuestion.Content = Name_subject;

            // Загрузка текста вопроса
            listBox_formWinAddEditQuestion.Text = this.Сurrent_textQuestion;


            // Загрузка категорий  
            int index = 0;
            int selectedIndex = 0;
            using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
            {
                // Поиск имени и id категории по выбраному вопросу
                var cat = from c in db.Categories
                          from q in db.Questions
                          where q.text_quesation == Сurrent_textQuestion
                          where q.id_category_ques == c.id_category
                          select c;

                foreach (var p in cat)
                {
                    Name_category = p.name_category;
                    Id_category = p.id_category;
                }

                // Загрузка имен категорий в комбобокс
                var var_name_of_category = from t in db.Categories
                                           from w in db.Subjects
                                           where Name_subject == w.name_subject
                                           where t.id_subject_cat == w.id_subject
                                           select t;

                foreach (var p in var_name_of_category)
                {
                    comboBox_name_of_category_formWinAddEditQuestion.Items.Add(p.name_category);
                    if (Name_category == p.name_category)
                        selectedIndex = index;

                    index++;
                    //MessageBox.Show(comboBox_name_of_subject_questions.SelectedItem.ToString(), "");
                }
            }
            comboBox_name_of_category_formWinAddEditQuestion.SelectedIndex = selectedIndex;
            Name_category = comboBox_name_of_category_formWinAddEditQuestion.SelectedItem.ToString();

            // Загрузка текущего рисунка, если он есть
            if (Сurrent_picture != "")
                pictureOfQuestion.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + Сurrent_picture, UriKind.Absolute));
                
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_formWinAddEditQuestion.Text == "")
                MessageBox.Show("Вы не написали вопрос.", "Предупреждение!");
            else
            {
                try
                {
                    Questions ques = new Questions();
                    using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                    {

                        // Определение текущей категории вопроса
                        var name_of_category = from t in db.Categories
                                               where t.name_category == comboBox_name_of_category_formWinAddEditQuestion.SelectedItem
                                               select t;

                        foreach (var p in name_of_category)
                        {
                            Id_category = p.id_category;
                        }

                        // Удаляем запись с выбраным вопросом
                        db.Questions.RemoveRange(db.Questions.Where(x => x.text_quesation == old_textQuestion));
                        //db.SaveChanges();

                        // Присвоение полей для записи в БД
                        ques.id_subject_ques = Id_subject;
                        ques.id_category_ques = Id_category;
                        ques.text_quesation = listBox_formWinAddEditQuestion.Text;
                        ques.pathToPicture = currentpathToPicture;

                        db.Questions.Add(ques);
                        db.SaveChanges();

                        // Обновление dataGridTeacher_list_questions на форме WinTeacher
                        //WinTeacher.Load_questions_to_dataGrid();
                    }

                    MessageBox.Show("Записи успешно обновлены", "Информация");

                    // Закрытие окна
                    this.Close();
                }
                catch (Exception e2)
                {
                    MessageBox.Show(e2.Message, "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_LoadPicture_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".jpg"; // Default file extension
            dlg.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string fullFileName = dlg.FileName;
                //MessageBox.Show("", filename);

                // Копирование файла в папку images
                try
                {
                    FileInfo fileInf = new FileInfo(fullFileName);
                    string nameFile = fileInf.Name;
                    //string newPathFile = @"..\..\images\" + fileInf.Name;
                    string newPathFile = @"images\" + fileInf.Name;
                    //string newPathFile1 = "images/" + fileInf.Name;
                    //MessageBox.Show("newPathFile: " + newPathFile, "fileInf.Name: " + fileInf.Name);

                    if (fileInf.Exists)
                    {
                        fileInf.CopyTo(newPathFile, true);

                        //pictureOfQuestion.Source = new BitmapImage(new Uri(newPathFile1, UriKind.Relative));
                        pictureOfQuestion.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + fileInf.Name, UriKind.Absolute));
                        currentpathToPicture = fileInf.Name;                                     
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удается скопировать файл из-за исключения: " + ex.Message);
                }
            }
        }


    }
}