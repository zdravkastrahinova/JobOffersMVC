using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Services;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult List()
        {
            UsersListViewModel model = new UsersListViewModel();
            model.Users = usersService.GetAll();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("List");
            }

            UserDetailsViewModel model = usersService.GetDetails(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View(new UserEditViewModel());
            }

            UserEditViewModel model = usersService.GetById(id.Value);
            if (model == null)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id == 0) // create
            {
                usersService.Insert(model);

                return RedirectToAction("List");
            }

            UserEditViewModel user = usersService.GetById(model.Id);
            if (user == null)
            {
                return RedirectToAction("List");
            }

            usersService.Update(model);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            usersService.Delete(id.Value);

            return RedirectToAction("List");
        }

        public IActionResult Upload(IFormFile ProfileImage)
        {
            int loggedUserId = HttpContext.Session.GetInt32("loggedUserId").Value;

            usersService.UploadImage(loggedUserId, ProfileImage);

            return RedirectToAction("Details", new { id = loggedUserId });
        }
    }
}