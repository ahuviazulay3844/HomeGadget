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
    /// Interaction logic for DirectorCode.xaml
    /// </summary>
    public partial class DirectorCode : Page
    {
       
        codedirector code1;
        NavigationService nav;

      
        public DirectorCode( NavigationService nav)
        {
            InitializeComponent();
            this.nav = nav;
            code1 = new codedirector();
            code1 = BL.codedirectionService.Getcodedirector().First();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb1.Password))
            {
                MessageBox.Show("הקש קוד מנהל");
            }
            if ( tb1.Password.ToString()== code1.code)
            { 
            director director = new director();     
            nav.Navigate(director);
            }
        else
            {
                MessageBox.Show("הקוד"+" "+ tb1.Password+" "+"הינו קוד שגוי");  


            }
        }
    }
}
