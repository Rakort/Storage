using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BuissnesLayer.Interfaces
{
    public interface IRepository<T> where T:Table
    {
        IEnumerable<T> GetAll(bool includeMaterials = false, Expression<Func<T, bool>> expression = null, int skip = 0, int count = 0);
        T GetById(int id, bool includeMaterials = false);
        T GetByExpression(Expression<Func<T, bool>> expression, bool includeMaterials = false);
        int GetCount(Expression<Func<T, bool>> expression = null);
        void Save(T item);
        void Delete(T item);
    }

   
}
