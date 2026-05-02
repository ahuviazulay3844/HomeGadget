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
    /// Interaction logic for ClientOfDirector.xaml
    /// </summary>
    public partial class ClientOfDirector : Page
    {
        Client c;
        IdValidation idv;
        public ClientOfDirector()
        {
            InitializeComponent();
            c = new Client();
            IdValidation idv = new IdValidation();
            lv1.ItemsSource = ClientService.GetClient();
              
        }

      
        private void updateclient(object sender, RoutedEventArgs e)
        {
             if (lv1.SelectedItems != null) 
            { 
            addclient2 addclient = new addclient2((Client)lv1.SelectedItem);
            NavigationService.Navigate(addclient);
            }
        }

       
      
        private void lookforp(object sender, RoutedEventArgs e)
        {
            lv1.ItemsSource = ClientService.GetClient().Where(x => x.Phone.StartsWith(Searchbyphone.Text)).ToList();
        }

        private void SearchbyID_TextChanged(object sender, TextChangedEventArgs e)
        {
            lv1.ItemsSource = ClientService.GetClient().Where(x => x.Id.StartsWith(SearchbyID.Text)).ToList();

        }

        private void Searchbyph_TextChanged(object sender, TextChangedEventArgs e)
        {
            lv1.ItemsSource = ClientService.GetClient().Where(x => x.Phone.StartsWith(Searchbyphone.Text)).ToList();

        }

      
    }
}
