using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BuissnesLayer.Implementations
{
    public class EfProductCountRepository : EfRepository<ProductCount>
    {
        public EfProductCountRepository(EFDBContext context) : base(context)
        {
        }

        protected override DbSet<ProductCount> DbSet => _context.ProductCount;
        protected override IQueryable<ProductCount> GetQueryable(bool includeMaterials = false)
        {
             return _context.Set<ProductCount>().Include(x => x.Product).AsNoTracking();
        }

        protected override IQueryable<ProductCount> GetOrderBy(IQueryable<ProductCount> items)
        {
            return items.OrderBy(c => c.Product.ProductName);
        }
    }
}
