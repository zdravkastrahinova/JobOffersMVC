using JobOffersMVC.Models;
using JobOffersMVC.ViewModels;
using System.Collections.Generic;

namespace JobOffersMVC.Services.ModelServices.Abstractions
{
    public interface IBaseService<TModel, TDetailsViewModel, TEditViewModel>
        where TModel : BaseModel
        where TDetailsViewModel : BaseViewModel
        where TEditViewModel : BaseViewModel
    {
        List<TDetailsViewModel> GetAll();

        TDetailsViewModel GetDetails(int id);

        TEditViewModel GetById(int id);

        void Insert(TEditViewModel model);

        void Update(TEditViewModel model);

        void Delete(int id);
    }
}
