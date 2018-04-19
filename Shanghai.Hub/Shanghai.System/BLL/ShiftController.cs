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
using Microsoft.AspNet.Identity;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class ShiftController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Shift> EmployeeDayJobTypeShift_Get(int employeeid, int jobtypeid, DateTime shiftdate)
        {
            using (var context = new ShanghaiContext())
            {
                var resault = from x in context.Shifts
                              where x.EmployeeID.Equals(employeeid) && x.JobTypeID.Equals(jobtypeid) && x.ShiftDate.Equals(shiftdate)
                              select x;
                return resault.ToList();
            }

        }
        #region CRUD


        [DataObjectMethod(DataObjectMethodType.Update)]
        public void SetShiftToFinalized(int shiftid)
        {
            using (var context = new ShanghaiContext())
            {
                var existingItem = context.Shifts.Find(shiftid);
                existingItem.isFinal = true;
                // do i need to add is modified???
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void SetShiftToUnFinalized(int shiftid)
        {
            using (var context = new ShanghaiContext())
            {
                var existingItem = context.Shifts.Find(shiftid);
                existingItem.isFinal = false;
                // do i need to add is modified???
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void Create_Shift(Shift shiftitem)
        {
            using (var context = new ShanghaiContext())
            {
                shiftitem = context.Shifts.Add(shiftitem);
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void Update_Shift(ShiftTrade item)
        {
            using(var context = new ShanghaiContext())
            {
                if(item.NewEmployee == null)
                {
                    throw new Exception ("No new Employee has agreed to the shift");
                }
                else
                {
                    var exist = context.Shifts.Find(item.ShiftID);
                    exist.EmployeeID = item.NewEmployee ?? default(int);
                    context.Entry(exist).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
        
        public void DeleteShift(int shiftid)
        {
            using (var context = new ShanghaiContext())
            {
                var existingItem = context.Shifts.Find(shiftid);
                context.Shifts.Remove(existingItem);
                context.SaveChanges();

            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteShift(Shift item)
        {
            DeleteShift(item.ShiftID);
        }
        #endregion
        //[DataObjectMethod(DataObjectMethodType.Select)]
        //public List<JobType> Job_List()
        //{
        //    using (var context = new ShanghaiContext())
        //    {
        //        return context.JobTypes.ToList();
        //    }
        //}

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Shift> ShiftByDate(DateTime thedate)
        {
            using (var context = new ShanghaiContext())
            {
                var resulat = from x in context.Shifts
                              where x.ShiftDate.Equals(thedate)
                              select x;
                return resulat.ToList();
            }
        }




        public List<ShiftGroup> GetShiftsByWeek(DateTime start, DateTime end)
        {
            using (var context = new ShanghaiContext())
            {
                List<ShiftGroup> groups = new List<ShiftGroup>();

                var shifts = (from x in context.Shifts.Include(x => x.Employee).Include(x => x.JobType).ToList()
                              where x.ShiftDate >= start
                              && x.ShiftDate <= end
                              where x.JobType.isActive
                              select new ShiftSummary
                              {
                                  ShiftID = x.ShiftID,
                                  EmployeeId = x.EmployeeID,
                                  EmployeeName = x.Employee.FullName,
                                  StartTime = x.StartTime,
                                  EndTime = x.EndTime,
                                  JobTypeID = x.JobTypeID,
                                  isFinal = x.isFinal
                              }).ToList();
                for (DateTime date = start; date <= end; date = date.AddDays(1))
                {
                    var jobs = (from x in context.JobTypes.ToList()
                                where x.isActive
                                select new ShiftGroup
                                {
                                    JobType = x.JobTypeID,
                                    JobDescription = x.Description,
                                    Date = date,
                                    Shifts = shifts.Where(y => y.StartTime.Date == date && y.JobTypeID == x.JobTypeID).ToList()
                                }).ToList();
                    groups.AddRange(jobs);
                }
                return groups;
            }
        }

        public Shift GetShiftByID(int Id)
        {
            using(var context = new ShanghaiContext())
            {
                return context.Shifts.Include(x => x.Employee).Where(x => x.ShiftID == Id).FirstOrDefault();
            }
        }

        public List<ShiftGroup> GetFinalizedShiftListByWeek(DateTime start, DateTime end)
        {
            using (var context = new ShanghaiContext())
            {
                List<ShiftGroup> groups = new List<ShiftGroup>();
                var shifts = (from x in context.Shifts.Include(x => x.Employee).Include(x => x.JobType).ToList()
                              where x.ShiftDate >= start
                              && x.ShiftDate <= end
                              && x.isFinal == true
                              select new ShiftSummary
                              {
                                  ShiftID = x.ShiftID,
                                  EmployeeId = x.EmployeeID,
                                  EmployeeName = x.Employee.FullName,
                                  StartTime = x.StartTime,
                                  EndTime = x.EndTime,
                                  JobTypeID = x.JobTypeID,
                                  isFinal = x.isFinal
                              }).ToList();
                for (DateTime date = start; date <= end; date = date.AddDays(1))
                {
                    var jobs = (from x in context.JobTypes.ToList()
                                select new ShiftGroup
                                {
                                    JobType = x.JobTypeID,
                                    JobDescription = x.Description,
                                    Date = date,
                                    Shifts = shifts.Where(y => y.StartTime.Date == date && y.JobTypeID == x.JobTypeID).ToList()
                                }).ToList();
                    groups.AddRange(jobs);
                }
                return groups;
            }
        }

        public List<ShiftGroup> GetShiftsByWeekByEmployee(DateTime start, DateTime end, int EmployeeID)
        {
            using (var context = new ShanghaiContext())
            {
                List<ShiftGroup> groups = new List<ShiftGroup>();

                var shifts = (from x in context.Shifts.Include(x => x.Employee).Include(x => x.JobType).ToList()
                              where x.ShiftDate >= start
                              && x.ShiftDate <= end
                              && x.EmployeeID == EmployeeID
                              select new ShiftSummary
                              {
                                  ShiftID = x.ShiftID,
                                  EmployeeId = x.EmployeeID,
                                  EmployeeName = x.Employee.FullName,
                                  StartTime = x.StartTime,
                                  EndTime = x.EndTime,
                                  JobTypeID = x.JobTypeID
                              }).ToList();
                for (DateTime date = start; date <= end; date = date.AddDays(1))
                {
                    var jobs = (from x in context.JobTypes.ToList()
                                select new ShiftGroup
                                {
                                    JobType = x.JobTypeID,
                                    JobDescription = x.Description,
                                    Date = date,
                                    Shifts = shifts.Where(y => y.StartTime.Date == date && y.JobTypeID == x.JobTypeID).ToList()
                                }).ToList();
                    groups.AddRange(jobs);
                }
                return groups;
            }
        }

        // KW Methods to add and edit job Types
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddJobType(JobType newJob)
        {
            using (var context = new ShanghaiContext())
            {
                context.JobTypes.Add(newJob);
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public JobType Get_JobTypeById(int jobId)
        {
            using (var context = new ShanghaiContext())
            {
                return context.JobTypes.Where(x => x.JobTypeID == jobId).FirstOrDefault();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<JobType> List_JobTypes()
        {
            using (var context = new ShanghaiContext())
            {
                return context.JobTypes.ToList();
            }
        }

        public void EditJobType(JobType changedJob)
        {
            using (var context = new ShanghaiContext())
            {
                var jobToEdit = context.JobTypes.Where(x => x.JobTypeID == changedJob.JobTypeID).FirstOrDefault();

                if (jobToEdit.Description != changedJob.Description)
                    jobToEdit.Description = changedJob.Description;
                jobToEdit.isActive = changedJob.isActive;
                context.SaveChanges();
                
            }
        }

        //KW get info for onshift manager
        public List<string> GetOnShiftManagerContactInfo(List<int> managerIds)
        {
            List<string> contactInfo = new List<string>();
            List<int> onDutyManager = new List<int>();
            using (var context = new ShanghaiContext())
            {
                var results = context.WorkHours.Where(x => x.StartTime != null && x.EndTime == null).Select(x => x.EmployeeID).ToList();
                foreach(var item in results)
                {
                    foreach(var manager in managerIds)
                    {
                        if(manager == item)
                        {
                            onDutyManager.Add(manager);
                        }
                    }
                }

                foreach(var manager in onDutyManager)
                {
                    var cellNumber = context.Employees.Where(x => x.EmployeeID == manager).Select(x => x.CellPhone).First().ToString();
                    var cellProvider = context.Employees.Where(x => x.EmployeeID == manager).Select(x => x.CellularProvider.CellEmail).First().ToString();
                    string info = cellNumber + cellProvider;
                    contactInfo.Add(info);
                }
                

            }
            return contactInfo;

        }
    }
}
