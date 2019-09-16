using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
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
    public class ComingViewModel : ViewModelPage
    {
        public override ICommand Refresh => new SimpleCommand(() => OnPropertyChanged(nameof(ComingEntries)));

        public ICommand NewComing => new SimpleCommand((item) =>
        {
            if (item is ComingModelView coming)
                ShowWin.ShowNewComing(coming);
            else
                ShowWin.ShowAddComing();
        });

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<ComingModelView> ComingEntries
        {
            get {
            if (StartDate == new DateTime()) StartDate = DateTime.MinValue;
            if (EndDate == new DateTime()) EndDate = DateTime.MaxValue;
            PageCount = (int)Math.Ceiling(_services.Comings.GetCount(StartDate, EndDate)/ (double)CountItemInPage);
            return new ObservableCollection<ComingModelView>(_services.Comings.GetAll(StartDate, EndDate, (CurrentPage-1)*CountItemInPage, CountItemInPage));
        }
    }

        private readonly ServicesManager _services;


        public ComingViewModel(ServicesManager services)
        {
            _services = services;
            CountItemInPage = 2;
        }
        
    }
}
