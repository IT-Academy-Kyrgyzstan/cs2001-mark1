using DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                    await Authenticate(user.Name);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User ucheck = null;
                using (AppContext db = new DataAccess.AppContext())
                {
                    ucheck = db.Users.FirstOrDefault(t => t.Login == model.Login);


                    if (ucheck == null)
                    {
                        //if (model.Password != model.ConfirmPassword)
                        //{
                        //    ModelState.AddModelError("", "Пароли не совпадают");
                        //}
                        //else
                        //{
                            db.Users.Add(new DataAccess.User { Login = model.Login, Name = model.Name, Password = model.Password, Phone = model.Phone });
                            db.SaveChanges();
                        
                    }

                }
            }
            return View("Model");
        }
    }
}
