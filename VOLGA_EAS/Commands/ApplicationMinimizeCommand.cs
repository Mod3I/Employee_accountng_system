﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VOLGA_EAS.Commands.Base;

namespace VOLGA_EAS.Commands
{
    public class ApplicationMinimizeCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Windows[0].WindowState = WindowState.Minimized;
    }
}