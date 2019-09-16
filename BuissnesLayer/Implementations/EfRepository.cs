using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BuissnesLayer.Interfaces;
using DataLayer;
using System.Data.Entity;
using EntityState = System.Data.Entity.EntityState;

namespace BuissnesLayer.Implementations
{
    public abstract class EfRepository<T> : IRepository<T> where T : Table
    {
        protected readonly EFDBContext _context;

        protected EfRepository(EFDBContext context)
        {
            _context = context;
        }

        protected abstract DbSet<T> DbSet { get; }

        protected abstract IQueryable<T> GetQueryable(bool includeMaterials = false);
        protected abstract IQueryable<T> GetOrderBy(IQueryable<T> items);


        public void Delete(T item)
        {
            DbSet.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool includeMaterials = false,
            Expression<Func<T, bool>> expression = null, int skip = 0, int count = 0)
        {
            IQueryable<T> comings = GetQueryable(includeMaterials);

            if (expression != null) comings = comings.Where(expression);
            if (skip > 0) comings = GetOrderBy(comings).Skip(skip);
            if (count > 0) comings = comings.Take(count);

            return comings.ToList();
        }

        public T GetByExpression(Expression<Func<T, bool>> expression, bool includeMaterials = false)
        {
            return GetQueryable(includeMaterials).FirstOrDefault(expression);
        }

        public int GetCount(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
                return GetQueryable().Count(expression);
            else
                return GetQueryable().Count();
        }

        public T GetById(int id, bool includeMaterials = false)
        {
            return GetByExpression(x => x.Id == id, includeMaterials);
        }

        public void Save(T item)
        {
            if (item.Id == 0)
                DbSet.Add(item);
            else
                _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }




    }
}

