using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Services;
using JobOffersMVC.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class AuthController : Controller
    {
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

            UsersRepository usersRepository = new UsersRepository();
            User user = usersRepository.GetByUsernameAndPassword(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("BadCredentials", "Invalid username or password.");

                return View(model);
            }

            AuthenticationService.LoggedUser = user;

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
            // 1. Validate model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 2. Map view model to User model
            User user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            UsersRepository usersRepository = new UsersRepository();
            usersRepository.Insert(user);

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            AuthenticationService.LoggedUser = null;
            
            return RedirectToAction("Login");
        }
    }
}
