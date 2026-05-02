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

namespace home_center.UI
{
    /// <summary>
    /// Interaction logic for ItemOrder1.xaml
    /// </summary>
    public partial class ItemOrder1 : Page
    {
       
        model.Item item;
        public ItemOrder1(model.Order order)
        {
            InitializeComponent();
            DataContext = order.ItemOrder;            

            //lvo2.ItemsSource = (lvo2.SelectedItem)(ItemOrder);
            //lvo2.ItemsSource = lvo2.SelectedItem(OrderService.GetOrder());
            lvo2.ItemsSource =order.ItemOrder.ToList();
        }
    }
}
