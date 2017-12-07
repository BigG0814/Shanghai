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

        public List<EmployeeBySkills> Level_Get(int employeeid)
        {
            using (var context = new WorkScheduleContext())
            {
                var results = from x in context.EmployeeSkills
                              where x.EmployeeID.Equals(employeeid)
                              select new EmployeeBySkills
                              {
                                  Level = x.Level                                  
                              };
                return results.ToList();
            }
        }

        

        public List<SkillSet> Register_Employee(string firstname, string lastname, string homephone,
            string skillname, int level, int? yoe, decimal hourlywage, int skillid)
        {
            using (var context = new WorkScheduleContext())
            {
                //code to go here
                //Part One:
                //query to get the playlist id
                var exists = (from x in context.Employees
                              where x.FirstName.Equals(firstname)
                                && x.LastName.Equals(lastname)
                                && x.HomePhone.Equals(homephone)
                              select x).FirstOrDefault();

                //initialize the tracknumber
                int employeeskillnumber = 0;
                //I will need to create an instance of PlaylistTrack
                EmployeeSkills newemployeeskill = null;

                //determine if a playlist "parent" instances needs to be
                // created
                if (exists == null)
                {
                    //this is a new playlist
                    //create an instance of playlist to add to Playlist tablge
                    exists = new Employees();
                    exists.FirstName = firstname;
                    exists.LastName = lastname;
                    exists.HomePhone = homephone;
                    exists = context.Employees.Add(exists);
                    //at this time there is NO phyiscal pkey
                    //the psuedo pkey is handled by the HashSet
                    employeeskillnumber = 1;
                }
                else
                {
                    //playlist exists
                    //I need to generate the next track number
                    employeeskillnumber = exists.EmployeeSkills.Count() + 1;

                    //validation: in our example a track can ONLY exist once
                    //   on a particular playlist
                    newemployeeskill = exists.EmployeeSkills.SingleOrDefault(x => x.SkillID == skillid);
                    if (newemployeeskill != null)
                    {
                        throw new Exception("Employeelist already has requested Skill.");
                    }
                }

                //Part Two: Add the PlaylistTrack instance
                //use navigation to .Add the new track to PlaylistTrack
                newemployeeskill = new EmployeeSkills();
                newemployeeskill.SkillID = skillid;
                newemployeeskill.Skills.Description = skillname;
                newemployeeskill.Level = level;
                newemployeeskill.YearsOfExperience = yoe;
                newemployeeskill.HourlyWage = hourlywage;

                //NOTE: the pkey for PlaylistId may not yet exist
                //   using navigation one can let HashSet handle the PlaylistId
                //   pkey value
                exists.EmployeeSkills.Add(newemployeeskill);

                //physically add all data to the database
                //commit
                context.SaveChanges();
                return Skills_List(firstname, lastname, homephone, skillname, level, yoe, hourlywage);
            }
        }//eom


        //[DataObjectMethod(DataObjectMethodType.Select, false)]


        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<EmployeeBySkills> EmployeeSkill_List()
        //{
        //    using (var context = new WorkScheduleContext())
        //    {
        //        var results = from x in context.EmployeeSkills
        //                      select new EmployeeBySkills
        //                      {
        //                          ID = x.EmployeeID,
        //                          Employee = x.Employees.LastName + "," + x.Employees.FirstName,
        //                          Skill =x.Skills.Description,
        //                          Phone = x.Employees.HomePhone,
        //                          Active = x.Employees.Active,
        //                          SkillLevel = x.Level,
        //                          YOE = x.YearsOfExperience
        //                      };
        //        return results.ToList();
        //    }
        //}



        #region Report query
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeSkillReport> EmployeeSkillReport_Get()
        {
            using (var context = new WorkScheduleContext())
            {
                var results = from x in context.EmployeeSkills
                              select new EmployeeSkillReport
                              {
                                  Skill = x.Skills.Description,
                                  Name = x.Employees.FirstName + "," + x.Employees.LastName,
                                  Phone = x.Employees.HomePhone,
                                  Level = x.Level == 1 ? "Novice" : x.Level == 2 ? "Proficient" : "Expert",
                                  YOE = x.YearsOfExperience
                              };
                return results.ToList();
            }
        }
        #endregion

        #region CRUD
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

        #endregion

    }
}
