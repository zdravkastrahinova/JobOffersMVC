using System.Collections.Generic;
using System.Linq;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobOffersMVC.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel, new()
    {
        private readonly JobOffersDbContext dbContext;
        private readonly DbSet<T> dbSet;

        protected BaseRepository(JobOffersDbContext context)
        {
            dbContext = context;
            dbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T item)
        {
            dbSet.Add(item);

            dbContext.SaveChanges();
        }

        public void Update(T item)
        {
            T element = GetById(item.Id);

            if (element != null)
            {
                dbContext.Entry(element).State = EntityState.Detached;
            }

            dbContext.Entry(item).State = EntityState.Modified;

            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            dbSet.Remove(GetById(id));

            dbContext.SaveChanges();
        }
    }
}
