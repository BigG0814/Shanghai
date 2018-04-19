using Shanghai.Data.Entities;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.System.BLL
{
    [DataObject]
   public class PaymentTypeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PaymentType> Payment_List()
        {
            using (var context = new ShanghaiContext())
            {
                return context.PaymentTypes.ToList();
            }
        }
    }
}
