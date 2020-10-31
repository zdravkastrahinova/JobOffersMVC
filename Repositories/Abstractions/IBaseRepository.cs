using System.Collections.Generic;

namespace JobOffersMVC.Repositories.Abstractions
{
    public interface IBaseRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(int id);
    }
}
