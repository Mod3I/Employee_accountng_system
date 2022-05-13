using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VOLGA_EAS_SIMPLE.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private USER _selectedUser = new USER();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = Loging.Text;
            var password = Passwordg.Text;
            USER _confirmedUser = new USER();
            _confirmedUser = VOLGA_EAS_DBEntities1.GetContext().USERS.FirstOrDefault(p => p.USER_NAME == login);

            if (_confirmedUser != null)
            {
                if (_confirmedUser.USER_PASSWORD == password)
                {
                    App.CurrentUser = _confirmedUser;
                    Manager.MainFrame.Navigate(new MainPageWithSort());
                }
                else
                {
                    MessageBox.Show("Не правильный пароль");
                    Loging.Clear();
                    Passwordg.Clear();
                }
            }
            else
            {
                MessageBox.Show("Не правильный логин");
                Loging.Clear();
                Passwordg.Clear();
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new RegistrationPage());
        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // Suppress copy and paste
            if (e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Paste)
                e.Handled = true;
        }
    }
}
