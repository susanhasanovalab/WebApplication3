using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class AuthWithIdentity
    {
        public class RegisterUser
        {
            [Required(ErrorMessage = "Please enter fullname.")]
            public string FullName { get; set; }
            [Required(ErrorMessage = "Please enter email."), EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please enter password.")]
            public string Password { get; set; }
        }
    }
    public class LoginUser
    {
        [Required(ErrorMessage = "Please enter email."), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}
