using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для WinAddCategory.xaml
    /// </summary>
    public partial class WinAddEditQuestion : Window
    {
        // Свойство для получения значение логина преподавателя с главной формы
        public string TeacherLogin { get; set; }

        // Свойство для для хранения id_subject текущего веделленого предмета
        public int Id_subject { get; set; }

        // Свойство для для хранения id_subject текущего выделенного предмета
        public string Name_subject { get; set; }

        // Свойство для для хранения id_subject текущего выделенного предмета
        public int Id_category { get; set; }

        // Свойство для для хранения id_subject текущего выделенного предмета
        public string Name_category { get; set; }

        // Свойство для для хранения текущего текста вопроса -- ДЛЯ РЕДАКТИРОВАНИЯ
        public string Сurrent_textQuestion { get; set; }

        // Свойство для для хранения имени файла текущего рисунка -- ДЛЯ РЕДАКТИРОВАНИЯ
        public string Current_picture { get; set; }

        string currentpathToPicture = "";
 


        public WinAddEditQuestion()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Загрузка предметов в labels
            label_name_of_subject_formWinAddEditQuestion.Content = Name_subject;


            // Загрузка категорий          
            using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
            {
                var var_name_of_category = from t in db.Categories
                                           from w in db.Subjects
                                           where Name_subject == w.name_subject
                                           where t.id_subject_cat == w.id_subject
                                           select t;

                foreach (var p in var_name_of_category)
                {
                    comboBox_name_of_category_formWinAddEditQuestion.Items.Add(p.name_category);
                    //MessageBox.Show(comboBox_name_of_subject_questions.SelectedItem.ToString(), "");
                }
            }
            comboBox_name_of_category_formWinAddEditQuestion.SelectedIndex = 0;
            Name_category = comboBox_name_of_category_formWinAddEditQuestion.SelectedItem.ToString();
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(listBox_formWinAddEditQuestion.Text == "")
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

                        // Присвоение полей для записи в БД
                        ques.id_subject_ques = Id_subject;
                        ques.id_category_ques = Id_category;
                        ques.text_quesation = listBox_formWinAddEditQuestion.Text;
                        ques.pathToPicture = currentpathToPicture;

                        db.Questions.Add(ques);
                        db.SaveChanges();
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
