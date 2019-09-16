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
    public class EfWriteoffRepository : EfRepository<Writeoff>
    {
        public EfWriteoffRepository(EFDBContext context) : base(context)
        {
        }

        protected override DbSet<Writeoff> DbSet => _context.Writeoff;

        protected override IQueryable<Writeoff> GetQueryable(bool includeMaterials = false)
        {
            if (includeMaterials)
                return _context.Set<Writeoff>().Include(y => y.ProductCounts.Select(s=>s.Product)).AsNoTracking();
            else
                return _context.Writeoff;
        }

        protected override IQueryable<Writeoff> GetOrderBy(IQueryable<Writeoff> items)
        {
            return items.OrderBy(c => c.Date);
        }

    }
}
