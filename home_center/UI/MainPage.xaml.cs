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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Order order;
        public MainPage()
        {
            InitializeComponent();
           
        }

        private void DirectorClick(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu1.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
           if(((MenuItem)sender).Tag.ToString()=="1")
                 mainframe.Navigate(new DirectorCode(NavigationService.GetNavigationService(this)));

        }

        private void Ouritem(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu1.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            if (AllStatic.myOrder.Client!=null)
            {
                mainframe.Navigate(new Pageofitem(AllStatic.myClient));
            }
            else
            { 
            pagebeforecast pagebeforecast = new pagebeforecast();   
            mainframe.Navigate(pagebeforecast);
            }
        }

        private void about(object sender, RoutedEventArgs e)
        {

            foreach (MenuItem item in menu1.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            About about = new About();
            mainframe.Navigate(about);
        }

        private void CartofItem1(object sender, RoutedEventArgs e)
        { 
            if(AllStatic.myOrder.ItemOrder.Count==0)
            {
                MessageBox.Show("אין פריטים בסל");
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                
           
            foreach (MenuItem item in menu1.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            cart cart = new cart();
            mainframe.Navigate(cart);
            }
        }

        private void club(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu1.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            Club_card club = new Club_card();
            mainframe.Navigate(club);       }

        private void UpDate(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu1.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            if (AllStatic.myOrder.Client==null)
            {
                MessageBoxResult answeare = MessageBox.Show("אופסס המערכת לא זיהתה אותך ,האם ברצונך להרשם אלינו", "", MessageBoxButton.YesNo);
                if (answeare == MessageBoxResult.Yes)
                {
                    addclient addclient= new addclient();
                    //NavigationService.Navigate(addclient);
                    mainframe.Navigate(addclient);

                }
                else
                {
                    NavigationService.Navigate(new MainPage());
                }



            }
             else if (AllStatic.myClient != null)
                {
                    addclient2 addclient = new addclient2((Client)AllStatic.myClient);
                    mainframe.Navigate(addclient);
                }
        }

        private void opinion(object sender, RoutedEventArgs e)
        {
            message message3 = new message();
            mainframe.Navigate(message3);
        }
    }
}
