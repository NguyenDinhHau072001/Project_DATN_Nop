using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ProjectDATN.Web.ViewModel
{
    public class SignupViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
       
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email address is missing or invalid")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password,ErrorMessage ="Password incorect")]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
