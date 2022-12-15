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

namespace train
{
    /// <summary>
    /// Логика взаимодействия для koefficent.xaml
    /// </summary>
    public partial class koefficent : Window
    {
        public koefficent()
        {
            InitializeComponent();
            vib_isp.ItemsSource = z4_train_MihailovaEntities.getContext().Executor.Where(a =>a.User.IsDeleted == false).Prepend(new Executor() { User = new User() { FirstName = "Все исполнители" } });
            vib_isp.SelectedIndex = 0;
            show_kef.ItemsSource = z4_train_MihailovaEntities.getContext().Executor.Where(g => g.User.IsDeleted == false).ToList();
        }

        private void vib_isp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Users().Show();
            Close();
        }
    }
}
