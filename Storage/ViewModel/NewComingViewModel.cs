using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using PresentationLayer;
using PresentationLayer.Models;
using Storage.Annotations;
using Storage.Commands;


namespace Storage.ViewModel
{
    public sealed class NewComingViewModel : ViewModelBase
    {
        public CollectionView ProductEntries => new CollectionView(_services!=null ?_services.Products.GetAll():new List<ProductModel>());
        public CollectionView ProviderEntries => new CollectionView(_services != null ? _services.Providers.GetAll().Select(s=>s.Name): new List<String>());
        public ComingModelEdit Coming { get; set; }
        public ProductCountModel ProductCount { get; set; }

        public CollectionView ProductCounts => new CollectionView(
            Coming != null ? Coming.ProductCounts : new List<ProductCountModel>());
        public ICommand Save { get; set; }
        public ICommand Cancel => new SimpleCommand(ShowWin.Back);
        public ICommand AddProduct => new SimpleCommand(() =>
        {
            if (ProductCount.Count <= 0 || ProductCount.Product == null) return;
            if (Coming.ProductCounts.Select(s => s.Product.Id).Contains(ProductCount.Product.Id)) return;
            Coming.ProductCounts.Add(new ProductCountModel()
            {
                Count = ProductCount.Count,
                Product = ProductCount.Product
            });
            OnPropertyChanged(nameof(ProductCounts));
        }, (_) => ProductCount != null && ProductCount.Count > 0 && ProductCount.Product != null);
        public ICommand NewProduct => new SimpleCommand(() =>
        {
            ShowWin.AddedProduct(sumbit: new SimpleCommand(() =>
                OnPropertyChanged(nameof(ProductEntries))));
        });
        public ICommand NewProvider => new SimpleCommand(() =>
        {
            ShowWin.AddedProvider(sumbit: new SimpleCommand(() =>
                OnPropertyChanged(nameof(ProviderEntries))));
        });

        private ServicesManager _services;
        private void Init(ServicesManager services)
        {
            _services = services;
            ProductCount = new ProductCountModel();
            
            Save = new SimpleCommand(() =>
            {
                services.Comings.Save(Coming);
                ShowWin.Back();
            });
        }
        
        public NewComingViewModel(ServicesManager services, int comingId)
        {
            Init(services);
            Coming = services.Comings.GetComing(comingId);
        }

        public NewComingViewModel(ServicesManager services)
        {
            Init(services);
            Coming = new ComingModelEdit() { InvoiceDate = DateTime.Now.Date };
        }

        public NewComingViewModel() { }



    }
}
