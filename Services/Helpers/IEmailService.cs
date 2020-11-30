using JobOffersMVC.ViewModels;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Helpers
{
    public interface IEmailService
    {
        Task SendAsync(EmailViewModel model);
    }
}
