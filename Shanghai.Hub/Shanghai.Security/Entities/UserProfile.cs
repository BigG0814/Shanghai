using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanghai.Security.Entities
{
    public class UserProfile
    {
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; internal set; }
        public int? EmployeeId { get; set; }
        public string FullName { get; internal set; }
        public IEnumerable<string> RoleMemberships { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
