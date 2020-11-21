using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersService usersService;

        public AuthController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        public IActionResult Login()
        {
            return View();
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserDetailsViewModel user = usersService.GetByUsernameAndPassword(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("BadCredentials", "Invalid username or password.");

                return View(model);
            }

            HttpContext.Session.SetInt32("loggedUserId", user.Id); // JsonSerializer.Serialize(user)

            return RedirectToAction("List", "Users");
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        public IActionResult Register()
        {
            return View();
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            usersService.Insert(model);

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            
            return RedirectToAction("Login");
        }
    }
}
