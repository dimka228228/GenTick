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
        public WinStudent()
        {
            InitializeComponent();

            textBoxFIO.IsReadOnly = true;

            using (ExamTicket_dbEntities db = new ExamTicket_dbEntities())
            {
                var users = db.Groups;
                foreach (Groups u in users)
                    comboBoxGroup.Items.Add(u.name_group);
            }

        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();


            //MessageBox.Show("Hello World!");
        }
    }
}
