using DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;
using AppContext = DataAccess.AppContext;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppContext db;

        public AccountController(IConfiguration configuration)
        {
            db = new AppContext(configuration["ConnectionString"]);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user.Login);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userLogin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {   
                    User user = await db.Users.FirstOrDefaultAsync((t => t.Login == model.Login));
                    if (user == null)
                    {
                            db.Users.Add(new DataAccess.User
                            { 
                                Login = model.Login, 
                                Name = model.Name,
                                Password = model.Password, 
                                Phone = model.Phone ,
                                Email = model.Email
                            });
                            await db.SaveChangesAsync();
                        return RedirectToAction("Login", "Account");
                    }
                ModelState.AddModelError("", "Login is already taken");
            }
            return View (model);
        }


        [Authorize]
        public async Task<IActionResult> Settings(SettingsModel newUserValues)
        {
            var userLogin = User.Identity.Name;
            var user = await db.Users.FirstOrDefaultAsync(t => t.Login == userLogin);

            if (newUserValues != null)
            {
                if (user.Password == newUserValues.OldPassword)
                {
                    user.Password = newUserValues.NewPassword;
                    await db.SaveChangesAsync();
                }


                return View(user);
            }

            return View(user);
        }
    }
}

