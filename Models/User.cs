using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models
{
    public class User : IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public int? CustomerID { get; set; }
        public Customer Customer { get; set; }

        [NotMapped]
        public IList<String> RoleNames { get; set; }

        public User() { }
        public User(string email)
        {
            Email = email;
        }
    }
}
