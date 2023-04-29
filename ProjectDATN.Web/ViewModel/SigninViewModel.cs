using System.ComponentModel.DataAnnotations;

namespace ProjectDATN.Web.ViewModel
{
    public class SigninViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is missing or invalid")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Password incorect")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
