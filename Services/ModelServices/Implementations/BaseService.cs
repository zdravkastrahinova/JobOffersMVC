using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersMVC.Services.ModelServices.Implementations
{
    public class BaseService<TModel, TDetailsViewModel, TEditViewModel> : IBaseService<TModel, TDetailsViewModel, TEditViewModel>
        where TModel : BaseModel
        where TDetailsViewModel : BaseViewModel
        where TEditViewModel : BaseViewModel
    {

        protected readonly IBaseRepository<TModel> repository;
        protected readonly IMapper mapper;

        public BaseService(IBaseRepository<TModel> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<TDetailsViewModel> GetAll()
        {
            return repository
                .GetAll()
                .Select(model => mapper.Map<TModel, TDetailsViewModel>(model))
                .ToList();
        }

        public TEditViewModel GetById(int id)
        {
            TModel model = repository.GetById(id);

            return mapper.Map<TModel, TEditViewModel>(model);
        }

        public TDetailsViewModel GetDetails(int id)
        {
            TModel model = repository.GetById(id);

            return mapper.Map<TModel, TDetailsViewModel>(model);
        }

        public virtual void Insert(TEditViewModel viewModel)
        {
            TModel model = mapper.Map<TModel>(viewModel);

            repository.Insert(model);
        }

        public virtual void Update(TEditViewModel viewModel)
        {
            TModel model = mapper.Map<TModel>(viewModel);

            repository.Update(model);
        }
    }
}
