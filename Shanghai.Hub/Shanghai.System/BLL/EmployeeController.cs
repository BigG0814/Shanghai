using Microsoft.AspNet.Identity;
using Shanghai.Data.Entities;
using Shanghai.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.System.BLL
{
    [DataObject]
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        #region Add Employee
        public int Add_new_Employee(Employee employeeItem)
        {
            using (var context = new ShanghaiContext())
            {
                employeeItem.LoginCode = GetNextEmployeeLogin();  
                employeeItem = context.Employees.Add(employeeItem);
                context.SaveChanges();
                string employeeLogin = employeeItem.LoginCode.ToString();
                string employeeUsername = employeeItem.UserName.ToString();

                string body = "<div><p>Welcome to Shanghai!<p>";
                body += "<p>You are now registered into the system as a new employee. Your employee details are below:</p>";
                body += "<ul>";
                body += "<li>Your login code: " + employeeLogin +"</li>";
                body += "<li>Your username: " + employeeUsername + "</li>";
                body += "<li>Your password: password1</li>";
                body += "</ul>";
                body += "<p>Please change your password after your first log in as the above password for first access only. Your login code is used to access the POS system at the restaurant.</p>";
                body += "</div>";

                MailSender sender = new MailSender(employeeItem.Email, "Welcome", body);
                sender.SendMail();
            }
            return employeeItem.EmployeeID;
        }
        #endregion
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Employee> List_Employee()
        {
            using(var context = new ShanghaiContext())
            {
                return context.Employees.Where(x=>x.EmployeeID != 1).ToList();
            }
        }

        public Employee Employee_GetByEmployeeID(int employeeid)
        {
            using (var context = new ShanghaiContext())
            {
                return context.Employees.Find(employeeid);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int Employee_Update(Employee item)
        {
            using (var context = new ShanghaiContext())
            {
                
                var data = context.Employees.Find(item.EmployeeID);
                data.EmployeeID = item.EmployeeID;
                data.FName = item.FName;
                data.LName = item.LName;
                data.UserName = item.UserName;
                data.Street = item.Street;
                data.City = item.City;
                data.Province = item.Province;
                data.Country = item.Country;
                data.PostalCode = item.PostalCode;
                data.Email = item.Email;
                data.CellPhone = item.CellPhone;
                data.HireDate = item.HireDate;
                data.ContactName = item.ContactName;
                data.ContactPhone = item.ContactPhone;
                data.ContactRelation = item.ContactRelation;
                data.ActiveYN = item.ActiveYN;
                context.Entry(data).State = EntityState.Modified;
                return context.SaveChanges();
                
            }
        }//eom

        public int GetNextEmployeeLogin()
        {
            using(var context = new ShanghaiContext())
            {
                var allLogins = context.Employees.Select(x => x.LoginCode).ToList();
                if(allLogins.Count == 0)
                {
                    return 1510;
                }
                else
                {
                    return allLogins.Max() + 15;
                }

            }
        }

        public int Employee_Remove(int employeeid)
        {
            using (var context = new ShanghaiContext())
            {
                
                Employee existingItem = context.Employees.Find(employeeid);
                if (existingItem == null)
                {
                    throw new Exception("Employee is no longer on file.");
                }
                existingItem.ActiveYN = false;
                context.Entry(existingItem).Property(x => x.ActiveYN).IsModified = true;
                return context.SaveChanges();

                
            }
        }//eom

        public Employee GetEmployeeByLoginCode(int LoginCode)
        {
            using(var context = new ShanghaiContext())
            {
                return context.Employees.Where(x => x.LoginCode == LoginCode).FirstOrDefault();
            }
        }

        //KW - Method to list Celluar Providers (required for sending ocnfirmation text to restaurant)
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<CellularProvider> ListCellularProviders ()
        {
            using (var context = new ShanghaiContext())
            {
                return context.CellularProviders.ToList();
            }
        }
    }
}
