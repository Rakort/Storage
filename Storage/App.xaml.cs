﻿using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Storage.ViewModel;


namespace Storage
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    { 
        //private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            ShowWin.ShowMain();
            //Current.MainWindow = new MainWindow();
            //Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            //this.container = new StandardKernel();
            //container.Bind<DB>().To<DB_SQLite>().InTransientScope();
        }

        private void ComposeObjects()
        {
            //Current.MainWindow = this.container.Get<MainWindow>();            
        }
    }

}
