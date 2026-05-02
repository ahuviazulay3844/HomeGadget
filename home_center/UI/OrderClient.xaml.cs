using home_center.BL;
using home_center.model;
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
using Xamarin.Forms;

namespace home_center.UI
{
    /// <summary>
    /// Interaction logic for OrderClient.xaml
    /// </summary>
    public partial class OrderClient : System.Windows.Controls.Page
    {
        public OrderClient()
        {
            InitializeComponent();
            string s = AllStatic.myOrder.PriceEnd.ToString(); 
            OrderService.UpdateOrder();
            AllStatic.myOrder.DateOrder = DateTime.Now;
            lvo.ItemsSource = OrderService.GetOrder();
        }

        private void itemorder(object sender, RoutedEventArgs e)
        {
            if (lvo.SelectedItem != null)
            {
                AllStatic.myOrder = (Order)lvo.SelectedItem;
                ItemOrder1 itemOrder2 = new ItemOrder1(AllStatic.myOrder);
                NavigationService.Navigate(itemOrder2);
            }
           } 
    }
}
