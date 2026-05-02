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
    /// Interaction logic for addclient.xaml
    /// </summary>
    public partial class addclient : Page
    {
        Client c;
        bool flagUpdate;
        public addclient()
        {
            InitializeComponent();
            c = new Client();
            c.Dateofbirth = DateTime.Now.AddYears(-119);
            this.DataContext = c;
            flagUpdate = false;
           
        }

     

        private void endd1(object sender, RoutedEventArgs e)
        {
            // בדיקת שדות טקסט ריקים
            if (string.IsNullOrEmpty(Id.Text) ||
                string.IsNullOrEmpty(FirstName.Text) ||
                string.IsNullOrEmpty(LastName.Text) ||
                string.IsNullOrEmpty(Phone.Text) ||
                string.IsNullOrEmpty(Email.Text))
            {
                MessageBox.Show("אנא מלא את כל הפרטים");
                return;
            }

            // בדיקת תאריך לידה תקין
            try
            {
                // המרת התאריך מהמחרוזת שהוזנה
                DateTime birthDate = DateTime.Parse(Dateofbirth.SelectedDate.ToString());

                // בדיקה אם תאריך הלידה פג או עבר
                if (birthDate > DateTime.Now)
                {
                    MessageBox.Show("תאריך לידה אינו תקין. לא ניתן להזין תאריך עתידי");
                    return;
                }

                // בדיקה אם תאריך הלידה עוד לא הגיע (לא נולד)
                if (birthDate < DateTime.Now.AddYears(-120))
                {
                    MessageBox.Show("תאריך לידה אינו תקין. לא ניתן להזין תאריך עתיק כל כך");
                    return;
                }
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות פירוט תאריך (פורמט לא תקין)
                MessageBox.Show("תאריך לידה אינו תקין. אנא נסה שוב");
                return;
            }

            // בדיקת שגיאות באמצעות ValidationRules
            if (Validation.GetHasError(Id) ||
                Validation.GetHasError(FirstName) ||
                Validation.GetHasError(LastName) ||
                Validation.GetHasError(Phone) ||
                Validation.GetHasError(Email) ||
                Validation.GetHasError(Dateofbirth))
            {
                MessageBox.Show("אנא ודא שכל השדות מולאו בצורה תקינה");
                return;
            }
            else {
                    
                if (!flagUpdate)
                {
                    if (ClientService.GetClient().Any(x => x.Id == c.Id))
                    {
                        MessageBox.Show("ת.ז. קיימת במערכת");
                        return;
                    }

                    c.Status = true;
                    ClientService.AddClient(c);
                    MessageBox.Show("נוסף בהצלחה");
                    ClientService.UpdateClient();
                    NavigationService.Navigate(new MainPage());
                }
                else
                {
                    this.DataContext = null;
                    this.DataContext = c;
                    if (ClientService.UpdateClient())
                        MessageBox.Show("עודכן בהצלחה");
                    else
                        MessageBox.Show("Error");
                }
                }
            }
        }
    }

