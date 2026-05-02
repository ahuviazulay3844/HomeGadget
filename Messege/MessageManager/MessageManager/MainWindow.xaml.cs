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
using MessageManager.ServiceReference1;

namespace MessageManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceReference1.Service1Client sc;
        ServiceReference1.Messege msg;
        public MainWindow()
        {
            InitializeComponent();
            sc = new ServiceReference1.Service1Client();
            msg = new ServiceReference1.Messege();
            msg.code = sc.Getcode();
        }

        private void saveMessage(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tb1.Text)||(string.IsNullOrEmpty(tb2.Text)))//בדיקת תקינות
                {
                MessageBox.Show("אנא מלא פרטים");
                return;
                }
           if(ValidateId(tb1.Text) == false)
                {
                MessageBox.Show("תעודת זהות לא תקינה!");
                return;
            }
           
            msg.status = true;
            msg.id=tb1.Text;    
            msg.content=tb2.Text;    
            if (sc.AddMessage(msg))
            {
               msg= new ServiceReference1.Messege();
                tb1.Text = "";
                tb2.Text = "";
                msg.code=sc.Getcode();
            }
            MessageBox.Show("נשלח בהצלחה");
        }
        public static bool ValidateId(string id)
        {
            // בדיקת אורך
            if (id.Length != 9)
            {
                return false;
            }

            // בדיקת תבנית - וודא שהתעודת זהות מכילה רק ספרות
            foreach (char c in id)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            // חישוב - וודא שהספרה האחרונה תואמת את הספרות הקודמות
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                int digit = int.Parse(id[i].ToString());
                sum += (i % 2 == 0) ? digit : (digit < 5) ? digit * 2 : digit * 2 - 9;
            }
            int lastDigit = int.Parse(id[8].ToString());
            return (sum + lastDigit) % 10 == 0;
        }
    }
}
