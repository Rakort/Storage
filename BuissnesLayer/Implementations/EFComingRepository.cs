using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BuissnesLayer.Interfaces;
using DataLayer;

namespace BuissnesLayer.Implementations
{
    public class EfComingRepository : EfRepository<Coming>
    {
        public EfComingRepository(EFDBContext context) : base(context)
        {
        }

        protected override DbSet<Coming> DbSet => _context.Coming;

        protected override IQueryable<Coming> GetQueryable(bool includeMaterials = false)
        {
            if (includeMaterials)
                return _context.Set<Coming>().Include(x => x.Provider).Include(y => y.ProductCounts.Select(s=>s.Product)).AsNoTracking();
            else
                return _context.Set<Coming>().Include(x => x.Provider).AsNoTracking();
        }

        protected override IQueryable<Coming> GetOrderBy(IQueryable<Coming> items)
        {
            return items.OrderBy(c => c.InvoiceNumber);
        }

    }
}
