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
    public class RegistrationViewModel : ViewModel
    {
        public ICommand NavigateLoginCommand { get; }

        public RegistrationViewModel(NavigationStore navigationStore)
        {
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));
        }
    }
}
