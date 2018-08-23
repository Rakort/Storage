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
using Storage.Annotations;
using Storage.Commands;
using Storage.Model;

namespace Storage.ViewModel
{
    public sealed class NewWriteoffViewModel : DependencyObject, INotifyPropertyChanged
    {
        public CollectionView StorageEntries { get; set; }
        public ObservableCollection<WriteoffProduct> ProductCount { get; set; }
        public ICommand AddProduct { get; set; }
        public Writeoff Writeoff { get; set; }
        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }

        public bool Show { get; set; }

        public NewWriteoffViewModel()
        {
        }

        public NewWriteoffViewModel(Writeoff writeoff = null)
        {
            Show = false;
            Writeoff = new Writeoff();
            ProductCount = new ObservableCollection<WriteoffProduct>();
            StorageEntries = new CollectionView(Sql.GetTable<Product>().Where(w => w.Count > 0));

            AddProduct = new SimpleCommand(() =>
            {
                var id = (StorageEntries.CurrentItem as Product).IdProduct;
                if (ProductCount.Count(w => w.IdProduct == id) == 0)
                    ProductCount.Add(new WriteoffProduct() {IdProduct = id, Count = 1});
            });

            Cancel = new SimpleCommand(ShowWin.ShowWriteoff);

            if (writeoff == null)
                Save = new SimpleCommand(() =>
                {
                    Writeoff.Date = DateTime.Now;
                    Sql.Add(Writeoff);
                    int id = Sql.GetTable<Writeoff>().OrderByDescending(o => o.IdWriteoff).First().IdWriteoff;
                    foreach (var product in ProductCount)
                    {
                        product.IdWriteoff = id;
                        Sql.Add(product);
                        var prod = Sql.GetTable<Product>()
                            .First(f => f.IdProduct == product.IdProduct);
                        prod.Count -= product.Count;
                        Sql.Update(prod);
                    }
                    ShowWin.ShowWriteoff();
                });
            else
            {
                Writeoff = writeoff;
                ProductCount = new ObservableCollection<WriteoffProduct>(Sql.GetTable<WriteoffProduct>()
                    .Where(w => w.IdWriteoff == writeoff.IdWriteoff));
                OnPropertyChanged(nameof(ProductCount));
                Save = new SimpleCommand(() =>
                {
                    Sql.Update(Writeoff);

                    Sql.GetTable<WriteoffProduct>()
                        .Where(w => w.IdWriteoff == writeoff.IdWriteoff)
                        .ToList()
                        .ForEach(d =>
                        {
                            var prod = Sql.GetTable<Product>()
                                .First(f => f.IdProduct == d.IdProduct);
                            prod.Count += d.Count;
                            Sql.Update(prod);
                            Sql.Delete(d);
                        });

                    foreach (var product in ProductCount)
                    {
                        product.IdWriteoff = writeoff.IdWriteoff;
                        Sql.Add(product);
                        var prod = Sql.GetTable<Product>()
                            .First(f => f.IdProduct == product.IdProduct);
                        prod.Count -= product.Count;
                        Sql.Update(prod);
                    }
                    ShowWin.ShowWriteoff();
                });
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
