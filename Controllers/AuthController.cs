using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        { }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            UsersRepository usersRepository = new UsersRepository();
            User user = usersRepository.GetByUsernameAndPassword(model.Username, model.Password);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            UsersRepository usersRepository = new UsersRepository();
            usersRepository.Insert(model);

            return RedirectToAction("Login");
        }
    }
}
