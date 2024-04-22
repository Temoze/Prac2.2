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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prac1._2
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        private SampleDatabaseEntities context = new SampleDatabaseEntities();
        public Page3()
        {
            InitializeComponent();
            UsersDataGrid.ItemsSource = context.Users.ToList();
        }
        private void Page3Add_Click(object sender, RoutedEventArgs e)
        {
            int userID = int.Parse(UserIDTextBox.Text);
            string userName = UserNameTextBox.Text;
            string email = EmailTextBox.Text;

            var newUser = new Users
            {
                UserID = userID,
                UserName = userName,
                Email = email
            };

            using (var context = new SampleDatabaseEntities())
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                UsersDataGrid.ItemsSource = context.Users.ToList();
            }

            UserIDTextBox.Text = "";
            UserNameTextBox.Text = "";
            EmailTextBox.Text = "";

            MessageBox.Show("Данные добавлены");
        }

        private void Page3Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersDataGrid.SelectedItem as Users;

            if (selectedUser != null)
            {
                using (var context = new SampleDatabaseEntities())
                {
                    context.Users.Attach(selectedUser);
                    context.Users.Remove(selectedUser);
                    context.SaveChanges();
                }

                UsersDataGrid.ItemsSource = context.Users.ToList();
                MessageBox.Show("Данные удалены");
            }
            else
            {
                MessageBox.Show("Выберите данные для удаления");
            }
        }

        private void Page3Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedUserUpdate = UsersDataGrid.SelectedItem as Users;

            if (selectedUserUpdate != null)
            {
                selectedUserUpdate.UserName = UserNameUpdateTextBox.Text;
                selectedUserUpdate.Email = EmailUpdateTextBox.Text;

                using (var context = new SampleDatabaseEntities())
                {
                    var userToUpdate = context.Users.FirstOrDefault(u => u.UserID == selectedUserUpdate.UserID);
                    if (userToUpdate != null)
                    {
                        userToUpdate.UserName = selectedUserUpdate.UserName;
                        userToUpdate.Email = selectedUserUpdate.Email;
                        context.SaveChanges();
                    }
                }
                UsersDataGrid.ItemsSource = context.Users.ToList();
            }
        }
    }
}
