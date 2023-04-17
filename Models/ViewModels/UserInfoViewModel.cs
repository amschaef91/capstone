using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PersonalProject.Models.ViewModels
{
    public class UserInfoViewModel
    {
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public User User { get; set; }
    }
}
