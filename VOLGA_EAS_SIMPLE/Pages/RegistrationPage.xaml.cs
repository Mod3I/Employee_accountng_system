using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private USERS _currentUser = new USERS();
        private string ConfPass;

        public RegistrationPage()
        {
            InitializeComponent();
            this.DataContext = new VOLGA_EAS_DBEntities();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUser.USER_EMAIL))
                errors.AppendLine("Укажите название электронную почту");

            if (string.IsNullOrWhiteSpace(_currentUser.USER_NAME))
                errors.AppendLine("Укажите никнейм");

            if (string.IsNullOrWhiteSpace(_currentUser.USER_PASSWORD))
                errors.AppendLine("Укажите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUser.USER_ID == 0 && _currentUser.USER_PASSWORD == ConfPass)
                VOLGA_EAS_DBEntities.GetContext().USERS.Add(_currentUser);

            try
            {
                VOLGA_EAS_DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // Suppress copy and paste
            if (e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Paste)
                e.Handled = true;
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new LoginPage());
        }
    }
}
