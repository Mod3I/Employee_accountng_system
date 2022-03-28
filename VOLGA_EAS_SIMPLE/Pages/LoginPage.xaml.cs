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
        private USERS _selectedUser = new USERS();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //StringBuilder errors = new StringBuilder();

            //if (string.IsNullOrWhiteSpace(_selectedUser.USER_NAME))
            //    errors.AppendLine("Укажите название проекта");

            //if (string.IsNullOrWhiteSpace(_selectedUser.USER_PASSWORD))
            //    errors.AppendLine("Укажите описание проекта");

            //if (errors.Length > 0)
            //{
            //    MessageBox.Show(errors.ToString());
            //    return;
            //}

            SqlConnection connection1 = new SqlConnection("Data Source=DESKTOP-5CU2HHD;Initial Catalog=VOLGA_EAS_DB;Integrated Security=True");
            SqlCommand comand1 = new SqlCommand();
            SqlDataAdapter adaptor1 = new SqlDataAdapter();
            DataSet dataset1 = new DataSet();
            DataTable dt = new DataTable();

            comand1.CommandText = @"SELECT ""USER_NAME"", ""USER_PASSWORD"" FROM ""USERS"" WHERE ""USER_NAME""='" + Loging.Text +
            @"'AND ""USER_PASSWORD""='" + Passwordg.Text + "'";


            comand1.Connection = connection1;

            adaptor1.SelectCommand = comand1;
            adaptor1.Fill(dataset1, "0");
            adaptor1.Fill(dt);
            int count1 = dataset1.Tables[0].Rows.Count;
            if (count1 > 0)
            {
                connection1.Close();
                Manager.MainFrame.Navigate(new MainPageWithSort());
            }
            else
            {
                MessageBox.Show("Не правильный логин или пароль");
                Loging.Clear();
                Passwordg.Clear();
            }
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

            //SqlConnection connection1 = new SqlConnection();
            //connection1.ConnectionString = Properties.Settings.Default.qwertyConnectionString1;
            //SqlCommand comand1 = new SqlCommand();
            //SqlDataAdapter adaptor1 = new SqlDataAdapter();
            //DataSet dataset1 = new DataSet();
            //DataTable Manager.MainFrame.Navigate(new MainPage());
            //SqlC dt = new DataTable();

            //comand1.CommandText = @"SELECT ""id"", ""name"" FROM ""Table_1"" WHERE ""id""='" + tb1.Text +
            //@"'AND ""Password""='" + tb2.Text + "'";

            //comand1.Connection = connection1;

            //adaptor1.SelectCommand = comand1;
            //adaptor1.Fill(dataset1, "0");
            //adaptor1.Fill(dt);
            //int count1 = dataset1.Tables[0].Rows.Count;
            //if (count1 > 0)
            //{
            //    connection1.Close();
            //    MainWindow Menu = new MainWindow();
            //    Menu.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Не правильный логин или пароль");
            //    tb1.Clear();
            //    tb2.Clear();
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
