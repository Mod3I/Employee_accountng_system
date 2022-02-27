using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VOLGA_EAS.Commands;
using VOLGA_EAS.Commands.Store;
using VOLGA_EAS.ViewModels.Base;

namespace VOLGA_EAS.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private string _Title = "VOLGA";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        public ICommand NavigateRegistrationCommand { get; }

        public LoginViewModel(NavigationStore navigationStore)
        {
            NavigateRegistrationCommand = new NavigateCommand<RegistrationViewModel>(navigationStore, () => new RegistrationViewModel(navigationStore));
        }
    }
}
