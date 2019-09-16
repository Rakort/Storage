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
    public class EfProductRepository : EfRepository<Product>
    {
        public EfProductRepository(EFDBContext context) : base(context)
        {
        }

        protected override DbSet<Product> DbSet => _context.Product;
        protected override IQueryable<Product> GetQueryable(bool includeMaterials = false)
        {
            return DbSet;
        }

        protected override IQueryable<Product> GetOrderBy(IQueryable<Product> items)
        {
            return items.OrderBy(c => c.ProductName);
        }
    }
}
