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
    /// Interaction logic for ChageCode.xaml
    /// </summary>
    public partial class ChageCode : Page
    {
        codedirector code1;
        public ChageCode()
        {
            InitializeComponent();
            code1 = new codedirector();
            code1 = BL.codedirectionService.Getcodedirector().First();


        }

        private void ChangeCode(object sender, RoutedEventArgs e)
        {
            if (Pb1.Password.ToString() == code1.code)
            {
                Pb1.Visibility= Visibility.Collapsed;   
                tbl1.Visibility = Visibility.Collapsed;
                b1.Visibility = Visibility.Collapsed;
                Pb3.Visibility= Visibility.Collapsed;   
                tbl3.Visibility = Visibility.Collapsed;
                b3.Visibility = Visibility.Collapsed;
                b2.Visibility = Visibility.Visible ;
                Pb2.Visibility = Visibility.Visible;
                tbl2.Visibility = Visibility.Visible;   
            }
            else 
            {
                MessageBox.Show(" קוד שגוי , הקש פעם נוספת ");
            }
        }

        private void again(object sender, RoutedEventArgs e)
        {
           
        }

        private void againe1(object sender, RoutedEventArgs e)
        {
           
            Pb3.Visibility = Visibility.Visible;
            tbl3.Visibility = Visibility.Visible;
            b3.Visibility = Visibility.Visible;

         
           
        }
    }
}
