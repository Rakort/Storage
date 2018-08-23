using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Data;
using System.Windows.Input;
using Storage.Commands;
using Storage.Model;


namespace Storage.ViewModel
{
    public class StorageViewModel : DependencyObject
    {
        public ICommand EditProduct { get; set; }
        public ICommand ApplyFilter { get; set; }
        public string FindProduct { get; set; }
        public CollectionView StorageEntries { get; set; }
        public CollectionView AvailabilityEntries { get; set; }

        public StorageViewModel()
        {
            StorageEntries = new CollectionView(Sql.GetTable<Product>());

            EditProduct = new SimpleCommand(() =>
                ShowWin.AddedProduct(Sql.GetValue<Product>((StorageEntries.CurrentItem as Product).IdProduct)));
            AvailabilityEntries = new CollectionView(
                new List<string>() {"Все", "Только в наличии", "Нет в наличии", "Ниже минимального остатка"});
            ApplyFilter = new SimpleCommand(() =>
            {
                StorageEntries.Filter = null;
                StorageEntries.Filter = StorageFilter;
            });
            FindProduct = "";
        }

        private bool StorageFilter(object obj)
        {
            var storage = (obj as Product);
            if (storage == null)
                return false;

            if (!storage.ProductName.ToLower().Contains(FindProduct.ToLower()))
                return false;

            var ava = AvailabilityEntries.CurrentItem.ToString();
            switch (ava)
            {
                case "Только в наличии": return storage.Count > 0;
                case "Нет в наличии": return storage.Count == 0;
                case "Ниже минимального остатка": return storage.Count < storage.MinCount;
                default: return true;
            }
        }


    }


}
