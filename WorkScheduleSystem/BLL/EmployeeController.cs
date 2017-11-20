using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Data.Entities;
using WorkScheduleSystem.DAL;

namespace WorkScheduleSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Employees> Employees_List()
        {
            using (var context = new WorkScheduleContext())
            {
                return context.Employees.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Employees Employees_Get(int employeeid)
        {
            using (var context = new WorkScheduleContext())
            {
                return context.Employees.Find(employeeid);
            }
        }
    }
}
