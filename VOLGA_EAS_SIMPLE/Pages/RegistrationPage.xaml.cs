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
        //private USERS _currentUser = new USERS();

        public RegistrationPage()
        {
            InitializeComponent();
            //this.DataContext = new VOLGA_EAS_DBEntities();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //private string ConfPass = ConffPasswordr.Text;
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
            //    Manager.MainFrame.Navigate(new MainPage());
            //}
            //else
            //{
            //    MessageBox.Show("Не правильный логин или пароль");
            //    Loging.Clear();
            //    Passwordg.Clear();
            //}
            //StringBuilder errors = new StringBuilder();

            //if (string.IsNullOrWhiteSpace(_currentUser.USER_EMAIL))
            //    errors.AppendLine("Укажите электронную почту");

            //if (string.IsNullOrWhiteSpace(_currentUser.USER_NAME))
            //    errors.AppendLine("Укажите никнейм");

            //if (string.IsNullOrWhiteSpace(_currentUser.USER_PASSWORD))
            //    errors.AppendLine("Укажите пароль");

            //if (errors.Length > 0)
            //{
            //    MessageBox.Show(errors.ToString());
            //    return;
            //}

            //if (_currentUser.USER_ID == 0 && _currentUser.USER_PASSWORD == ConfPass)
            //    VOLGA_EAS_DBEntities.GetContext().USERS.Add(_currentUser);

            //try
            //{
            //    VOLGA_EAS_DBEntities.GetContext().SaveChanges();
            //    MessageBox.Show("Информация сохранена");
            //    Manager.MainFrame.GoBack();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
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
