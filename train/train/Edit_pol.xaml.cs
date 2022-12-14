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
    /// Логика взаимодействия для Edit_pol.xaml
    /// </summary>
    public partial class Edit_pol : Window
    {
        public Executor executor;
        public Edit_pol()
        {
            InitializeComponent();
            greyd.ItemsSource = z4_train_MihailovaEntities.getContext().Executor.Select(e => e.Grade).Distinct().ToList();
            men.ItemsSource = z4_train_MihailovaEntities.getContext().Manager.Select(e => e.User).ToList();
            til.Text = "Добавление пользователя";
        }

        public void Init(Executor executor)
        {
            til.Text = "Редактирование пользователя";
            this.executor = executor;

            sec_name.Text = executor.User.FirstName;
            fir_name.Text = executor.User.MiddleName;
            patr.Text = executor.User.LastName;
            greyd.SelectedItem = executor.Grade;
            men.SelectedItem = executor.Manager.User;
            login.Text = executor.User.Login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (executor == null)
            {
                if (password.Text == "")
                {
                    MessageBox.Show("Заполните поле пароля");
                    return;
                }
                executor = new Executor();
                executor.User = new User();

            }


            executor.User.FirstName = sec_name.Text;
            executor.User.MiddleName = fir_name.Text;
            executor.User.LastName = patr.Text;
            executor.Grade = greyd.Text;
            executor.Manager = ((User)men.SelectedItem).Manager;
            executor.User.Login = login.Text;
            
            if (password.Text != "")
            { 
                executor.User.Password = password.Text; 
            }

            

            if (executor.ID < 1)
            {
                executor.User.IsDeleted = false;
                z4_train_MihailovaEntities.getContext().Executor.Add(executor);
                MessageBox.Show("Пользоветаль добавлена");
            }
            else
            {
                MessageBox.Show("Изменения сохранены");

            }




            z4_train_MihailovaEntities.getContext().SaveChanges();


            Close();
        }
    }
}
