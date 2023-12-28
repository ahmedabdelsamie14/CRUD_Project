using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationModel> _userManager;
		private readonly SignInManager<ApplicationModel> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationModel> userManager , SignInManager<ApplicationModel> signInManager , IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }

		public async Task<IActionResult> Index(string searchValue)
		{
			if (string.IsNullOrEmpty(searchValue))
			{
				var Users = _userManager.Users.Select(U => new UserViewModel() { Id = U.Id , FName = U.Fname
				 , LName = U.LName , Email = U.Email , PhoneNumber = U.PhoneNumber , Roles = _userManager.GetRolesAsync(U).Result});
				return View(Users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(searchValue); 

				var mapedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.Fname
				 ,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel>() { mapedUser });
			}

		}

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest(); //400

			var user = await _userManager.FindByIdAsync(id);
			
			if(user is null)
			{
				return NotFound();
			}

			var mappedUser = _mapper.Map<ApplicationModel , UserViewModel>(user);

            return View(viewName, mappedUser);

        }

        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel updatedUser)
        {
			if(id != updatedUser.Id)
			{
				return BadRequest();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var mappedUser = _mapper.Map<UserViewModel, ApplicationModel>(updatedUser);
					await _userManager.UpdateAsync(mappedUser);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.ToString());
				}
			}

            return View(updatedUser);
        }
    }
}
