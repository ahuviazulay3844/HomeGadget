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
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        model.ItemOrder itemOrder; // Assuming this is the correct variable name

        public UserControl2(model.ItemOrder itemOrder)
        {
            InitializeComponent();

            // Check if quantity is greater than 0 before adding to cart and setting DataContext
            if (itemOrder.Amount > 0) // Assuming "Count" is the property for quantity
            {
                this.DataContext = itemOrder;
                this.itemOrder = itemOrder; // Store the parameter for later use
                img1.Source = MyPicture.GetImage(itemOrder.Item.NameImage);
            }
            else
            {
                // Display an error message to the user
                MessageBox.Show("לא ניתן להוסיף את המוצר לעגלה כיוון שהכמות היא 0.");
            }
        }

        private void cartdel(object sender, RoutedEventArgs e)
        {
            AllStatic.myOrder.ItemOrder.Remove(itemOrder);
            ((StackPanel)Parent).Children.Remove(this);
            if(AllStatic.myOrder.ItemOrder.Count==0)
            {
            NavigationService.GetNavigationService(new MainPage());
            }
        }
    }
}
