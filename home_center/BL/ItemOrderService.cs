using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
    internal class ItemOrderService
    {
        public static void AddItemOrder(ItemOrder ItemOrder)
        {

            Globali.myDb.ItemOrder.Add(ItemOrder);
            Globali.myDb.SaveChanges(); 
        }
        public static List<ItemOrder> GetItemOrder()
        {
            return Globali.myDb.ItemOrder.ToList();
        }
        public static bool UpdateItemOrder()
        {
            try
            {
                Globali.myDb.SaveChanges();
                return true;   // ..אם העדכון הצליח 
            }
            catch (Exception)
            {
                return false;   //..אם לא הצליח 
            }
        }
        public static int GetCode()
        {
            if (GetItemOrder().Count == 0)
                return 100;
            return GetItemOrder().Max(x => x.Code) + 1;
        }
    }
}

