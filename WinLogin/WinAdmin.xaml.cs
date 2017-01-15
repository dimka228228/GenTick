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
        public WinAdmin()
        {
            InitializeComponent();

            textBox_id_teacher.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new ExamTicket_dbEntities();
            Load();
        }
        private void Load()
        {
            db.Teachers.Load();
            dataGrid_teachers.ItemsSource = db.Teachers.Local.ToBindingList();
        }
        private void Save()
        {
            db.SaveChanges();
            Load();
        }
        private void Add_Teacher_Click(object sender, RoutedEventArgs e)
        {
            
            Teachers tch = new Teachers();
            int id = db.Teachers.Select(t => t.id_teacher).Max() + 1;
            tch.id_teacher = id;
            db.Teachers.Add(tch);
            dataGrid_teachers.SelectedIndex = dataGrid_teachers.Items.Count - 1;

        }

        private void Save_Teacher_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Del_Teacher_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                db.Teachers.Remove((Teachers)dataGrid_teachers.SelectedItem);
                Save();
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
    }
}
