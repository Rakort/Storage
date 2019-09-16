using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EFDBContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Coming> Coming { get; set; }
        public DbSet<ProductCount> ProductCount { get; set; }
        public DbSet<Writeoff> Writeoff { get; set; }
        public DbSet<Provider> Provider { get; set; }

        //public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }
        public EFDBContext()
            : base("DbConnection")
        { }
        
    }




   
}
