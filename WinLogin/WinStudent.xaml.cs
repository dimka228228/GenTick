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

namespace WinLogin
{
    /// <summary>
    /// Логика взаимодействия для WinStudent.xaml
    /// </summary>
    public partial class WinStudent : Window
    {
        // Класс для хранения таблицы Questions
        public class QuestionsFromDb
        {
            public int Id { get; set; }
            public int Category { get; set; }
            public string Text { get; set; }
            public string Picture { get; set; }
        }

        // Список для сохранения єкземпляров QuestionsFromDb
        List<QuestionsFromDb> listQuestionsFromDb = new List<QuestionsFromDb>();
        // Список категорий, которые уже есть в билете
        List<int> listCatigory = new List<int>();
        
        // Выбранный предмет
        int current_id_subject = 0;
        // Количество вопросов в билете
        int countQuestion = 0;

        Random random = new Random();

        // Случайное число - индекс списка listQuestionsFromDb
        int randomIndex = 0;

        public WinStudent()
        {
            InitializeComponent();            
        }

        // Вывод вопросов и рисунков
        public void Generating()
        {
            try
            {
                    //MessageBox.Show("По этому предмету нет вопросов", "Предупреждение!");

                    // Очистка списка категорий билета 
                    listCatigory.Clear();
                    // Очистка списка  
                    listQuestions.Text = "Список вопросов";

                    // Формирование блоков в зависимости от количества вопросов
                    for (int i = 0; i < countQuestion; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                // Генерация случайного индекса в списке
                                RandomIndexlistQuestionsFromDb();
                                // Проверка на повторение категорий в билете
                                ChekCategory();
                                // Добавить в категорию в список
                                listCatigory.Add(GetFromlistQuestionsFromDb().Category);

                                stackPanelQuestion0.Margin = new Thickness(20, 20, 20, 10);
                                textBlockQuestion0.FontSize = 16;
                                textBlockQuestion0.TextWrapping = TextWrapping.Wrap;
                                textBlockQuestion0.Text = i + 1 + ". " + GetFromlistQuestionsFromDb().Text;

                                //MessageBox.Show(GetFromlistQuestionsFromDb().Text + " " + GetFromlistQuestionsFromDb().Picture, i.ToString());

                                if (GetFromlistQuestionsFromDb().Picture != "")
                                {
                                    imageQuestion0.Height = 150;
                                    imageQuestion0.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory +
                                        "\\images\\" + GetFromlistQuestionsFromDb().Picture, UriKind.Absolute));
                                }
                                // Удаление выбраного элемента из списка listQuestionsFromDb
                                listQuestionsFromDb.RemoveAt(randomIndex);
                                break;

                            case 1:
                                // Генерация случайного индекса в списке
                                RandomIndexlistQuestionsFromDb();
                                // Проверка на повторение категорий в билете
                                ChekCategory();
                                // Добавить в категорию в список
                                listCatigory.Add(GetFromlistQuestionsFromDb().Category);

                                stackPanelQuestion1.Margin = new Thickness(20, 20, 20, 10);
                                textBlockQuestion1.FontSize = 16;
                                textBlockQuestion1.Text = i + 1 + ". " + GetFromlistQuestionsFromDb().Text;

                                //MessageBox.Show(GetFromlistQuestionsFromDb().Text + " " + GetFromlistQuestionsFromDb().Picture, i.ToString());

                                if (GetFromlistQuestionsFromDb().Picture != "")
                                {
                                    imageQuestion1.Height = 150;
                                    imageQuestion1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory +
                                        "\\images\\" + GetFromlistQuestionsFromDb().Picture, UriKind.Absolute));
                                }
                                // Удаление выбраного элемента из списка listQuestionsFromDb
                                listQuestionsFromDb.RemoveAt(randomIndex);
                                break;

                            case 2:
                                // Генерация случайного индекса в списке
                                RandomIndexlistQuestionsFromDb();
                                // Проверка на повторение категорий в билете
                                ChekCategory();
                                // Добавить в категорию в список
                                listCatigory.Add(GetFromlistQuestionsFromDb().Category);

