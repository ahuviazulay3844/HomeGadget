using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
    internal class CityServise
    {
        public static List<City> GetCity()
        { return Globali.myDb.City.ToList(); }    
    }
}
