using VOLGA_EAS.Commands.Base;
using VOLGA_EAS.Commands.Store;
using VOLGA_EAS.ViewModels;

namespace VOLGA_EAS.Commands
{
    public class NavigateLoginCommand : Command
    {
        private readonly NavigationStore _navigationSotre;

        public NavigateLoginCommand(NavigationStore navigationStore)
        {
            _navigationSotre = navigationStore;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _navigationSotre.CurrentViewModel = new LoginViewModel(_navigationSotre);
        }
    }
}
