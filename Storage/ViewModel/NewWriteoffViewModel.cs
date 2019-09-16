using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using PresentationLayer;
using PresentationLayer.Models;
using Storage.Annotations;
using Storage.Commands;


namespace Storage.ViewModel
{
    public sealed class NewWriteoffViewModel : ViewModelBase
    {
        #region Fields
        private ServicesManager _services;
        public WriteoffModelEdit Writeoff { get; set; }
        public ProductCountModel ProductCount { get; set; }

        public CollectionView StorageEntries => new CollectionView( _services == null? new List<ProductModel>() : _services.Products.GetAll(Availability.Available));
        public ObservableCollection<ProductCountModel> ProductCounts
        {
            get =>
                new ObservableCollection<ProductCountModel>(
                    Writeoff != null ? Writeoff.ProductCounts : new List<ProductCountModel>());
            set => Writeoff.ProductCounts = value.ToList();
        }

        #endregion

        #region Commands

        public ICommand AddProduct => new SimpleCommand(() =>
        {
            //if (ProductCount.Count <= 0 || ProductCount.Product == null) return;
            //if (Writeoff.ProductCounts.Select(s => s.Product.Id).Contains(ProductCount.Product.Id)) return;
            Writeoff.ProductCounts.Add(new ProductCountModel()
            {
                Count = 1,
                Product = StorageEntries.CurrentItem as ProductModel
            });
            OnPropertyChanged(nameof(ProductCounts));
        });

        public ICommand Save => new SimpleCommand(() =>
        {
            _services.Writeoffs.Save(Writeoff);
            ShowWin.Back();
        });
        public ICommand Cancel => new SimpleCommand(ShowWin.Back);

        #endregion
        
        private void Init(ServicesManager services)
        {
            _services = services;
            ProductCount = new ProductCountModel();
            
        }

        public NewWriteoffViewModel()
        {
        }
        
        public NewWriteoffViewModel(ServicesManager services)
        {
            Init(services);
            Writeoff = new WriteoffModelEdit {Date = DateTime.Now};
        }

        public NewWriteoffViewModel(ServicesManager services, int writeoffId)
        {
            Init(services);
            Writeoff = services.Writeoffs.GetWriteoff(writeoffId);
        }
    }
}
