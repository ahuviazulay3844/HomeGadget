using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
    internal class codedirectionService
    {
        public static void Addcodedirection(codedirector codedirection)
        {

            Globali.myDb.codedirector.Add(codedirection);
            Globali.myDb.SaveChanges();
        }
        public static List<codedirector> Getcodedirector()
        {
            return Globali.myDb.codedirector.ToList();
        }
        public static bool Updatecodedirection()
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
        //public static int GetCode()
        //{
        //    if (Getcodedirector().Count == 0)
        //        return 100;
        //    return Getcodedirector().Max(x => x.code) + 1;
        //}
    }
}

