using Storage.Commands;
using Storage.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PresentationLayer;
using PresentationLayer.Models;
using BuissnesLayer;
using DataLayer;

namespace Storage.ViewModel
{
    public static class ShowWin
    {
        public static MainWindow mainWindow;
        private static readonly ServicesManager Services = new ServicesManager(new DataManager(new EFDBContext()));
        private static Stack<UserControl> userControls = new Stack<UserControl>();
        private static UserControl _currentControl;

        private static void ShowUserControl(UserControl uc)
        {
            userControls.Push(_currentControl);
            _currentControl = uc;
            mainWindow.ShowUserControl(uc);
        }

        private static void ShowHeadUserControl(UserControl uc)
        {
            userControls.Clear();
            _currentControl = uc;
            mainWindow.ShowUserControl(uc);
        }

        public static void Back()
        {
            if (IsBack) mainWindow.ShowUserControl(userControls.Pop());
        }

        public static bool IsBack => userControls.Count > 0;

        #region Окна

        public static void ShowMain()
        {
            mainWindow = new MainWindow(){DataContext = new MainViewModel()};
            mainWindow.Show();
            ShowStorage();
        }

        public static void ShowStorage()
        {
            ShowHeadUserControl(new StorageView() {DataContext = new StorageViewModel(Services)});
        }

        public static void ShowNewComing(ComingModelView coming)
        {
            ShowUserControl(new NewComingView() { DataContext = new NewComingViewModel(Services, coming.Id) });
        }
        public static void ShowAddComing()
        {
            ShowUserControl(new NewComingView() { DataContext = new NewComingViewModel(Services) });
        }

        public static void ShowComing()
        {
            ShowHeadUserControl(new ComingView() {DataContext = new ComingViewModel(Services) });
        }

        public static void ShowWriteoff()
        {
            ShowHeadUserControl(new WriteoffView() {DataContext = new WriteoffViewModel(Services)});
        }

        public static void ShowNewWriteoff(WriteoffModelView writeoff = null)
        {
            var vm = writeoff == null ? new NewWriteoffViewModel(Services) : new NewWriteoffViewModel(Services, writeoff.Id);
            ShowUserControl(new NewWriteoffView() { DataContext = vm });
        }

        #endregion

        #region Донолнительные Окна
        
        public static AddedView AddedProduct(ProductModel product = null, ICommand sumbit = null)
        {
            var tbName = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var lblNameE = new Label()
            {
                Content = "Необходимо заполнить Наименование товара",
                Foreground = Brushes.Red,
                Visibility = Visibility.Collapsed
            };
            var tbDescription = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var tbCode = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var tbArticle = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var tbMinCount = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var lblMinCountE = new Label()
            {
                Content = "Должно быть числом",
                Foreground = Brushes.Red,
                Visibility = Visibility.Collapsed
            };
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() {Content = "Наименование", Margin = new Thickness(10, 0, 10, 0)},
                    tbName,
                    lblNameE,
                    new Label() {Content = "Описание", Margin = new Thickness(10, 0, 10, 0)},
                    tbDescription,
                    new Label() {Content = "Код", Margin = new Thickness(10, 0, 10, 0)},
                    tbCode,
                    new Label() {Content = "Артикул", Margin = new Thickness(10, 0, 10, 0)},
                    tbArticle,
                    new Label() {Content = "Минимальный остаток", Margin = new Thickness(10, 0, 10, 0)},
                    tbMinCount
                }
            };
            if (product == null)
            {
                tbMinCount.Text = "0";
            }
            else
            {
                tbName.Text = product.ProductName;
                tbDescription.Text = product.Description;
                tbCode.Text = product.Code;
                tbArticle.Text = product.Article;
                tbMinCount.Text = product.MinCount.ToString();
            }

            a.Submit = new SimpleCommand(() =>
            {
                if (string.IsNullOrEmpty(tbName.Text.Trim()))
                {
                    lblNameE.Visibility = Visibility.Visible;
                    return;
                }
                int res;
                if (!int.TryParse(tbMinCount.Text.Trim(), out res))
                {
                    lblMinCountE.Visibility = Visibility.Visible;
                    return;
                }
                Services.Products.Save(new ProductModel()
                {
                    ProductName = tbName.Text,
                    Description = tbDescription.Text,
                    Code = tbCode.Text,
                    Article = tbArticle.Text,
                    MinCount = int.Parse(tbMinCount.Text),
                    Count = 0
                });
                sumbit?.Execute(null);
                a.Close();
            });

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        public static AddedView AddedProvider(ProviderModel provider = null, ICommand sumbit = null)
        {
            var tbName = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var lblNameE = new Label()
            {
                Content = "Необходимо заполнить Имя поставщика",
                Foreground = Brushes.Red,
                Visibility = Visibility.Collapsed
            };
            var tbAddress = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var tbEmail = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var tbPhone = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var tbNote = new TextBox() {Margin = new Thickness(10, 0, 10, 10)};
            var a = new AddedView
            {
                Controls = new List<Control>
                {
                    new Label() {Content = "Имя", Margin = new Thickness(10, 0, 10, 0)},
                    tbName,
                    lblNameE,
                    new Label() {Content = "Адрес", Margin = new Thickness(10, 0, 10, 0)},
                    tbAddress,
                    new Label() {Content = "E-mail", Margin = new Thickness(10, 0, 10, 0)},
                    tbEmail,
                    new Label() {Content = "Телефон", Margin = new Thickness(10, 0, 10, 0)},
                    tbPhone,
                    new Label() {Content = "Примечание", Margin = new Thickness(10, 0, 10, 0)},
                    tbNote
                }
            };
            if (provider != null)
            {
                tbName.Text = provider.Name;
                tbAddress.Text = provider.Address;
                tbEmail.Text = provider.Email;
                tbPhone.Text = provider.Phone;
                tbNote.Text = provider.Note;
            }

            a.Submit = new SimpleCommand(() =>
            {
                if (string.IsNullOrEmpty(tbName.Text.Trim()))
                {
                    lblNameE.Visibility = Visibility.Visible;
                    return;
                }
                Services.Providers.Save(new ProviderModel()
                {
                    Name = tbName.Text,
                    Address = tbAddress.Text,
                    Email = tbEmail.Text,
                    Phone = tbPhone.Text,
                    Note = tbNote.Text
                });
                sumbit?.Execute(null);
                a.Close();
            });

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        #endregion
    }
}
