using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Storage.Commands;
using Storage.Model;

namespace Storage.ViewModel
{
    public class ComingViewModel : DependencyObject
    {
        public ICommand NewComing { get; set; }
        public ICommand ApplyFilter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CollectionView ComingEntries { get; set; }
        public ComingViewModel()
        {
            NewComing = new SimpleCommand((b) =>
            {
                if (Convert.ToBoolean(b))
                    ShowWin.ShowNewComing();
                else
                    ShowWin.ShowNewComing(ComingEntries.CurrentItem as Coming);
            });
            ComingEntries = new CollectionView(Sql.GetTable<Coming>());
            ApplyFilter = new SimpleCommand(() =>
            {
                ComingEntries.Filter = null;
                ComingEntries.Filter = ComingFilter;
            });
        }

        private bool ComingFilter(object obj)
        {
            var coming = (obj as Coming);
            if (coming == null)
                return false;
            if (StartDate != new DateTime())
                if (coming.InvoiceDate < StartDate && coming.InvoiceDate > EndDate) return false;
            return true;
        }
    }
}
