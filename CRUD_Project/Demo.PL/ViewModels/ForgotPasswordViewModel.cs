using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ForgotPasswordViewModel
	{

		[Required(ErrorMessage = "Email is Required")]
		[DataType(DataType.EmailAddress)] 
		public string Email { get; set; }

	}
}
