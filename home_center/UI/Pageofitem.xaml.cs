using HandyControl.Tools.Extension;
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
    /// Interaction logic for Pageofitem.xaml
    /// </summary>
    public partial class Pageofitem : Page
    {
        private Order order;

        public Pageofitem(model.Client client)
        {
            InitializeComponent();
            AllStatic.myOrder.Client = client;
            CreateButton();
            CreateProduct();
        }

        private void CreateButton()
        {
            Button btn;
            foreach (Category it in BL.CategoryService.GetCategory())
            {
                btn = new Button();
                stk1.Children.Add(btn);
                btn.Content = it.Dedripition;
                btn.DataContext= it;
                btn.Click += Btn_Click;
                btn.Foreground = Brushes.LightSeaGreen;
                btn.FontSize = 15;  
                btn.BorderBrush = Brushes.Transparent;
                btn.FontWeight = FontWeights.Bold;  
                btn.Background = Brushes.White; 
                btn.HorizontalAlignment = HorizontalAlignment.Center;   
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            WrpofCart.Children.Clear();
            model.Category cat = (sender as Button).DataContext as Category;
            BL.ItemService.GetItem().Where(x => x.Category == cat.Code).ToList().ForEach(x => WrpofCart.Children.Add(new UserControl1(x)));
        }

        private void CreateProduct()
        {
            BL.ItemService.GetItem().Where(x =>x.Status==true).ToList().ForEach(x => WrpofCart.Children.Add(new UserControl1(x)) ); 
        }

        private void tocast_Click(object sender, RoutedEventArgs e)
        {
            if (AllStatic.myOrder.ItemOrder.Count == 0 || (AllStatic.myOrder.ItemOrder.Count>0 && AllStatic.myOrder.ItemOrder.ToList().Sum(y=>y.Amount)==0))
            {
                MessageBox.Show("אין פרטים בסל");
            }
            else
            {
                
           

            cart cart = new cart();
            NavigationService.Navigate(cart);
            }
        }

        private void CategoryOfCatt(object sender, RoutedEventArgs e)
        {

            WrpofCart.Children.Clear();
            model.Category cat = (sender as Button).DataContext as Category;
            BL.ItemService.GetItem().Where(x => x.Category == cat.Code).ToList().ForEach(x => WrpofCart.Children.Add(new UserControl1(x)));
        }
    }
}
