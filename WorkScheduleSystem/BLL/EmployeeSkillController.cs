using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Data.Entities;
using WorkSchedule.Data.POCOs;
using WorkScheduleSystem.DAL;

namespace WorkScheduleSystem.BLL
{
    [DataObject]
    public class EmployeeSkillController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeBySkills> Skills_ListEmployees(int skillid)
        {
            using (var context = new WorkScheduleContext())
            {
                var results = from x in context.EmployeeSkills
                              where x.SkillID.Equals(skillid)
                              select new EmployeeBySkills
                              {
                                  Name = x.Employees.LastName + "," + x.Employees.FirstName,
                                  Phone = x.Employees.HomePhone,
                                  Active = x.Employees.Active,
                                  SkillLevel = x.Level,
                                  YOE = x.YearsOfExperience
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeSkills> Employees_List()
        {
            using (var context = new WorkScheduleContext())
            {
                return context.EmployeeSkills.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmployeeSkills EmployeeSkills_Get(int employeeskillid)
        {
            using (var context = new WorkScheduleContext())
            {
                return context.EmployeeSkills.Find(employeeskillid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int EmployeeSkills_Add(EmployeeSkills item)
        {
            using (var context = new WorkScheduleContext())
            {
                item = context.EmployeeSkills.Add(item);    //staging
                context.SaveChanges();              //commit of the request
                return item.EmployeeSkillID;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int EmployeeSkills_Update(EmployeeSkills item)
        {
            using (var context = new WorkScheduleContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        public int EmployeeSkills_Delete(int employeeskillid)
        {
            using (var context = new WorkScheduleContext())
            {
                var existingItem = context.EmployeeSkills.Find(employeeskillid);
                context.EmployeeSkills.Remove(existingItem);
                return context.SaveChanges();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int EmployeeSkills_Delete(EmployeeSkills item)
        {
            return EmployeeSkills_Delete(item.EmployeeSkillID);
        }

    }
}
