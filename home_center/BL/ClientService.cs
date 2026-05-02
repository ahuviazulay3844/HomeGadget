using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
   
    internal class ClientService
    {
        
        public static void AddClient(Client c)
        {

            Globali.myDb.Client.Add(c);
            Globali.myDb.SaveChanges();
        }
        public static List<Client> GetClient()
        {
            return Globali.myDb.Client.Where(x=>x.Status==true).ToList();
        }
        public static bool UpdateClient()
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
    }
}
