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
        public RelayCommand NavigateMainMenuViewCommand { get; set; }
        public RelayCommand NavigateCutsceneViewCommand { get; set; }

        //View Model Constructor, Navigate Implementation
        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;

            //Navigation Command Implementations
            NavigateMainMenuViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<MainMenuViewModel>(); }, canExecute: o => true);
            NavigateCutsceneViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<CutsceneViewModel>(); }, canExecute: o => true);
        }
    }
}
