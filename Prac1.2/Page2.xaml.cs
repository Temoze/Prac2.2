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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private SampleDatabaseEntities context = new SampleDatabaseEntities();
        public Page2()
        {
            InitializeComponent();
            OrdersDataGrid.ItemsSource = context.Orders.ToList();
        }
        private void Page2Add_Click(object sender, RoutedEventArgs e)
        {
            int orderID = int.Parse(OrderIDTextBox.Text);
            int usersID = int.Parse(UsersIDTextBox.Text);
            DateTime orderDate = DateTime.Parse(OrderDateTextBox.Text);
            decimal totalAmount = decimal.Parse(TotalAmountTextBox.Text);

            var newOrder = new Orders
            {
                OrderID = orderID,
                UserID = usersID,
                OrderDate = orderDate,
                TotalAmount = totalAmount
            };

            using (var context = new SampleDatabaseEntities())
            {
                context.Orders.Add(newOrder);
                context.SaveChanges();
                OrdersDataGrid.ItemsSource = context.Orders.ToList();
            }

            OrderIDTextBox.Text = "";
            UsersIDTextBox.Text = "";
            OrderDateTextBox.Text = "";
            TotalAmountTextBox.Text = "";

            MessageBox.Show("Данные добавлены");
        }

        private void Page2Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OrdersDataGrid.SelectedItem as Orders;

            if (selectedOrder != null)
            {
                using (var context = new SampleDatabaseEntities())
                {
                    context.Orders.Attach(selectedOrder);
                    context.Orders.Remove(selectedOrder);
                    context.SaveChanges();
                }

                OrdersDataGrid.ItemsSource = context.Orders.ToList();
                MessageBox.Show("Данные удалены");
            }
            else
            {
                MessageBox.Show("Выберите данные для удаления");
            }
        }

        private void Page2Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrderUpdate = OrdersDataGrid.SelectedItem as Orders;

            if (selectedOrderUpdate != null)
            {
                selectedOrderUpdate.UserID = int.Parse(UsersIDUpdateTextBox.Text);
                selectedOrderUpdate.OrderDate = DateTime.Parse(OrderDateUpdateTextBox.Text);
                selectedOrderUpdate.TotalAmount = decimal.Parse(TotalAmountUpdateTextBox.Text);

                using (var context = new SampleDatabaseEntities())
                {
                    var orderToUpdate = context.Orders.FirstOrDefault(o => o.OrderID == selectedOrderUpdate.OrderID);
                    if (orderToUpdate != null)
                    {
                        orderToUpdate.UserID = selectedOrderUpdate.UserID;
                        orderToUpdate.OrderDate = selectedOrderUpdate.OrderDate;
                        orderToUpdate.TotalAmount = selectedOrderUpdate.TotalAmount;
                        context.SaveChanges();
                    }
                }
                OrdersDataGrid.ItemsSource = context.Orders.ToList();
            }
        }
    }
}
