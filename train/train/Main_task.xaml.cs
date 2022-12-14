using System;
using System.CodeDom.Compiler;
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
using System.Windows.Threading;

namespace train
{
    /// <summary>
    /// Логика взаимодействия для Main_task.xaml
    /// </summary>
    public partial class Main_task : Window
    {
        int sec = 0;
        DispatcherTimer timer = new DispatcherTimer();
       
        public Main_task()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += block_time;
            timer.Start();
            stat.Items.Add("Все");
            stat.Items.Add("запланирована");
            stat.Items.Add("исполняется");
            stat.Items.Add("выполнена");
            stat.Items.Add("отменена");
            stat.SelectedIndex = 0;

            var m = Main.user.Manager;
            if (m == null)
            {
                til.Text = "Мои задачи";
                name_pole.Text = "Имя исполнителя:";
                name_isp.Visibility = Visibility.Visible;
                name_isp.Text = Main.user.ToString();
                
            }
            else
            {
                name_pole.Text = "Выбор исполнителя";
                vib_isp.Visibility = Visibility.Visible;
                vib_isp.ItemsSource = z4_train_MihailovaEntities.getContext().Executor.Where(a => a.ManagerID == Main.user.ID).Prepend(new Executor() { User = new User() { FirstName = "Все исполнители" } });
                vib_isp.SelectedIndex = 0;
                til.Text = "Управление задачами";
                Pol.Visibility = Visibility.Visible;
                vib_isp_SelectionChanged(null, null);
            }
            stat_SelectionChanged(null, null);
        }
        private void block_time(object sender, object e)
        {
            sec++;
            block.Text = "Время до блокировки: " + (30 - sec) + " минут";
            if (sec >= 30)
            {
                Button_Click(null, null);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Main().Show();
            timer.Stop();
            Close();

        }


        private void show_task_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e == null) return;
            var a = ((FrameworkElement)e.OriginalSource).DataContext as Task;
            var w = new Edit();
            w.Init(a);
            w.ShowDialog();
            show_task.Items.Refresh();

        }

        private void add_z_Click(object sender, RoutedEventArgs e)
        {

            var w = new Edit();
            w.ShowDialog();
            show_task.ItemsSource = z4_train_MihailovaEntities.getContext().Task.Where(a => a.Executor.ManagerID == Main.user.ID).ToList();
        }



        private void rem_Click(object sender, RoutedEventArgs e)
        {
            var a = ((Button)sender).DataContext as Task;
            z4_train_MihailovaEntities.getContext().Task.Remove(a);
            z4_train_MihailovaEntities.getContext().SaveChanges();
            (show_task.ItemsSource as ICollection<Task>).Remove(a);
            show_task.Items.Refresh();
        }

        private void Pol_Click(object sender, RoutedEventArgs e)
        {
            new Users().Show();
            Close();
        }


        private void updateTask()
        {
            var data = z4_train_MihailovaEntities.getContext().Task.Select(a => a);
            var m = Main.user.Manager;
            
            if (m == null)
            {
                if (stat.SelectedItem.ToString() != "Все")
                {
                    data = data.Where(a => a.ExecutorID == Main.user.ID && a.Status == stat.SelectedItem.ToString());
                }
                else
                {
                    data = data.Where(a => a.ExecutorID == Main.user.ID);
                }
            }
            else
            {
                if (vib_isp.SelectedItem == null) return;
                if (stat.SelectedItem.ToString() != "Все")
                {
                    data = data.Where(a => a.Executor.ManagerID == Main.user.ID && a.Status == stat.SelectedItem.ToString());
                }
                else
                {
                    data = data.Where(a => a.Executor.ManagerID == Main.user.ID);

                }
                if (vib_isp.SelectedItem.ToString() != "Все исполнители ")
                {
                    data = data.Where(a => a.ExecutorID == ((Executor)vib_isp.SelectedItem).ID); 
                }
               
            }
            
            show_task.ItemsSource = data.ToList();
        }

        private void stat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTask();

        }

        private void vib_isp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTask();
        }

        
    }
}
