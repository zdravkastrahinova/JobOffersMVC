using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.UserApplications;

namespace JobOffersMVC.Services.ModelServices.Implementations
{
    public class UserApplicationsService : BaseService<UserApplication, UserApplicationDetailsViewModel, UserApplicationEditViewModel>, IUserApplicationsService
    {
        public UserApplicationsService(IUserApplicationsRepository userApplicationsRepository, IMapper mapper)
            : base (userApplicationsRepository, mapper)
        {

        }
    }
}
