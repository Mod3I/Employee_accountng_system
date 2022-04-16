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
                    Manager.MainFrame.Navigate(new MainPageWithSort());
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
            //SqlConnection connection1 = new SqlConnection("Data Source=DESKTOP-5CU2HHD;Initial Catalog=VOLGA_EAS_DB;Integrated Security=True");
            //SqlCommand comand1 = new SqlCommand();
            //SqlDataAdapter adaptor1 = new SqlDataAdapter();
            //DataSet dataset1 = new DataSet();
            //DataTable dt = new DataTable();

            //comand1.CommandText = @"SELECT ""USER_NAME"", ""USER_PASSWORD"" FROM ""USERS"" WHERE ""USER_NAME""='" + Loging.Text +
            //@"'AND ""USER_PASSWORD""='" + Passwordg.Text + "'";


            //comand1.Connection = connection1;

            //adaptor1.SelectCommand = comand1;
            //adaptor1.Fill(dataset1, "0");
            //adaptor1.Fill(dt);
            //int count1 = dataset1.Tables[0].Rows.Count;
            //if (count1 > 0)
            //{
            //    connection1.Close();
            //    Manager.MainFrame.Navigate(new MainPageWithSort());
            //}
            //else
            //{
            //    MessageBox.Show("Не правильный логин или пароль");
            //    Loging.Clear();
            //    Passwordg.Clear();
            //}

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
