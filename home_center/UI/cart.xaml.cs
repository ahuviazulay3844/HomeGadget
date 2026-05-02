using HandyControl.Tools.Extension;
using home_center.BL;
using home_center.model;
using home_center.UI;
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
    /// Interaction logic for cart.xaml
    /// </summary>
    public partial class cart : Page
    {
        Item item;
        ItemOrder itemOrder;
        public cart()
        {
            InitializeComponent();
            itemOrder = new ItemOrder();
            item = new Item();
            Createcast();
        }

        private void Createcast()
        {
            // Filter items with amount greater than 0
            var filteredItems = AllStatic.myOrder.ItemOrder.Where(x => x.Amount > 0);

            // Add filtered items to the panel
            foreach (var itemOrder in filteredItems)
            {
                stp1.Children.Add(new UserControl2(itemOrder));
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!AllStatic.myClient.Member)
            {
                MessageBoxResult answeare = MessageBox.Show("האם ברצונך להצטרף למועדון שלנו", "", MessageBoxButton.YesNo);
                if (answeare == MessageBoxResult.Yes)
                {
                    Club_card club_Card = new Club_card();
                    NavigationService.Navigate(club_Card);
                }
                else
                {
                    payment payment = new payment();
                    NavigationService.Navigate(payment);
                }
            }
            else
            {
                payment payment = new payment();
                NavigationService.Navigate(payment);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AllStatic.myClient.Member==true)
            {
                payment payment = new payment();
                NavigationService.Navigate(payment);
            }
            else
            { 
                if(!AllStatic.myClient.Member)
                {
                    MessageBoxResult answeare = MessageBox.Show("האם ברצונך להצטרף למועדון שלנו", "", MessageBoxButton.YesNo);
                if (answeare == MessageBoxResult.Yes)
                {
                    Club_card club_Card = new Club_card();
                    NavigationService.Navigate(club_Card);
                }
                else               
                {
                    payment payment = new payment();
                    NavigationService.Navigate(payment);
                }
                }
            }

        }

        private void forcast(object sender, RoutedEventArgs e)
        {

            if( (AllStatic.myOrder.ItemOrder.Count==0) )
            {
                MessageBox.Show("אין פרטים בסל");
                MainPage mainPage = new MainPage();
                NavigationService.Navigate(mainPage);

            }
            else {
            if (AllStatic.myClient.Member==false)
            {
                MessageBoxResult answeare = MessageBox.Show("האם ברצונך להצטרף למועדון שלנו", "", MessageBoxButton.YesNo);
                if (answeare == MessageBoxResult.Yes)
                {
                    Club_card club_Card = new Club_card();
                    NavigationService.Navigate(club_Card);
                }
                else
                {
                        
                    payment payment = new payment();
                    NavigationService.Navigate(payment);
                }
            }
            else
            {
                payment payment = new payment();
                NavigationService.Navigate(payment);
            }
        }
        }

    }
}

 

