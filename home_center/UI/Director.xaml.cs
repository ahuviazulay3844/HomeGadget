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
using static HandyControl.Tools.Interop.InteropValues;

namespace home_center.UI
{
    /// <summary>
    /// Interaction logic for director.xaml
    /// </summary>
    public partial class director : Page
    {
        public director()
        {
            InitializeComponent();
        }

        private void Canchcode(object sender, RoutedEventArgs e)
        {
            {
                foreach (MenuItem item in menu2.Items)
                {
                    item.BorderBrush = Brushes.White;
                }
              ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
                myframe2.Navigate(new ChageCode());

            }

        }

        private void clientofdirector(object sender, RoutedEventArgs e)
        {
            {
                foreach (MenuItem item in menu2.Items)
                {
                    item.BorderBrush = Brushes.White;
                }
              ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
               myframe2.Navigate(new ClientOfDirector());
            }
        }

        private void kindofitem(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu2.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            Kindofitem kindofitem = new Kindofitem(); 
            myframe2.Navigate(kindofitem); 
        }

        private void Order(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu2.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            OrderClient orderclient = new OrderClient();
            myframe2.Navigate(orderclient);
        }

        private void Opinion(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu2.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            message message=new message();
           myframe2.Navigate(message);        
        }

        private void catgory(object sender, RoutedEventArgs e)
        {
         AddCatgory addCatgory = new AddCatgory();  
            myframe2.Navigate(addCatgory);
        }

        private void kindofcatgory(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menu2.Items)
            {
                item.BorderBrush = Brushes.White;
            }
            ((MenuItem)sender).BorderBrush = Brushes.LightSeaGreen;
            KindofCatgory kindofCatgory= new KindofCatgory();
            myframe2.Navigate(kindofCatgory);
        }

        private void HomePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}