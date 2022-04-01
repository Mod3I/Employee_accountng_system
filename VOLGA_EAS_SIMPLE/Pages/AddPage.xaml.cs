using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace VOLGA_EAS_SIMPLE.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private PROJECT _currentProject = new PROJECT();
        public AddPage(PROJECT selectedProject)
        {
            InitializeComponent();

            if (selectedProject != null)
                _currentProject = selectedProject;

            DataContext = _currentProject;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentProject.PROJECT_NAME))
                errors.AppendLine("Укажите название проекта");

            if (string.IsNullOrWhiteSpace(_currentProject.PROJECT_DISCRIPTION))
                errors.AppendLine("Укажите описание проекта");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProject.PROJECT_ID == 0)
                VOLGA_EAS_DBEntities1.GetContext().PROJECTS.Add(_currentProject);

            try
            {
                VOLGA_EAS_DBEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
