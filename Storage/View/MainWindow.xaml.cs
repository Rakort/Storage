using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Storage.Model;
using Storage.ViewModel;
using Table = System.Windows.Documents.Table;

namespace Storage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl _userControl, _addedWindow;
        public MainWindow()
        {
            InitializeComponent();
            Sql.CreateTable();
            ShowWin.mainWindow = this;
            
            ShowWin.ShowStorage();
        }
        public void ShowUserControl(UserControl newUserControl)
        {
            MainGrid.Children.Remove(_addedWindow);
            StackPanel.Children.Remove(_userControl);

            _userControl = newUserControl;
            _userControl.VerticalAlignment = VerticalAlignment.Stretch;
            _userControl.HorizontalAlignment = HorizontalAlignment.Stretch;

            StackPanel.Children.Add(_userControl);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowStorage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowComing();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowWriteoff();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowInfo();
        }

        public void ShowAddedWindow(UserControl addedWin)
        {
            MainGrid.Children.Remove(_addedWindow);
            _addedWindow = addedWin;
            MainGrid.Children.Add(addedWin);
            Grid.SetColumn(addedWin, 1);
        }
    }
}
