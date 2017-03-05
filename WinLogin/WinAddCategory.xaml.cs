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
    /// Логика взаимодействия для WinAddCategory.xaml
    /// </summary>
    public partial class WinAddCategory : Window
    {
        // Свойства для получения значение имени категории и текущего предмета
        // с формы "Преподаватель" вкладки "Категории"
        public string NameCategory { get; set; }
        public int IdCurrentSubject { get; set; }

        public WinAddCategory()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(IdCurrentSubject.ToString(), "");

        }
    }
}
