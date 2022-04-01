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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPage(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var projectsForRemoving = DGridProjects.SelectedItems.Cast<PROJECT>().ToList();

            if (MessageBox.Show($"Вы точно желаете удаить следующие {projectsForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    VOLGA_EAS_DBEntities1.GetContext().PROJECTS.RemoveRange(projectsForRemoving);
                    VOLGA_EAS_DBEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Элементы удалены!");

                    DGridProjects.ItemsSource = VOLGA_EAS_DBEntities1.GetContext().PROJECTS.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPage((sender as Button).DataContext as PROJECT));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                VOLGA_EAS_DBEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProjects.ItemsSource = VOLGA_EAS_DBEntities1.GetContext().PROJECTS.ToList();
            }
        }
    }
}
