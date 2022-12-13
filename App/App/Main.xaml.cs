using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User a;
            
            try
            {
                a = ekzam_MihailovaEntities.getContext().User.Where(b => b.Login == login.Text).First();
            }
            catch
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }
            if (pas.Text == a.Password)
            {
                MessageBox.Show("Авторизация успешна");
            }
            else
            {
                MessageBox.Show("Пароль не верен");
            }
        }
    }
}
