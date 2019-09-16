using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Data;
using System.Windows.Input;
using PresentationLayer;
using PresentationLayer.Models;
using Storage.Commands;



namespace Storage.ViewModel
{
    public class StorageViewModel : ViewModelPage
    {
        public ICommand EditProduct => new SimpleCommand(() =>
            ShowWin.AddedProduct(StorageEntries.CurrentItem as ProductModel));
        public ICommand ApplyFilter => new SimpleCommand(() =>Refresh.Execute(null));
        public override ICommand Refresh => new SimpleCommand(() => OnPropertyChanged(nameof(StorageEntries)));
        public string FindProduct { get; set; }

        public CollectionView StorageEntries {
            get
            {
                if (_services == null) return new CollectionView(new List<ProductModel>());
                switch (SelectionAvailability)
                {
                    case "Только в наличии": availability = Availability.Available; break;
                    case "Нет в наличии": availability = Availability.NotAvailable; break;
                    case "Ниже минимального остатка": availability = Availability.BelowMinBalance; break;
                    default: availability = Availability.All; break;
                }

                PageCount = (int) Math.Ceiling(_services.Products.GetCount(availability, FindProduct) /
                                               (double) CountItemInPage);
                return new CollectionView(_services.Products.GetAll(availability, FindProduct, 
                    (CurrentPage - 1) * CountItemInPage, CountItemInPage));
            }
        }
        public CollectionView AvailabilityEntries { get; set; }
        public string SelectionAvailability { get; set; }

        private readonly ServicesManager _services;
        public StorageViewModel(ServicesManager services)
        {
            _services = services;
            AvailabilityEntries = new CollectionView(
                new List<string>() {"Все", "Только в наличии", "Нет в наличии", "Ниже минимального остатка"});
            FindProduct = "";
            CountItemInPage = 10;

        }

        public StorageViewModel(){}

        private Availability availability;
       


    }

    


}
