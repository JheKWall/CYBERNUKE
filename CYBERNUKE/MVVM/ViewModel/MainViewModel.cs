using CYBERNUKE.Core;
using CYBERNUKE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.ViewModel
{
    class MainViewModel : Core.ViewModel
    {
        //Var
        private INavigationService _navigation;

        //Constructor
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        //Navigation Commands
        public RelayCommand NavigateCombatViewCommand { get; set; }
        public RelayCommand NavigateCutsceneViewCommand { get; set; }
        public RelayCommand NavigateMainMenuViewCommand { get; set; }
        public RelayCommand NavigateOverworldViewCommand { get; set; }
        public RelayCommand NavigateTownViewCommand { get; set; }

        //View Model Constructor, Navigate Implementation
        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;

            //Navigation Command Implementations
            NavigateCombatViewCommand = new RelayCommand(o => Navigation.NavigateTo<CombatViewModel>(), o => true);
            NavigateCutsceneViewCommand = new RelayCommand(o => Navigation.NavigateTo<CutsceneViewModel>(), o => true);
            NavigateMainMenuViewCommand = new RelayCommand(o => Navigation.NavigateTo<MainMenuViewModel>(), o => true);
            NavigateOverworldViewCommand = new RelayCommand(o => Navigation.NavigateTo<OverworldViewModel>(), o => true);
            NavigateTownViewCommand = new RelayCommand(o => Navigation.NavigateTo<TownViewModel>(), o => true);
        }
    }
}
