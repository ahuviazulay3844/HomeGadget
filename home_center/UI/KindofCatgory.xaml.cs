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
    /// Interaction logic for KindofCatgory.xaml
    /// </summary>
    public partial class KindofCatgory : Page
    {
       model.Category Category;
        public KindofCatgory()
        {
            InitializeComponent();
            Category=new Category();    
            lv6.ItemsSource = CategoryService.GetCategory();
        }

        private void addcatgory(object sender, RoutedEventArgs e)
        {
            AddCatgory addCatgory = new AddCatgory();
           NavigationService.Navigate(addCatgory);
        }

        private void deletecatgory(object sender, RoutedEventArgs e)
        {
            if (lv6.SelectedItems != null)
            {
                ((Category)lv6.SelectedItem).Status = false;
                CategoryService.UpdateCategoryt();
                lv6.ItemsSource = null;
                lv6.ItemsSource = CategoryService.GetCategory();
                MessageBox.Show("!המוצר נמחק בהצלחה");
            }
        }

        
    }
}
