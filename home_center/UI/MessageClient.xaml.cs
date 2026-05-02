using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Interaction logic for MessageClient.xaml
    /// </summary>
    public partial class MessageClient : Page
    {
        public MessageClient()
        {
            InitializeComponent();
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            //messege.code = 1;
            //messege.status = false;
            //service1.AddMessage(messege);
        }
    }
}
