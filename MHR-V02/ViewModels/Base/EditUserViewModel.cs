using System.ComponentModel.DataAnnotations;

namespace MHR_V02.ViewModels.Base
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
