using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shanghai.Data.Entities;
using Shanghai.Security.DAL;
using Shanghai.Security.Entities;
using Shanghai.System;
using Shanghai.System.BLL;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Security.BLL
{
    [DataObject]
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }

        public void AddPOSSYSTEM()
        {
            // Add a POSSYSTEM user, if one doesn't exist
            string username = ConfigurationManager.AppSettings["posUserName"];
            if (!Users.Any(u => u.UserName.Equals(username)))
            {
                var posAccount = new ApplicationUser()
                {
                    UserName = username,
                    Email = ConfigurationManager.AppSettings["posEmail"],
                    EmailConfirmed = true
                };
                this.Create(posAccount, ConfigurationManager.AppSettings["posPassword"]);
                this.AddToRole(posAccount.Id, ConfigurationManager.AppSettings["posRole"]);
            }
        }

        public void AddManagerOne()
        {
            // Add a default Manager user, if one doesn't exist
            string username = "ManagerOne";
            if (!Users.Any(u => u.UserName.Equals(username)))
            {
                var managerAccount = new ApplicationUser()
                {
                    UserName = username,
                    Email = "shanghaiappsystem@gmail.com",
                    EmailConfirmed = true
                };
                this.Create(managerAccount, ConfigurationManager.AppSettings["newUserPassword"]);
                this.AddToRole(managerAccount.Id, ConfigurationManager.AppSettings["manageRole"]);
            }
        }

        #region Standard CRUD Operations for Users
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<UserProfile> ListAllUsers()
        {
            var rm = new RoleManager();
            // The UserManager for ASP.Net Identity has a built-in property for all users
            var result = from person in Users.ToList()
                         select new UserProfile
                         {
                             UserId = person.Id,
                             UserName = person.UserName,
                             Email = person.Email,
                             EmailConfirmed = person.EmailConfirmed,
                             CustomerId = person.CustomerId,
                             EmployeeId = person.EmployeeId,
                             RoleMemberships = person.Roles.Select(r => rm.FindById(r.RoleId).Name)
                         };

            using (var context = new ShanghaiContext())
            {
                foreach (var person in result)
                    if (person.EmployeeId.HasValue)
                        person.FullName = context.Employees.Find(person.EmployeeId).FullName;
            }

            return result.ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddUser(UserProfile userInfo, string role)
        {
            var userAccount = new ApplicationUser()
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email,
                EmployeeId = userInfo.EmployeeId
            };

            this.Create(userAccount, ConfigurationManager.AppSettings["newUserPassword"]);

            if (role == "Employees")
                this.AddToRole(userAccount.Id, ConfigurationManager.AppSettings["employeeRole"]);
            else if (role == "Managers")
                this.AddToRole(userAccount.Id, ConfigurationManager.AppSettings["manageRole"]);

        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void RemoveUser(UserProfile userInfo)
        {
            var user = Users.Single(u => u.EmployeeId == userInfo.EmployeeId);
            if (user.UserName == ConfigurationManager.AppSettings["adminUserName"])
                throw new Exception("The webmaster account cannot be removed");
            this.Delete(this.FindById(user.Id));
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateUser(UserProfile userInfo, string role)
        {
            ApplicationUser user = Users.Where(u => u.EmployeeId == userInfo.EmployeeId).First();
            user.UserName = userInfo.UserName;
            user.Email = userInfo.Email;
            if(this.IsInRole(user.Id, "Employees"))
            {
                this.RemoveFromRole(user.Id, ConfigurationManager.AppSettings["employeeRole"]);
            }
            else if(this.IsInRole(user.Id, "Managers"))
            {
                this.RemoveFromRole(user.Id, ConfigurationManager.AppSettings["manageRole"]);
            }
            if (role == "Employees")
                this.AddToRole(user.Id, ConfigurationManager.AppSettings["employeeRole"]);
            else if (role == "Managers")
                this.AddToRole(user.Id, ConfigurationManager.AppSettings["manageRole"]);

            this.Update(user);
        }

        public Employee GetEmployeeByLoggedInUser(string username)
        {
            ApplicationUser user = Users.Where(x => x.UserName == username).First();
            int EmpID = user.EmployeeId.Value;
            EmployeeController sysmgr = new EmployeeController();
            Employee person = sysmgr.Employee_GetByEmployeeID(EmpID);
            return person;
        }

        public List<int> GetManagerUsers()
        {
            List<int> managers = new List<int>();
            List<ApplicationUser> users = Users.ToList();
            foreach(var user in users)
            {
                if(this.IsInRole(user.Id, "Managers"))
                {
                    managers.Add(user.EmployeeId.Value);
                }
            }

            return managers;
        }
        #endregion
    }
}
