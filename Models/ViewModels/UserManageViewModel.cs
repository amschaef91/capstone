using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Models.ViewModels
{
    public class UserManageViewModel
    {
        public ChangePasswordViewModel PasswordViewModel { get; set; }
        public ChangeEmailViewModel EmailViewModel { get; set; }

        public User User { get; set; }
        public Customer Customer { get; set; }
    }
}
