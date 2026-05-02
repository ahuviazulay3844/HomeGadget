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
    /// Interaction logic for Kindofitem.xaml
    /// </summary>
    public partial class Kindofitem : Page
    {
        Item item;
        public Kindofitem()
        {
            InitializeComponent();
            ItemOrder itemInorder = new ItemOrder();
            item = new Item();
            item.Status = true;
            lv3.ItemsSource = ItemService.GetItem().Where(x=>x.Status==true).ToList();
        }

        private void additem(object sender, RoutedEventArgs e)
        {
            Additem additem = new Additem(ItemService.GetCode().ToString());
            NavigationService.Navigate(additem);
        }

        private void updateitem(object sender, RoutedEventArgs e)
        {
            if (lv3.SelectedItems != null)
            {
                Additem additem = new Additem((Item)lv3.SelectedItem);
                NavigationService.Navigate(additem);
            }
        }
        private void delete(object sender, RoutedEventArgs e)
        {

            if (lv3.SelectedItems != null)
            {
                ((Item)lv3.SelectedItem).Status = false;
                ItemService.UpdateItem();
                lv3.ItemsSource = null;
                lv3.ItemsSource = ItemService.GetItem();
                item.Status = false;//הפיכה למוצר לא קיים
                ItemService.UpdateItem();
                MessageBox.Show("!המוצר נמחק בהצלחה");
                NavigationService.Navigate(new Kindofitem());
            }
        }
        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            if (lv3.SelectedItems != null)
            {
                Additem c = new Additem((Item)lv3.SelectedItem);
                NavigationService.Navigate(c);
            }
        }
    }
}