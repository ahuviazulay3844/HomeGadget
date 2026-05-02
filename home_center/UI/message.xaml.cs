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
using home_center.BL;
using home_center.ServiceReference1;
using home_center.ServiceReference2;


namespace home_center.UI
{
    /// <summary>
    /// Interaction logic for message.xaml
    /// </summary>
    public partial class message : Page
    {
       ServiceReference2.Service1Client sc;    
        public message()
        {
            InitializeComponent();
            sc = new ServiceReference2.Service1Client();
            dg.ItemsSource = sc.GetAllMesseges().Select(x => new{x.id,x.content,x.status,x.code }).ToList();
        }
    }
}
