using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationModel> _userManager;
		private readonly SignInManager<ApplicationModel> _signInManager;

		public AccountController(UserManager<ApplicationModel> userManager , SignInManager<ApplicationModel> signInManager) 
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}


		#region Sign Up
		public IActionResult SignUp()
        {

            return View();

        }
        [HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{

         
			if(ModelState.IsValid) //Server Side Validation
            {

				var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    //Manual Mapping
                    user = new ApplicationModel()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        IsAgree = model.IsAgree,
                        Fname = model.FName,
                        LName = model.LName,

                    };
                   var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn)); // da Sign In bta3 Controller base tlama  m4 3mla action
                        
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
					return View(model);
				}
                ModelState.AddModelError(string.Empty, "User Name is Already exists");
              
            }
			return View(model);
		}
        #endregion

        #region SignIn

        public  IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]

		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe,false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index),(nameof(HomeController).Replace(nameof(Controller), string.Empty)));
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");

            }
            return View(model);
		}


        #endregion


        #region SignOut

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion

        #region ForgotPassword

        public IActionResult ForgotPassword()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = model.Email , token } , Request.Scheme) ;
                if (ModelState.IsValid)
                {
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Recipents = model.Email,
                        Body = resetPasswordUrl

                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));

                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
                
            }
            return View(model);
        }
        #endregion

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string email , string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            string email = TempData["Email"] as string;
            string token = TempData["Token"] as string;

            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ResetPasswordAsync(user, token, model.newPassword);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SignIn));

            }
            foreach(var error in result.Errors)
            {
                //await Console.Out.WriteLineAsync(error);
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }

   
}
