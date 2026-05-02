using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
    internal class ItemService
    {
        public static void AddItem(Item Item)
        {

            Globali.myDb.Item.Add(Item);
            Globali.myDb.SaveChanges();
        }
        public static List<Item> GetItem()
        {
            return Globali.myDb.Item.ToList();
        }
        public static bool UpdateItem()
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
            if (GetItem().Count == 0)
                return 100;
            return GetItem().Max(x => x.Code) + 1;
        }
    }
}

