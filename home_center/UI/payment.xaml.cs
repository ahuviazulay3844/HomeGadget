using HandyControl.Tools;
using home_center.BL;
using home_center.model;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static HandyControl.Tools.Interop.InteropValues;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace home_center.UI
{
    /// <summary>
    /// Interaction logic for payment.xaml
    /// </summary>
    public partial class payment : Page
    {
        double sum = 0;
        double sum2 = 0;
        double sum3 = 0;
        Class1 tashlum;
        bool c;
        bool b;
        model.City city1;
        int city2;
        ItemOrder ItemOrder;
        public payment()
        {
            InitializeComponent();
            tashlum = new Class1();
            ItemOrder = new ItemOrder();
            city1 = new City();

            city.ItemsSource = CityServise.GetCity();


            b = true;
            sum = AllStatic.myOrder.ItemOrder.Sum(x => x.Amount * x.Item.Price_base);
            AllStatic.myOrder.PriceEnd = sum;
            OrderService.UpdateOrder();
            sum1.Text = sum.ToString();
            if (Convert.ToInt32(sum1.Text) < 200)

            {
                tbms.Visibility = Visibility.Collapsed;
                mstaslomim.Visibility = Visibility.Collapsed;
                b = false;
            }
            if (AllStatic.myClient.Member)
            {
                if (DateTime.Now.Month == AllStatic.myClient.Dateofbirth.Month)
                {
                    sum2 = (sum) * 0.05;//לאחר 5% הנחה
                    sum3 = sum * 0.95;      //לאחר 5% הנחה
                    howmutch.Text = (sum2 + (sum3) * 0.3).ToString(); //מענק יום הולדת
                    howmutchafter.Text = (sum - (Convert.ToDouble(howmutch.Text))).ToString();
                    MessageBox.Show("שמענו שמגיע לך מזל טוב ולכן אנו מעניקים לך 30% הנחה נוספים ");
                }
                else
                {
                    howmutch.Text = (((sum)) * 0.05).ToString();
                    howmutchafter.Text = (((sum)) * 0.95).ToString();
                }
            }
            else
            {
                if (!AllStatic.myClient.Member)
                {
                    howmutch.Text = 0.ToString();
                    howmutchafter.Text = (sum).ToString();
                }
            }

            this.stpPay.DataContext = tashlum;

            FilCombo();
        }

        private void FilCombo()
        {
            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 6; i++)
            {
                cmbYear.Items.Add(i.ToString());
            }

        }

        private void end3(object sender, RoutedEventArgs e)
        {
           
            if (b == false)
            {
                if (string.IsNullOrEmpty(mscard.Text) ||
                               string.IsNullOrEmpty(sfarot.Text) ||
                               string.IsNullOrEmpty(tb7.Text) ||
                               string.IsNullOrEmpty(tb8.Text) ||
                               string.IsNullOrEmpty(idcard.Text) ||
                               city.SelectedItem == null ||
                               tocef.SelectedItem == null ||
                               cmbYear.SelectedItem == null)
                {
                    MessageBox.Show("אנא מלא את כל הפרטים");

                    return;
                }
              
            }
            else if (b)
            {
                // בדיקת שדות טקסט ריקים
                if (string.IsNullOrEmpty(mscard.Text) ||
                    string.IsNullOrEmpty(sfarot.Text) ||
                    string.IsNullOrEmpty(tb7.Text) ||
                    string.IsNullOrEmpty(tb8.Text) ||
                    string.IsNullOrEmpty(idcard.Text) ||
                    tocef.SelectedItem == null ||
                    city.SelectedItem == null ||
                    cmbYear.SelectedItem == null ||
                   mstaslomim.SelectedItem == null)
                {
                    MessageBox.Show("אנא מלא את כל הפרטים");
                    return;
                }
                else
                {
                    if(IdValidation.ValidateId(idcard.Text)==false)
                    {
                        MessageBox.Show("תעודת זהות אינה תקינה");
                        return; 
                    }
                }
            }

            if (IdValidation.IsValidIsraeliCreditCardNumber(mscard.Text) == false)
            {
                MessageBox.Show("מספר כרטיס לא תקין");
                mscard.Text = "";
                return;
            }
            AllStatic.myOrder.CodeOrder = OrderService.GetCode();
            OrderService.AddOrder(AllStatic.myOrder);
            ItemService.UpdateItem();
            AllStatic.myOrder.DateOrder= DateTime.Now;  // תאריך הזמנה
            AllStatic.myOrder.PriceEnd = Convert.ToDouble(howmutchafter.Text);                            //
            OrderService.UpdateOrder();     
            AllStatic.myClient.Stree = tb7.Text;
            AllStatic.myClient.HouseNumber =int.Parse (tb8.Text);
            ClientService.UpdateClient();
            MessageBox.Show("התשלום בוצע בהצלחה");
            MessageBox.Show("המשלוח יגיע עד פתח הבית תוך שלושה ימים ");
            ClientService.UpdateClient();
            UpdateAmount();
            AllStatic.myOrder = new Order(); //איפוס הזמנה 
            AllStatic.myClient = new Client();
            NavigationService.Navigate(null);

        }

        private void UpdateAmount()
        {
            foreach (model.ItemOrder it in AllStatic.myOrder.ItemOrder)
            {
                it.Item.Amount -= it.Amount;
            }
            BL.ItemService.UpdateItem();
        }




        private void cmbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbYear.SelectedItem != null)
            {
                if (Convert.ToInt32(cmbYear.SelectedItem) == DateTime.Now.Year)
                { tocef.SelectedIndex = DateTime.Now.Month - 1; }
            }
        }

        private void tocef_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tocef.SelectedItem != null)
            {
                if (Convert.ToInt32(cmbYear.SelectedItem) == DateTime.Now.Year && tocef.SelectedIndex < DateTime.Now.Month)
                {
                    tocef.SelectedIndex = DateTime.Now.Month - 1;
                    MessageBox.Show("פג תוקף זה אנא הקש תוקף תקין");
                }
            }
        }

        private void sfarot3(object sender, RoutedEventArgs e)
        {
            if (sfarot.Text.Length > 3)
            {
                MessageBox.Show("!!מספר הספרות בגב הכרטיס שיש להזין הוא ,3 ");
                sfarot.Text = "";
            }
        }

      
        public static bool IsValidIsraeliCreditCardNumber(string cardNumberStr)
        {
            // בדיקות מקדימות
            if (string.IsNullOrEmpty(cardNumberStr))
            {
                return false;
            }

            if (cardNumberStr.Length != 16)
            {
                return false;
            }

            if (!cardNumberStr.StartsWith("53"))
            {
                return false;
            }

            if (!cardNumberStr.All(char.IsDigit))
            {
                return false;
            }

            // חישוב ספרת ביקורת (אלגוריתם Luhn)
            int sum = 0;
            for (int i = cardNumberStr.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumberStr[i].ToString());
                if (i % 2 == 0)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }
                sum += digit;
            }

            return sum % 10 == 0;
        }


        private void city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (city.SelectedItem != null)
            {

                City c = city.SelectedItem as City;
                int x = c.codeCity;
                AllStatic.myClient.City = x;
                ClientService.UpdateClient();

            }
        }
    }
}