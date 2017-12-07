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
    public class SkillController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SkillSet> Skill_List()
        {
            using (var context = new WorkScheduleContext())
            {
                var results = from x in context.Skills
                              select new SkillSet
                              {
                                  SkillId = x.SkillID,
                                  SkillName = x.Description
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Skills> Skills_List()
        {
            using (var context = new WorkScheduleContext())
            {
                return context.Skills.ToList();
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
