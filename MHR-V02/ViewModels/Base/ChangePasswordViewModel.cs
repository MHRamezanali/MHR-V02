using System.ComponentModel.DataAnnotations;

namespace MHR_V02.ViewModels.Base
{
    public class ChangePasswordViewModel
    {
        public Guid UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تایید آن یکسان نیستند.")]
        public string ConfirmPassword { get; set; }
    }

}
