using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Storage.Annotations;
using Storage.Commands;
using Storage.Model;

namespace Storage.ViewModel
{
    public sealed class NewComingViewModel : DependencyObject, INotifyPropertyChanged
    {
        public CollectionView ProductEntries { get; set; }
        public ObservableCollection<ComingProduct> ProductCount { get; set; }
        public CollectionView ProviderEntries { get; set; }
        public Coming Coming { get; set; }
        public ComingProduct ComingProduct { get; set; }
        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand AddProduct { get; set; }
        public ICommand NewProduct { get; set; }
        public ICommand NewProvider { get; set; }
        public String Provider { get; set; }
        
        public NewComingViewModel(Coming coming = null)
        {
            ProductEntries = new CollectionView(Sql.GetTable<Product>());
            ProviderEntries = new CollectionView(Sql.GetTable<Provider>().Select(s => s.Name));
            ComingProduct = new ComingProduct();
            if (coming == null)
            {
                Coming = new Coming() {InvoiceDate = DateTime.Now};
                ProductCount = new ObservableCollection<ComingProduct>();

                var firstOrDefault = Sql.GetTable<Coming>().OrderByDescending(o => o.InvoiceNumber).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    Coming.InvoiceNumber = firstOrDefault.InvoiceNumber + 1;
                }
            }
            else
            {
                Coming = coming;
                ProductCount = new ObservableCollection<ComingProduct>(Sql.GetTable<ComingProduct>()
                    .Where(w => w.IdComing == coming.IdComing));
                OnPropertyChanged(nameof(ProductCount));
                Provider = Sql.GetTable<Provider>().First(f => f.IdProvider == coming.IdProvider).Name;
            }

            NewProduct = new SimpleCommand(() =>
            {
                ShowWin.AddedProduct(sumbit: new SimpleCommand(() =>
                {
                    ProductEntries = new CollectionView(Sql.GetTable<Product>());
                    OnPropertyChanged(nameof(ProductEntries));
                }));
            });
            NewProvider = new SimpleCommand(() =>
            {
                ShowWin.AddedProvider(sumbit: new SimpleCommand(() =>
                {
                    ProviderEntries = new CollectionView(Sql.GetTable<Provider>().Select(s => s.Name));
                    OnPropertyChanged(nameof(ProviderEntries));
                }));
            });
            AddProduct = new SimpleCommand(() =>
            {
                if (ComingProduct.Count <= 0) return;
                ComingProduct.IdProduct = (ProductEntries.CurrentItem as Product).IdProduct;
                if (ProductCount.Select(s => s.IdProduct).Contains(ComingProduct.IdProduct)) return;
                ProductCount.Add(new ComingProduct()
                {
                    Count = ComingProduct.Count,
                    IdProduct = ComingProduct.IdProduct
                });
            });
            if (coming == null)
                Save = new SimpleCommand(() =>
                {
                    Coming.IdProvider = Sql.GetValue<Provider>(Provider).IdProvider;
                    Sql.Add(Coming);
                    int id = Sql.GetTable<Coming>().OrderByDescending(o => o.IdComing).First().IdComing;

                    foreach (var product in ProductCount)
                    {
                        product.IdComing = id;
                        Sql.Add(product);
                        var prod = Sql.GetTable<Product>()
                            .First(f => f.IdProduct == product.IdProduct);
                        prod.Count += product.Count;
                        Sql.Update(prod);
                    }
                    ShowWin.ShowComing();
                });
            else
                Save = new SimpleCommand(() =>
                {
                    Sql.GetTable<ComingProduct>()
                        .Where(w => w.IdComing == Coming.IdComing)
                        .ToList()
                        .ForEach(d =>
                        {
                            var prod = Sql.GetTable<Product>()
                                .First(f => f.IdProduct == d.IdProduct);
                            prod.Count -= d.Count;
                            Sql.Update(prod);
                            Sql.Delete(d);
                        });
                    Coming.IdProvider = Sql.GetValue<Provider>(Provider).IdProvider;
                    
                    Sql.Update(Coming);
                    foreach (var product in ProductCount)
                    {
                        product.IdComing = Coming.IdComing;
                        Sql.Add(product);
                        var prod = Sql.GetTable<Product>()
                            .First(f => f.IdProduct == product.IdProduct);
                        prod.Count += product.Count;
                        Sql.Update(prod);
                    }
                    ShowWin.ShowComing();
                });

            Cancel = new SimpleCommand(ShowWin.ShowComing);
        }

        public NewComingViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
