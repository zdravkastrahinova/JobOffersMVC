using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Http;

namespace JobOffersMVC.Services.ModelServices.Abstractions
{
    public interface IUsersService : IBaseService<User, UserDetailsViewModel, UserEditViewModel>
    {
        UserDetailsViewModel GetByUsernameAndPassword(string username, string password);

        void Insert(RegisterViewModel viewModel);

        void UploadImage(int userId, IFormFile formFile);
    }
}
