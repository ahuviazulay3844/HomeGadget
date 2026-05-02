//using home_center.UI.model;
using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
    public class Globali
    {

        public static Entities4 myDb;

        static Globali()
        { myDb = new Entities4(); }
    }
}
