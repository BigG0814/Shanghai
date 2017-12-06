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
    public class SkillController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Skills> Skills_List()
        {
            using (var context = new WorkScheduleContext())
            {
                var results = from x in context.Skills
                              orderby x.Description
                              select new Skills
                              {
                                  SkillID = x.SkillID,
                                  Description = x.Description
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Skills Skills_Get(int skillid)
        {
            using (var context = new WorkScheduleContext())
            {
                return context.Skills.Find(skillid);
            }
        }
    }
}
