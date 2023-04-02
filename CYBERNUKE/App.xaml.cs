using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CYBERNUKE.Core;
using CYBERNUKE.MVVM.ViewModel;
using CYBERNUKE.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CYBERNUKE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Backing for _serviceProvider
        private readonly ServiceProvider _serviceProvider;

        // App Constructor
        public App()
        {
            IServiceCollection services = new ServiceCollection(); //Contains the services defined beneath it (i.e. Service COLLECTION)

            services.AddSingleton<MainWindow>(ServiceProvider => new MainWindow 
            { 
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
            }); //MainWindow Registered so we can open it on startup, the ServiceProvider binds it to its ViewModel class

            //Registered Services (for each ViewModel)
            //Referred to elsewhere as "Singletons"
            //Views MUST be registered before implementation
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<CombatViewModel>();
            services.AddSingleton<CutsceneViewModel>();
            services.AddSingleton<MainMenuViewModel>();
            services.AddSingleton<OverworldViewModel>();
            services.AddSingleton<TownViewModel>();

            //Delegate for viewModelType, Nav. Service Registry
            services.AddSingleton<Func<Type, ViewModel>>(ServiceProvider => viewModelType => (ViewModel)ServiceProvider.GetRequiredService(viewModelType));
            services.AddSingleton<INavigationService, NavigationService>();

            _serviceProvider = services.BuildServiceProvider(); //Service Provider
        }

        // OnStartup Override, Opens to [View].Show();
        protected override void OnStartup(StartupEventArgs e)
        {
            //Open to MainWindow
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);

            //Navigate to Main Menu on Startup
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            if (mainWindowViewModel.NavigateMainMenuViewCommand.CanExecute(null))
                mainWindowViewModel.NavigateMainMenuViewCommand.Execute(null);
        }
    }
}
