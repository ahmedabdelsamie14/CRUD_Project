using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class SignUpViewModel
	{

   


        [Required(ErrorMessage ="UserName is Required")]
        public string UserName { get; set; }

		[Required(ErrorMessage = "FirstName is Required")]
		public string FName { get; set; }

		[Required(ErrorMessage = "LastName is Required")]
		public string LName { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5,ErrorMessage ="Mnimum Password Length is 5")]
		[DataType(DataType.Password)] //34an yzhar k astreks
		public string Password { get; set; }



		[Required(ErrorMessage = "Password is Required")]
		[Compare(nameof(Password),ErrorMessage ="Confirm Password not matched Password")]
		[DataType(DataType.Password)] //34an yzhar k astreks
		public string ConfirmPassword { get; set; }


        public bool IsAgree  { get; set; }









    }
}
