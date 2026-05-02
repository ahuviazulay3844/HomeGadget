using home_center.BL;
using home_center.model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
    /// Interaction logic for Additem.xaml
    /// </summary>
    public partial class Additem : Page
    {
        string n;
        Item item;
        bool flagUpdate;
        public Additem(string codeitem)
        {
            InitializeComponent();
            item = new Item();
            this.DataContext = item;
            flagUpdate = false;
            item.Price_after = item.Price_base;
            cmb.ItemsSource = CategoryService.GetCategory();//קטגוריות..

        }
        public Additem(Item item) : this(ItemService.GetCode().ToString())
        {


            this.item = item;
            this.DataContext = null;
            this.DataContext = item;
            flagUpdate = true;//במצב עידכון ולא הוספה
            imageadditem.Source = MyPicture.GetImage(item.NameImage);
            n = MyPicture.UploadImage_Dlg();

        }

        private void addimage(object sender, RoutedEventArgs e)
        {
n = MyPicture.UploadImage_Dlg();
            imageadditem.Source = MyPicture.GetImage(n);
            item.NameImage = n;
        }

        private void enditem(object sender, RoutedEventArgs e)
        {
            // בדיקת שדות ריקים
            if (string.IsNullOrEmpty(tbi2.Text) ||
                string.IsNullOrEmpty(tbi3.Text) ||
                string.IsNullOrEmpty(tbi4.Text) ||
                tbi3.Text == (0).ToString() ||
                tbi4.Text == (0).ToString() ||
              cmb.SelectedItem == null ||
                imageadditem.Source == null)
            {
                MessageBox.Show("אנא מלא את כל הפרטים");
                return;
            }
            else
            {
                if (Validation.GetHasError(tbi2) || Validation.GetHasError(tbi4) || Validation.GetHasError(tbi3))
                {
                    MessageBox.Show("Error");
                    return;
                }
                else
                {
                    if (!flagUpdate)
                    {

                        if (ItemService.GetItem().Where(x => x.Status == true).ToList().Any(x => x.NameImage == n))
                        {
                            MessageBox.Show("מוצר זה קיים במערכת");
                            return;
                        }
                        else
                        {
                            item.Code = ItemService.GetCode();
                            //ItemService.UpdateItem();
                            item.Status = true;
                            ItemService.AddItem(item);
                            MessageBox.Show("נוסף בהצלחה");
                            //ItemService.UpdateItem();
                            NavigationService.Navigate(new Kindofitem());
                        }
                    }

                    else
                    {
                        this.DataContext = null;
                        this.DataContext = item;
                        if (ItemService.UpdateItem())
                        {
                            MessageBox.Show("עודכן בהצלחה");
                            NavigationService.Navigate(new Kindofitem());
                        }
                        else
                            MessageBox.Show("Error");
                    }
                }
            }
        }
    }
}