                                stackPanelQuestion2.Margin = new Thickness(20, 20, 20, 10);
                                textBlockQuestion2.FontSize = 16;
                                textBlockQuestion2.Text = i + 1 + ". " + GetFromlistQuestionsFromDb().Text;

                                //MessageBox.Show(GetFromlistQuestionsFromDb().Text + " " + GetFromlistQuestionsFromDb().Picture, i.ToString());

                                if (GetFromlistQuestionsFromDb().Picture != "")
                                {
                                    imageQuestion2.Height = 150;
                                    imageQuestion2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory +
                                        "\\images\\" + GetFromlistQuestionsFromDb().Picture, UriKind.Absolute));
                                }
                                // Удаление выбраного элемента из списка listQuestionsFromDb
                                listQuestionsFromDb.RemoveAt(randomIndex);
                                break;

                            case 3:
                                // Генерация случайного индекса в списке
                                RandomIndexlistQuestionsFromDb();
                                // Проверка на повторение категорий в билете
                                ChekCategory();
                                // Добавить в категорию в список
                                listCatigory.Add(GetFromlistQuestionsFromDb().Category);

                                stackPanelQuestion3.Margin = new Thickness(20, 20, 20, 10);
                                textBlockQuestion3.FontSize = 16;
                                textBlockQuestion3.Text = i + 1 + ". " + GetFromlistQuestionsFromDb().Text;

                                //MessageBox.Show(GetFromlistQuestionsFromDb().Text + " " + GetFromlistQuestionsFromDb().Picture, i.ToString());

                                if (GetFromlistQuestionsFromDb().Picture != "")
                                {
                                    imageQuestion3.Height = 150;
                                    imageQuestion3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory +
                                        "\\images\\" + GetFromlistQuestionsFromDb().Picture, UriKind.Absolute));
                                }
                                // Удаление выбраного элемента из списка listQuestionsFromDb
                                listQuestionsFromDb.RemoveAt(randomIndex);
                                break;
               
                    }
                }
                // Формирование футера
                stackPanelFooter.Margin = new Thickness(0, 0, 0, 10);          

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("По этому предмету нет вопросов", "Предупреждение!");
            }                
        }

        // Метод для выборки вопросов в билет
        private QuestionsFromDb GetFromlistQuestionsFromDb()
        {
            //MessageBox.Show(randomNumber.ToString(),"");
            QuestionsFromDb tempQuestionsFromDb = listQuestionsFromDb[randomIndex];
            return tempQuestionsFromDb;
        }
        private void RandomIndexlistQuestionsFromDb()
        {
            randomIndex = random.Next(0, listQuestionsFromDb.Count);
        }

        // Проверка на повторение категорий в билете
        private void ChekCategory()
        {
                for (int q = 0; q < listCatigory.Count; q++)
                {
                    if (GetFromlistQuestionsFromDb().Category == listCatigory[q])
                    {
                        RandomIndexlistQuestionsFromDb();
                        ChekCategory();
                    }
                }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Очистка всего списка вопросов с конкретного предмета
             listQuestionsFromDb.Clear();

                using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
                {
                // Выяснить количество вопросов в билете
                var countQues = from s in db.Subjects
                                where s.name_subject == (string)labelSubject.Content
                                select s;
                foreach (var tt in countQues)
                {
                    countQuestion = (int)tt.quantity_questions_of_tiket;
                    current_id_subject = tt.id_subject;
                }

                // Загрузка вопросов в класс и массив
                var questions = from q in db.Questions
                                where q.id_subject_ques == current_id_subject
                                select new { id = q.id_question, category = q.id_category_ques, text = q.text_quesation, picture = q.pathToPicture };
                foreach (var query in questions)
                {
                    QuestionsFromDb qFromDb = new QuestionsFromDb();
                    qFromDb.Id = query.id;
                    qFromDb.Category = query.category;
                    qFromDb.Text = query.text;
                    qFromDb.Picture = query.picture;

                    listQuestionsFromDb.Add(qFromDb);
                }
            }

            // Вывод вопросов и рисунков
            Generating();
        }
    }
}
