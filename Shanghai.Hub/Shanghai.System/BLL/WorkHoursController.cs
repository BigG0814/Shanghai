using Shanghai.Data.Entities;
using Shanghai.Data.POCOs;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class WorkHoursController
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void Create_WorkHours(WorkHour WorkHoursitem)
        {
            using (var context = new ShanghaiContext())
            {
                WorkHoursitem = context.WorkHours.Add(WorkHoursitem);
                context.SaveChanges();
            }
        }
      
        public bool FindActiveWorkHours(int EmployeeID)
        {
            using (var context = new ShanghaiContext())
            {
                bool active;
                WorkHour result = (from x in context.WorkHours
                             where x.EmployeeID == EmployeeID && x.StartTime != null && x.EndTime == null
                             select x).FirstOrDefault();
                if (result == null)
                {
                    active = false; //if there is not an active WorkHours returned in the query set the bool to false
                }
                else
                {
                    active = true;

                }
                return active;
            }
        }
        public void EndWorkHours(int EmployeeID)
        {
            using (var context = new ShanghaiContext())
            {
                int WorkHID = (from x in context.WorkHours
                              where x.EmployeeID == EmployeeID && x.StartTime != null && x.EndTime == null
                              select x.WorkID).FirstOrDefault();
                var existing = context.WorkHours.Find(WorkHID);
                existing.EndTime = DateTime.Now;
                context.Entry(existing).Property(x => x.EndTime).IsModified = true;
                context.SaveChanges();

            }
        }
        public void StartWorkHours(int EmployeeID)
        {
            using (var context = new ShanghaiContext())
            {
               //stop from being able to make more then one open shift
                    WorkHour WorkHours = new WorkHour();
                    WorkHours.EmployeeID = EmployeeID;
                    WorkHours.StartTime = DateTime.Now;
                        context.WorkHours.Add(WorkHours);
                        context.SaveChanges();
//existing = context.WorkHours.Find(WorkHoursID);
                //existing.StartTime = DateTime.Now;
                //context.Entry(existing).Property(x => x.StartTime).IsModified = true;
                //context.SaveChanges();

            }
        }

        public void DeleteWorkHours(int WorkHoursid)
        {
            using (var context = new ShanghaiContext())
            {
                var existingItem = context.WorkHours.Find(WorkHoursid);
                context.WorkHours.Remove(existingItem);
                context.SaveChanges();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
       public List<WorkHour> GetWorkHoursByEmployee(int EmployeeID)
        {
            using (var context = new ShanghaiContext())
            {
                //DateTime weekending = DateTime.Today;
                //for (int i = 0; i < 6; i++)
                //{
                //    if (weekending.DayOfWeek == DayOfWeek.Sunday)
                //    {
                //        i = 6;
                //    }
                //    else
                //    {
                //        weekending = weekending.AddDays(1);
                //    }
                //}

                //var WeekStarting = weekending.AddDays(-6);
                //var WeekEnding = weekending;
                List<WorkHour> EmployeeHours;
                EmployeeHours = (from x in context.WorkHours.ToList()
                                  where x.EmployeeID == EmployeeID
                                 //where x.StartTime >= WeekStarting
                                 //&& x.EndTime <= weekending
                                  select x).Reverse().ToList();
                return EmployeeHours;
            }
        }
    }
}
