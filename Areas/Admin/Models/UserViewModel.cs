using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Areas.Admin.Models
{
    public class UserViewModel
    {
            public IEnumerable<User> Users { get; set; } = new List<User>();
            public IEnumerable<Role> Roles { get; set; } = new List<Role>();

            public string UserId { get; set; } 
            public string RoleName { get; set; } 

    }
    
}
