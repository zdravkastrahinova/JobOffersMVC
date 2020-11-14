using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.UserApplications;

namespace JobOffersMVC.Services.ModelServices.Abstractions
{
    public interface IUserApplicationsService : IBaseService<UserApplication, UserApplicationDetailsViewModel, UserApplicationEditViewModel>
    {
    }
}
