using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VOLGA_EAS.Commands.Store;
using VOLGA_EAS.ViewModels.Base;
using VOLGA_EAS.Views.Windows.MainWindowViews;

namespace VOLGA_EAS.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string _Title = "VOLGA";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }


        private readonly NavigationStore _navigationStore;

        public ViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        //private LoginView _LoginView = new LoginView();

        //private RegistrationView _RegistrationView = new RegistrationView();

        //private INotifyPropertyChanged _CurrentViewModel;
        //public INotifyPropertyChanged CurrentViewModel
        //{
        //    get => { return _CurrentViewModel; }
        //    set
        //    {
        //        _CurrentViewModel = value;
        //        OnPropertyChanged(() => CurrentViewModel);
        //    }
        //}

        //private override OnPropertyChanged(Func<ViewModel> p) => throw new NotImplementedException();
    }
}
