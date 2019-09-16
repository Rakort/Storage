using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using PresentationLayer;
using PresentationLayer.Models;
using Storage.Commands;

namespace Storage.ViewModel
{
    public class WriteoffViewModel : ViewModelPage
    {
        public override ICommand Refresh => new SimpleCommand(() => OnPropertyChanged(nameof(WriteoffEntries)));
        public ICommand NewWriteoff => new SimpleCommand((b) =>
        {
            if (b is WriteoffModelView writeoff)
                ShowWin.ShowNewWriteoff(writeoff);
            else
                ShowWin.ShowNewWriteoff();
        });
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ObservableCollection<WriteoffModelView> WriteoffEntries
        {
            get
            {
                if (_services == null) return new ObservableCollection<WriteoffModelView>();
                if (StartDate == new DateTime()) StartDate = DateTime.MinValue;
                if (EndDate == new DateTime()) EndDate = DateTime.MaxValue;
                PageCount = (int) Math.Ceiling(
                    _services.Comings.GetCount(StartDate, EndDate) / (double) CountItemInPage);
                return new ObservableCollection<WriteoffModelView>(_services.Writeoffs.GetAll(StartDate, EndDate,
                    (CurrentPage - 1) * CountItemInPage, CountItemInPage));
            }
        }

        private readonly ServicesManager _services;
        public WriteoffViewModel(ServicesManager services)
        {
            _services = services;
            CountItemInPage = 2;
        }
        public WriteoffViewModel()
        {

        }
    }
}
