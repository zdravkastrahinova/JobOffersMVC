using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class UsersController : Controller
    {
        private readonly IUsersRepository usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IActionResult List()
        {
            UsersListViewModel model = new UsersListViewModel();
            model.Users = usersRepository
                .GetAll()
                .Select(u => new UserDetailsViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).ToList();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            // 1. Id is always provided
            if (id == 0)
            {
                return RedirectToAction("List");
            }

            // 2. Get User by Id
            User user = usersRepository.GetById(id);

            if (user == null)
            {
                return RedirectToAction("List");
            }

            // 3. Map to ViewModel
            UserDetailsViewModel model = new UserDetailsViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            // 4. Return View
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            // 1. Check id param
            if (!id.HasValue)
            {
                return View(new UserEditViewModel());
            }

            // 2. Get User from DB
            User user = usersRepository.GetById(id.Value);

            if (user == null)
            {
                return RedirectToAction("List");
            }

            // 3. Map to ViewModel
            UserEditViewModel model = new UserEditViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            // 4. Return View
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel model)
        {
            // 1. Validate model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 2. Map ViewModel to Model
            User user;
            if (model.Id == 0) // create
            {
                user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                usersRepository.Insert(user);

                return RedirectToAction("List");
            }

            user = usersRepository.GetById(model.Id);

            if (user == null)
            {
                return RedirectToAction("List");
            }

            user = new User
            {
                Id = model.Id,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // 3. Update
            usersRepository.Update(user);

            // 4. Redirect
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            usersRepository.Delete(id.Value);

            return RedirectToAction("List");
        }
    }
}