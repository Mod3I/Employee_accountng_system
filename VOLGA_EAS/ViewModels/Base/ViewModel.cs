using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VOLGA_EAS.Views.Windows.MainWindowViews;

namespace VOLGA_EAS.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        //private LoginView _LoginView = new LoginView();

        //private RegistrationView _RegistrationView = new RegistrationView();

        //private INotifyPropertyChanged _CurrentViewModel;
        //public INotifyPropertyChanged CurrentViewModel
        //{
        //    get { return _CurrentViewModel; }
        //    set
        //    {
        //        _CurrentViewModel = value;
        //        OnPropertyChanged(() => CurrentViewModel);
        //    }
        //}

        //private void OnPropertyChanged(Func<INotifyPropertyChanged> p)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<INotifyPropertyChanged> ViewModelsToSwitch
        //{
        //    get
        //    {
        //        return new INotifyPropertyChanged[]
        //                   {
        //                           _LoginView,
        //                           _RegistrationView
        //                   };
        //    }
        //}
    }
}
