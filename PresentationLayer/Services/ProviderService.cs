using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using BuissnesLayer;
using DataLayer;
using PresentationLayer.Models;

namespace PresentationLayer.Services
{
    public class ProviderService
    {
        private DataManager _dataManager;
        
        public ProviderService(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }
        
        public List<ProviderModel> GetAll( int skip = 0, int count = 0 )
        {   
            return  _dataManager.Providers.GetAll(false, null, skip, count).Select(ProviderDbToModel).ToList();
        }

        public int GetCount()
        {
            return _dataManager.Providers.GetCount();
        }

        private ProviderModel ProviderDbToModelById(int providerId)
        {
            var provider = _dataManager.Providers.GetById(providerId, true);

            return ProviderDbToModel(provider);
        }

        private ProviderModel ProviderDbToModel(Provider provider)
        {
            return new ProviderModel
            {
                Id = provider.Id,
                Address = provider.Address,
                Email = provider.Email,
                Name = provider.Name,
                Note = provider.Note,
                Phone = provider.Phone
            };
        }

        public ProviderModel Save(ProviderModel provider)
        {
            Provider providerDb = ProviderModelToDb(provider);
            
            _dataManager.Providers.Save(providerDb);

            return ProviderDbToModel(providerDb);
        }

        private Provider ProviderModelToDb(ProviderModel provider)
        {
            var providerDb = provider.Id != 0 ? _dataManager.Providers.GetById(provider.Id) : new Provider();
            providerDb.Address = provider.Address;
            providerDb.Email = provider.Email;
            providerDb.Name = provider.Name;
            providerDb.Note = provider.Note;
            providerDb.Phone = provider.Phone;

            return providerDb;
        }


    }
}
