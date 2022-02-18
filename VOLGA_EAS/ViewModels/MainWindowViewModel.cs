using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOLGA_EAS.ViewModels.Base;

namespace VOLGA_EAS.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _Title = "VOLGA";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
    }
}
