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
    /// Interaction logic for pagebeforecast.xaml
    /// </summary>
    public partial class pagebeforecast : Page
    {
        Client client;
       
        public pagebeforecast()
        {
            InitializeComponent();
            client = new Client();
           
        }

        private void bendbefore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void endbefore(object sender, RoutedEventArgs e)
        {
            bool b = ClientService.GetClient().Any(x => x.Id == tbidbefore.Text);
            if (b)
            {
                client = ClientService.GetClient().FirstOrDefault(x => x.Id == tbidbefore.Text);
                Pageofitem pageofitem = new Pageofitem(client);
                NavigationService.Navigate(pageofitem);

            }
            else
            {
                addclient2 addclient = new addclient2(tbidbefore.Text);
                NavigationService.Navigate(addclient);
            }
        }

        private void endbefore1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbidbefore.Text))
            {
                MessageBox.Show("הקש תעודת זהות");
            }
            bool b = ClientService.GetClient().Any(x => x.Id == tbidbefore.Text && x.Status == true);

            IdValidation idValidation = new IdValidation();
           
            if (!IdValidation.ValidateId(tbidbefore.Text))
            {
                MessageBox.Show("!!תעודת זהות לא תקינה");
                tbidbefore.Text = "";
            }
            else
            {
            if (b)
            {
                client = ClientService.GetClient().FirstOrDefault(x => x.Id == tbidbefore.Text);
                AllStatic.myClient = client;
                AllStatic.myOrder.ItemOrder.Clear();
                Pageofitem pageofitem = new Pageofitem(client);
                NavigationService.Navigate(pageofitem);

            }
            else
            {
                addclient2 addclient = new addclient2(tbidbefore.Text);
                NavigationService.Navigate(addclient);
            }
            }
        }
    }
}

