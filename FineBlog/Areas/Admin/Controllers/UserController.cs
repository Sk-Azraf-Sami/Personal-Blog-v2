﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using FineBlog.Models;
using FineBlog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notification; 
        public UserController(UserManager<ApplicationUser> userManager, 
                             SignInManager<ApplicationUser> signInManager, 
                             INotyfService notyfService)
        {
            _notification = notyfService;   
            _userManager = userManager;
            _signInManager = signInManager; 
        }
        public IActionResult Index()
        {
            return View();
        }
        
        // before page address : https://localhost:7189/admin/user/login/
        [HttpGet("Login")] // now address: https://localhost:7189/login
        public IActionResult Login()
        {
            if(!HttpContext.User.Identity!.IsAuthenticated)
            {
                return View(new LoginVM());
            }
            //If login is successful and user in admin page
            //If https://localhost:7189/login enter this address, redirect to admin page, not in login page 
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        // before page address : https://localhost:7189/admin/user/login/
        [HttpPost("Login")] // now address: https://localhost:7189/login
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == vm.Username);
            if (existingUser == null) 
            {
                _notification.Error("Username does not exist");
                return View(vm);
            }
            await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, true);
            _notification.Success("Login Successful");
            return RedirectToAction("Index", "User", new { area = "Admin" });

        }
    }
}
