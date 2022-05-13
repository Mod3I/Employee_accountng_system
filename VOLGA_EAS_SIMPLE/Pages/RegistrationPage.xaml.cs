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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var mail = Emailr.Text;
            var login = Loginr.Text;
            var password = Passwordr.Text;
            var conffpassword = ConffPasswordr.Text;
            USER _confirmedUser = new USER();
            USER uSER = new USER();
            _confirmedUser = VOLGA_EAS_DBEntities1.GetContext().USERS.FirstOrDefault(p => p.USER_NAME == login);

            if (_confirmedUser == null)
            {
                if (password == conffpassword)
                {
                    uSER.USER_EMAIL = mail;
                    uSER.USER_NAME = login;
                    uSER.USER_PASSWORD = password;
                    uSER.USER_POSITION = 1;
                    VOLGA_EAS_DBEntities1.GetContext().USERS.Add(uSER);

                    try
                    {
                        VOLGA_EAS_DBEntities1.GetContext().SaveChanges();
                        MessageBox.Show("Новый пользователь зарегистрирован");
                        Manager.MainFrame.Navigate(new LoginPage());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }    
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                    ConffPasswordr.Clear();
                }
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином существует");
                Loginr.Clear();
                Passwordr.Clear();
                ConffPasswordr.Clear();
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
