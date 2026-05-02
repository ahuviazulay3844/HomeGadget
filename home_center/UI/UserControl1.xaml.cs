using HandyControl.Tools;
using HandyControl.Tools.Extension;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace home_center.UI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        int amount1 = 0;
        model.Item Item1;
        model.ItemOrder itemOrder;



        public UserControl1(model.Item item)
        {
            InitializeComponent();
            this.Margin = new Thickness(15);
            this.Item1 = item;
            //איגוד היוזרכונטרול ליין שקיבל כשנוצר
            this.DataContext = Item1;
            amount.DataContext = amount1;
            imagecart.Source = MyPicture.GetImage(item.NameImage);
            this.itemOrder = new ItemOrder();
            itemOrder.Item = item;
            itemOrder.CodeItem = item.Code;     
            itemOrder.Order=AllStatic.myOrder;
            itemOrder.CodeOrders = AllStatic.myOrder.CodeOrder;
            itemOrder.Status = true;
            amount.Text = amount1.ToString();
        }

       

        private void blessitem(object sender, RoutedEventArgs e)
        {
           
            if (amount1 != 0)
            {
                amount1--;
                amount.Text = amount1.ToString();
                amount.DataContext = null;
                amount.DataContext = amount1;             
                ItemOrderService.UpdateItemOrder();
            }
            else
            {
                MessageBox.Show("מצטערים,הפעולה אינה אפשרית");
            }

        }

        private void badditem(object sender, RoutedEventArgs e)
        { 

            if (this.Item1.Amount <= amount1)
            {
                MessageBox.Show("!!חסר במלאי");
            }
            else
            {
                amount1++;
                amount.Text = amount1.ToString();
                //itemOrder.Item.Amount = itemOrder.Item.Amount - amount1;   
                //itemOrder.Item.Amount -= amount1;
                ItemOrderService.UpdateItemOrder();

            }

        }

        private void Button_Click_1(ItemOrder ItemOrder)
        {
            itemOrder.Code = AllStatic.GetItemOrderCode();
            itemOrder.Amount = amount1;
            AllStatic.myOrder.ItemOrder.Add(itemOrder);

        }

     

      

        private void addcart1()
        {   int x = 0;
            itemOrder=new ItemOrder();
            if (amount1 == 0)
            {
                MessageBox.Show("!!הקש כמות");
            }
           
            else 
            { 
                if((amount1 >= 1)&&(x!=0))
                {
                    BL.ItemService.UpdateItem();
                }
                itemOrder.Code = BL.ItemOrderService.GetCode()+AllStatic.myOrder.ItemOrder.Count;
                itemOrder.Amount = int.Parse(amount.Text);
               
                itemOrder.Item = Item1;
            AllStatic.myOrder.ItemOrder.Add(itemOrder);
            BL.ItemService.UpdateItem();
            double allStaticObject = Item1.Price_base * itemOrder.Amount;
            MessageBox.Show("!!המוצר נוסף בהצלחה");
                x = 1;

            }

        }

        private void addcart(object sender, RoutedEventArgs e)
        {
           
                if (amount1 == 0)
                {
                    MessageBox.Show("הקש כמות.");
                   itemOrder.Amount=0;  
                    ItemService.UpdateItem();
                }
             
            else
            {
            

            if (AllStatic.myOrder.ItemOrder.Any(x => x.Item == Item1)&& (itemOrder.Amount != 0))//אך הכמות לא 0
            {
              MessageBoxResult r=  MessageBox.Show("האם ברצונך לעדכן את כמות", "", MessageBoxButton.YesNo);
                if ((r==MessageBoxResult.Yes))
                {
                    AllStatic.myOrder.ItemOrder.First(x => x.Item == Item1).Amount = Convert.ToInt32(amount.Text);
                        ItemService.UpdateItem();
                }
            }
            else
                addcart1();
            }
        }
    }
}


