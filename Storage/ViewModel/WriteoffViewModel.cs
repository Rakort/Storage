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
    public class WriteoffViewModel : DependencyObject
    {
        public ICommand NewWriteoff { get; set; }
        public ICommand ApplyFilter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CollectionView WriteoffEntries { get; set; }

        public WriteoffViewModel()
        {
            NewWriteoff = new SimpleCommand((b) =>
            {
                if (Convert.ToBoolean(b))
                    ShowWin.ShowNewWriteoff();
                else
                    ShowWin.ShowNewWriteoff(WriteoffEntries.CurrentItem as Writeoff);
            });
            WriteoffEntries = new CollectionView(Sql.GetTable<Writeoff>());
            ApplyFilter = new SimpleCommand(() =>
            {
                WriteoffEntries.Filter = null;
                WriteoffEntries.Filter = WriteoffFilter;
            });
        }

        private bool WriteoffFilter(object obj)
        {
            var writeoff = (obj as Writeoff);
            if (writeoff == null)
                return false;
            if (StartDate != new DateTime())
                if (writeoff.Date < StartDate && writeoff.Date > EndDate) return false;
            return true;
        }
    }
}
