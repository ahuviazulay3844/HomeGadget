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
    /// Interaction logic for AddCatgory.xaml
    /// </summary>
    public partial class AddCatgory : Page
    {
        model.Category Cat;
        public AddCatgory()
        {
            InitializeComponent();
            Cat=new model.Category();   
        }

        private void Catgory1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(catgory.Text))
            {
                MessageBox.Show("הקש קטגוריה");
            }
            else 
            {

            Cat.Code=CategoryService.GetCode();
            Cat.Dedripition = catgory.Text;
            Cat.Status = true;
            CategoryService.AddCategory(Cat);
            CategoryService.UpdateCategoryt();
            MessageBox.Show("נוסף בהצלחה!!");
            NavigationService.Navigate(new KindofCatgory());
            }
        }
    }
}
