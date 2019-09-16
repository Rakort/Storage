using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuissnesLayer;
using DataLayer;
using PresentationLayer.Services;

namespace PresentationLayer
{
    public class ServicesManager
    {
        DataManager _dataManager;

        public ServicesManager(DataManager dataManager)
        {
            _dataManager = dataManager;
            Comings = new ComingService(_dataManager);
            Writeoffs = new WriteoffService(_dataManager);
            Products = new ProductService(_dataManager);
            Providers = new ProviderService(_dataManager);

        }
        public ComingService Comings { get; }
        public WriteoffService Writeoffs { get; }
        public ProductService Products { get; }
        public ProviderService Providers { get; }

    }
}
