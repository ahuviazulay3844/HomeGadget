//using home_center.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.BL
{
    internal class CategoryService
    {
        public static void AddCategory(model.Category Category)
        {
            
            Globali.myDb.Category.Add(Category);
            Globali.myDb.SaveChanges();
        }
        public static List<model.Category> GetCategory()
        {
            return Globali.myDb.Category.Where(x=>x.Status==true).ToList();
        }
        public static List<model.Category> GetCategoryAll()
        {
            return Globali.myDb.Category.ToList();
        }
        public static bool UpdateCategoryt()
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
            if (GetCategoryAll().Count == 0)
                return 100;
            return GetCategoryAll().Max(x => x.Code) + 1;
        }

    }
}

