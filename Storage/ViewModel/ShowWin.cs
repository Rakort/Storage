using Storage.Commands;
using Storage.Model;
using Storage.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Storage.ViewModel
{
    public static class ShowWin
    {
        public static MainWindow mainWindow;

        #region Окна

        public static void ShowStorage()
        {
            mainWindow.ShowUserControl(new StorageView() {DataContext = new StorageViewModel()});
        }

        public static void ShowNewComing(Coming coming = null)
        {
            mainWindow.ShowUserControl(new NewComingView() {DataContext = new NewComingViewModel(coming)});
        }

        public static void ShowComing()
        {
            mainWindow.ShowUserControl(new ComingView() {DataContext = new ComingViewModel()});
        }

        public static void ShowWriteoff()
        {
            mainWindow.ShowUserControl(new WriteoffView() {DataContext = new WriteoffViewModel()});
        }

        public static void ShowNewWriteoff(Writeoff writeoff = null)
        {
            mainWindow.ShowUserControl(new NewWriteoffView() {DataContext = new NewWriteoffViewModel(writeoff)});
        }

        #endregion

        #region Донолнительные Окна

        public static void ShowInfo()
        {
            mainWindow.ShowAddedWindow(new InfoView());
        }

        public static AddedView AddedProduct(Product product = null, ICommand sumbit = null)
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
                    Sql.Add(new Product()
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

            }
            else
            {
                tbName.Text = product.ProductName;
                tbDescription.Text = product.Description;
                tbCode.Text = product.Code;
                tbArticle.Text = product.Article;
                tbMinCount.Text = product.MinCount.ToString();
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
                    Sql.Update(new Product()
                    {
                        IdProduct = product.IdProduct,
                        ProductName = tbName.Text,
                        Description = tbDescription.Text,
                        Code = tbCode.Text,
                        Article = tbArticle.Text,
                        MinCount = int.Parse(tbMinCount.Text),
                        Count = product.Count
                    });
                    sumbit?.Execute(null);
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        public static AddedView AddedProvider(Provider provider = null, ICommand sumbit = null)
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
            if (provider == null)
            {
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbName.Text.Trim()))
                    {
                        lblNameE.Visibility = Visibility.Visible;
                        return;
                    }
                    Sql.Add(new Provider()
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
            }
            else
            {
                tbName.Text = provider.Name;
                tbAddress.Text = provider.Address;
                tbEmail.Text = provider.Email;
                tbPhone.Text = provider.Phone;
                tbNote.Text = provider.Note;
                a.Submit = new SimpleCommand(() =>
                {
                    if (string.IsNullOrEmpty(tbName.Text.Trim()))
                    {
                        lblNameE.Visibility = Visibility.Visible;
                        return;
                    }
                    Sql.Update(new Provider()
                    {
                        IdProvider = provider.IdProvider,
                        Name = tbName.Text,
                        Address = tbAddress.Text,
                        Email = tbEmail.Text,
                        Phone = tbPhone.Text,
                        Note = tbNote.Text
                    });
                    sumbit?.Execute(null);
                    a.Close();
                });
            }

            a.Init();
            mainWindow.ShowAddedWindow(a);
            return a;
        }

        #endregion
    }
}
