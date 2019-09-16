using Storage.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Storage.Commands;

namespace Storage.ViewModel
{
    public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class ViewModelPage : ViewModelBase
    {

        private int _currentPage;
        private int _pageCount;
        private int _countItemInPage;
        public int CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); }
        }

        public int PageCount
        {
            get => _pageCount;
            set { _pageCount = value; OnPropertyChanged(); }
        }

        public int CountItemInPage
        {
            get => _countItemInPage;
            set { _countItemInPage = value; OnPropertyChanged(); }
        }

        public ICommand SetItemsPerPage => new SimpleCommand((value) =>
        {
            int newVal = Int32.Parse(value.ToString());
            if (CountItemInPage != newVal)
            {
                CountItemInPage = newVal;
                CurrentPage = 1;
            }

        });
        public ICommand NextPageCommand => new SimpleCommand(() =>
        {
            CurrentPage++; Refresh.Execute(null);
        }, (_) => CurrentPage < PageCount);

        public ICommand LastPageCommand => new SimpleCommand(() =>
        {
            CurrentPage = PageCount; Refresh.Execute(null);
        }, (_) => CurrentPage < PageCount);
        public ICommand FirstPageCommand => new SimpleCommand(() =>
        {
            CurrentPage = 1; Refresh.Execute(null);
        }, (_) => CurrentPage > 1);
        public ICommand PrevPageCommand => new SimpleCommand(() =>
        {
            CurrentPage--; Refresh.Execute(null);
        }, (_) => CurrentPage > 1);

        public virtual ICommand Refresh { get; }
        protected ViewModelPage()
        {
            CurrentPage = 1;
            PageCount = 1;
        }
    }
}
