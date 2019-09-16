using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BuissnesLayer.Interfaces;
using DataLayer;

namespace BuissnesLayer.Implementations
{
    public class EFProviderRepository : EfRepository<Provider>
    {
        public EFProviderRepository(EFDBContext context) : base(context)
        {
        }

        protected override DbSet<Provider> DbSet => _context.Provider;
        protected override IQueryable<Provider> GetQueryable(bool includeMaterials = false)
        {
            return DbSet;
        }

        protected override IQueryable<Provider> GetOrderBy(IQueryable<Provider> items)
        {
            return items.OrderBy(c => c.Name);
        }
    }
}
