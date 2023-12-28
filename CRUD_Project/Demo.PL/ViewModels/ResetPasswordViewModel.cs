using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Mnimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }



        [Required(ErrorMessage = "Password is Required")]
        [Compare(nameof(newPassword), ErrorMessage = "Confirm Password not matched Password")]
        [DataType(DataType.Password)] 
        public string ConfirmPassword { get; set; }

    //    public string Token { get; set; }
    //    public string Email { get; set; }
    //
    }
}
