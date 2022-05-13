using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;

namespace VOLGA_EAS_SIMPLE
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public static Entities.VOLGA_EAS_DBEntities1
        public static USER CurrentUser = null;
    }
}
