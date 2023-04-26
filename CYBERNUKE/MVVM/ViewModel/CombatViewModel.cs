﻿using CYBERNUKE.Core;
using CYBERNUKE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.ViewModel
{
    class CombatViewModel : Core.ViewModel
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
        public RelayCommand NavigateCutsceneViewCommand { get; set; }
        public RelayCommand NavigateMainMenuViewCommand { get; set; }
        public RelayCommand NavigateOverworldViewCommand { get; set; }

        //View Model Constructor, Navigate Implementation
        public CombatViewModel(INavigationService navService)
        {
            Navigation = navService;

            //Navigation Command Implementations
            NavigateCutsceneViewCommand = new RelayCommand(o => Navigation.NavigateTo<CutsceneViewModel>(), o => true);
            NavigateMainMenuViewCommand = new RelayCommand(o => Navigation.NavigateTo<MainMenuViewModel>(), o => true);
            NavigateOverworldViewCommand = new RelayCommand(o => Navigation.NavigateTo<OverworldViewModel>(), o => true);
        }
    }
}
