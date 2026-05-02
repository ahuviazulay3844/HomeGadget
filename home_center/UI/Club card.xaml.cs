using home_center.BL;
using home_center.model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for Club_card.xaml
    /// </summary>
    public partial class Club_card : Page
    {
        Client Client1;

        public Club_card()
        {
            InitializeComponent();
           Client1 = new Client();
        }

        private void endclub(object sender, RoutedEventArgs e)
        {
            if( string.IsNullOrEmpty(tbclub.Text))
            {
                MessageBox.Show("הקש תעודת זהות");
            }
            Client1 = ClientService.GetClient().FirstOrDefault(x => x.Id == tbclub.Text);
            AllStatic.myClient = Client1;
            IdValidation idValidation = new IdValidation();
            if (!IdValidation.ValidateId(tbclub.Text))
            {
                MessageBox.Show("!!תעודת זהות לא תקינה");
            }
            else
            {
                if (Client1==null)
                {
                    MessageBoxResult answeare = MessageBox.Show("אופס אנחנו לא מכירים אותך.. להרשמה", "", MessageBoxButton.YesNo);
                    if (answeare == MessageBoxResult.Yes)
                    {
                        addclient addclient = new addclient();
                        NavigationService.Navigate(addclient);
                    }
                    else
                    {
                        NavigationService.Navigate(null);

                    }
                }
                else
                {
                    
                    if ((Client1 != null) && (Client1.Member==false))
                    {
                        
                        AllStatic.myClient.Member = true;                       
                        ClientService.UpdateClient();
                        MessageBox.Show("!!הצטרפת בהצלחה למועדון שלנו כעת תוכל להנות מהטבות שלנו");
                        NavigationService.Navigate(null); 


                    }
                    else 
                    {
                        if(AllStatic.myClient.Member)
                        MessageBox.Show("!!המערכת זיהתה אותך כחבר מועדון");
                        NavigationService.Navigate(null); 
                    }
                }
            }

        }
    }
}
