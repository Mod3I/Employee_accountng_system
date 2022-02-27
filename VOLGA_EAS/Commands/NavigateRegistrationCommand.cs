using System;
using VOLGA_EAS.Commands.Base;
using VOLGA_EAS.Commands.Store;
using VOLGA_EAS.ViewModels;
using VOLGA_EAS.ViewModels.Base;

namespace VOLGA_EAS.Commands
{
    //internal class NavigateRegistrationCommand<TViewModel> : Command
    //    where TViewModel : ViewModel
    //{
    //    private readonly NavigationStore _navigationSotre;
    //    private readonly Func<TViewModel> _createViewModel;

    //    public NavigateRegistrationCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
    //    {
    //        _navigationSotre = navigationStore;
    //        _createViewModel = createViewModel;
    //    }

    //    public override bool CanExecute(object parameter) => true;

    //    public override void Execute(object parameter)
    //    {
    //        _navigationSotre.CurrentViewModel = _createViewModel();
    //    }
    //}
}
//namespace VOLGA_EAS.Commands
//{
//    internal class NavigateRegistrationCommand : Command
//    {
//        private readonly NavigationStore _navigationSotre;

//        public NavigateRegistrationCommand(NavigationStore navigationStore)
//        {
//            _navigationSotre = navigationStore;
//        }

//        public override bool CanExecute(object parameter) => true;

//        public override void Execute(object parameter)
//        {
//            _navigationSotre.CurrentViewModel = new RegistrationViewModel(_navigationSotre);
//        }
//    }
//}
