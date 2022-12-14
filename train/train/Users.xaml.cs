using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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

namespace train
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
            show_user.ItemsSource = z4_train_MihailovaEntities.getContext().Executor.Where(i => i.User.IsDeleted == false).ToList();
            var g = z4_train_MihailovaEntities.getContext().Executor.Where(i => i.User.IsDeleted == false);

            foreach (var a in g)
            {

                a.Task.GroupBy(e => e.Status).Select(e => new { Name = e.Key, Count = e.Count() });
            }
            show_user.ItemsSource = g.ToList();
        }

        private void show_user_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e == null) return;
            var a = ((FrameworkElement)e.OriginalSource).DataContext as Executor;
            var m = new Edit_pol();
            m.Init(a);            
            m.ShowDialog();
            show_user.Items.Refresh();
            
        }

        private void add_z_Click(object sender, RoutedEventArgs e)
        {
            var m = new Edit_pol();
            
            m.ShowDialog();
            show_user.ItemsSource = z4_train_MihailovaEntities.getContext().Executor.Where(g => g.User.IsDeleted == false).ToList();
            
            
        }

        private void rem_Click(object sender, RoutedEventArgs e)
        {
            var a = ((Button)sender).DataContext as Executor;
            a.User.IsDeleted = true;
            z4_train_MihailovaEntities.getContext().SaveChanges();
            show_user.ItemsSource = z4_train_MihailovaEntities.getContext().Executor.Where(g=>g.User.IsDeleted == false).ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void kef_Click(object sender, RoutedEventArgs e)
        {
            new koefficent().Show();
            Close();
        }
    }
}
