using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static WebApplication3.Models.AuthWithIdentity;
using WebApplication3.Models;
using System;
using WebApplication3.Migrations;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser regUser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser // Fixed the error by replacing 'AppUser' with 'ApplicationUser'
                {
                    UserName = regUser.Email,
                    Email = regUser.Email,
                    FullName = regUser.FullName
                };

                var result = await _userManager.CreateAsync(user, regUser.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(regUser);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }

            return View(loginUser);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
    }
}