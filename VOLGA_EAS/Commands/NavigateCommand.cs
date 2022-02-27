using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOLGA_EAS.Commands.Base;
using VOLGA_EAS.Commands.Store;
using VOLGA_EAS.ViewModels.Base;

namespace VOLGA_EAS.Commands
{
    public class NavigateCommand<TViewModel> : Command
        where TViewModel : ViewModel
    {
        private readonly NavigationStore _navigationSotre;
        private readonly Func<TViewModel> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationSotre = navigationStore;
            _createViewModel = createViewModel;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _navigationSotre.CurrentViewModel = _createViewModel();
        }
    }
}
