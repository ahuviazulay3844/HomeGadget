using home_center.BL;
using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.UI
{
    internal class AllStatic
    {
        public static Order myOrder = new Order();
        public static Client myClient=new Client();
        int t;
        public static AllStatic allStaticObject = new AllStatic();
        public static int GetItemOrderCode()
        {
            if (myOrder.ItemOrder.Count > 0)
                return myOrder.ItemOrder.Max(x => x.Code) + 1;
            return ItemOrderService.GetCode();
        }
        
    }
}
