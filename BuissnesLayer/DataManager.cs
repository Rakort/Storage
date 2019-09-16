using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuissnesLayer.Implementations;
using BuissnesLayer.Interfaces;
using DataLayer;

namespace BuissnesLayer
{
    public class DataManager
    {
        public DataManager(EFDBContext context)
        {
            Products = new EfProductRepository(context);
            Comings = new EfComingRepository(context);
            Writeoffs = new EfWriteoffRepository(context);
            Providers = new EFProviderRepository(context);
            ProductCounts = new EfProductCountRepository(context);
        }

        public IRepository<Product> Products { get; }
        public IRepository<Coming> Comings { get; }
        public IRepository<Provider> Providers { get; }
        public IRepository<ProductCount> ProductCounts { get; }
        public IRepository<Writeoff> Writeoffs { get; }

    }
}
