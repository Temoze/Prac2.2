using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private SampleDatabaseEntities context = new SampleDatabaseEntities();
        public Page1()
        {
            InitializeComponent();
            OrderDetailsDataGrid.ItemsSource = context.OrderDetails.ToList();
        }
        private void Page1Add_Click(object sender, RoutedEventArgs e)
        {
            int orderDetailID = int.Parse(OrderDetailsIDTextBox.Text);
            int orderID = int.Parse(OrderIDTextBox.Text);
            string productName = ProductNameTextBox.Text;
            int quantity = int.Parse(QuantityTextBox.Text);
            decimal price = decimal.Parse(PriceTextBox.Text);

            var newOrderDetail = new OrderDetails
            {
                OrderDetailID = orderDetailID,
                OrderID = orderID,
                ProductName = productName,
                Quantity = quantity,
                Price = price
            };

            using (var context = new SampleDatabaseEntities())
            {
                context.OrderDetails.Add(newOrderDetail);
                context.SaveChanges();
                OrderDetailsDataGrid.ItemsSource = context.OrderDetails.ToList();
            }


            OrderDetailsIDTextBox.Text = "";
            OrderIDTextBox.Text = "";
            QuantityTextBox.Text = "";
            ProductNameTextBox.Text = "";
            PriceTextBox.Text = "";

            MessageBox.Show("Данные добавлены");
        }

        private void Page1Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrderDetail = OrderDetailsDataGrid.SelectedItem as OrderDetails;

            if (selectedOrderDetail != null)
            {
                using (var context = new SampleDatabaseEntities())
                { 
                    context.OrderDetails.Attach(selectedOrderDetail);
                    context.OrderDetails.Remove(selectedOrderDetail);
                    context.SaveChanges();
                }

                OrderDetailsDataGrid.ItemsSource = context.OrderDetails.ToList();
                MessageBox.Show("Данные удалены");
            }
            else
            {
                MessageBox.Show("Выберите данные для удаления");
            }
        }


        private void Page1Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrderDetailsUpdate = OrderDetailsDataGrid.SelectedItem as OrderDetails;

            if (selectedOrderDetailsUpdate != null)
            {
                selectedOrderDetailsUpdate.OrderID = int.Parse(OrderIDUpdateTextBox.Text);
                selectedOrderDetailsUpdate.ProductName = ProductNameUpdateTextBox.Text;
                selectedOrderDetailsUpdate.Quantity = int.Parse(QuantityUpdateTextBox.Text);
                selectedOrderDetailsUpdate.Price = decimal.Parse(PriceUpdateTextBox.Text);

                using (var context = new SampleDatabaseEntities())
                {
                    var orderToUpdate = context.OrderDetails.FirstOrDefault(o => o.OrderID == selectedOrderDetailsUpdate.OrderID);
                    if (orderToUpdate != null)
                    {
                        orderToUpdate.OrderID = selectedOrderDetailsUpdate.OrderID;
                        orderToUpdate.ProductName = selectedOrderDetailsUpdate.ProductName;
                        orderToUpdate.Quantity = selectedOrderDetailsUpdate.Quantity;
                        orderToUpdate.Price = selectedOrderDetailsUpdate.Price;
                        context.SaveChanges();
                    }
                }
                OrderDetailsDataGrid.ItemsSource = context.OrderDetails.ToList();
            }
        }
    }
}
