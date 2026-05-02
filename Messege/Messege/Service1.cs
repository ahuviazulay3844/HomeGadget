using Messege.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Messege
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        Model.meddegedbEntities1 MyDB = new meddegedbEntities1();
        public bool AddMessage(Model.Messege messege)
        {
            try
            {
                MyDB.Messege.Add(messege);
                MyDB.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        public List<Model.Messege> GetAllMesseges()
        {
            return MyDB.Messege.ToList();
        }

        public int Getcode()
        {
            if (GetAllMesseges().Count == 0)
                return 1;
            return (GetAllMesseges().Max(x => x.code) + 1);
        }

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public List<Model.Messege> GetMesseges(string idCust)
        {
            return MyDB.Messege.Where(x => x.content == idCust).ToList();
        }

        

        public bool SetStatuse(Model.Messege messege)
        {
            try
            {
                messege.status = false;
                MyDB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
    }
}
