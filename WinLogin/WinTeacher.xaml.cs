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
        public WinTeacher()
        {
            InitializeComponent();

            ExamTicket_dbEntities be = new ExamTicket_dbEntities();
            be.Teachers.Load();
            dataGridTeacher.ItemsSource = be.Teachers.Local.ToBindingList();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
