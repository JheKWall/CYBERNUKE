using CYBERNUKE.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.Services
{
    // Navigation Service Interface
    public interface INavigationService
    {
        //View that we are displaying
        ViewModel CurrentView { get; }

        //Function that handles switching views
        void NavigateTo<T>() where T : ViewModel;
    }

    // Navigation Service Implementation
    class NavigationService : ObservableObject, INavigationService
    {
        //Variables
        private ViewModel _currentView;
        private readonly Func<Type, ViewModel> _viewModelFactory;

        //Current View implementation
        public ViewModel CurrentView 
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        //Constructor
        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            //We pass in the ViewModel we want to switch to and this returns its Singleton instance (from App.xaml.cs)
            _viewModelFactory = viewModelFactory;
        }


        //Navigation Function Implementation
        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            //Function for actually switching screen to another View
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
