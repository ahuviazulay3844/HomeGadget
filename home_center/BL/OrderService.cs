using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
    internal class OrderService
    {
        public static void AddOrder(Order Order)
        {
            Globali.myDb.Order.Add(Order);
            Globali.myDb.SaveChanges();
        }
        public static List<Order> GetOrder()
        {
            return Globali.myDb.Order.ToList();
        }
        public static bool UpdateOrder()
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
            if (GetOrder().Count == 0)
                return 100;
            return GetOrder().Max(x => x.CodeOrder) + 1;
        }
    }
}

