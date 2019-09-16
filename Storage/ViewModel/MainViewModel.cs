using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Storage.Commands;

namespace Storage.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        public ICommand ShowStorage => new SimpleCommand(ShowWin.ShowStorage);
        public ICommand ShowComing => new SimpleCommand(ShowWin.ShowComing);
        public ICommand ShowWriteoff => new SimpleCommand(ShowWin.ShowWriteoff);
        public ICommand Back => new SimpleCommand(ShowWin.Back,(_) => ShowWin.IsBack);

    }
}
