using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace VOLGA_EAS_SIMPLE.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private PROJECT _currentProject = new PROJECT();

        private List<USER> _staffProject = new List<USER>();

        private List<USER> _staffForAdd = new List<USER>();

        private PROJECT_STAFF _currentStaff = new PROJECT_STAFF();

        private USER _selectedUser = new USER();

        ////private IQueryable<PROJECT_STAFF> _currentStaff;

        ////public IQueryable<PROJECT_STAFF> CurrentStaff { get => _currentStaff; set => _currentStaff = value; }

        public AddPage(PROJECT selectedProject)
        {
            InitializeComponent();

            if (selectedProject != null)
                _currentProject = selectedProject;

            UpdateEmployees();

            DataContext = _currentProject;
        }

        private void UpdateEmployees()
        {
            var currentStaff = new PROJECT_STAFF();
            var allUsers = VOLGA_EAS_DBEntities1.GetContext().USERS.ToList();
            _staffForAdd.AddRange(allUsers);
            var us1 = VOLGA_EAS_DBEntities1.GetContext().USERS.ToList();
            var staff = VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Where(p => p.PROJECT == _currentProject.PROJECT_ID).ToList();

            foreach (var thisUser in allUsers)
            {
                
                foreach (var thisStaff in staff)
                {

                    if (thisUser.USER_ID.Equals(thisStaff.USER))
                    {
                        _staffProject.Add(thisUser);
                        _staffForAdd.Remove(thisUser);
                    }
                    
                }
            }

            Employees.ItemsSource = _staffForAdd.ToList();
            LVEmployees.ItemsSource = _staffProject.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            try
            {
                var endStaff = LVEmployees.Items[0].ToString();
            }
            catch (Exception ex)
            {

            }

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
            {
                VOLGA_EAS_DBEntities1.GetContext().PROJECTS.Add(_currentProject);
            }

            PROJECT_STAFF newCurrentProjectStaff = new PROJECT_STAFF();
            newCurrentProjectStaff.PROJECT = _currentProject.PROJECT_ID;

            foreach (USER currentStaff in _staffProject)
            {
                if (!VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Where(p => p.PROJECT == _currentProject.PROJECT_ID).ToList().Equals(currentStaff))
                {
                    newCurrentProjectStaff.USER = currentStaff.USER_ID;
                    VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Add(newCurrentProjectStaff);
                }
            }

            try
            {
                VOLGA_EAS_DBEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.Navigate(new MainPageWithSort());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void AddStaff_Click(object sender, RoutedEventArgs e)
        {
            if ((USER)Employees.SelectedItem != null)
            {
                _staffProject.Add((USER)Employees.SelectedItem);
                LVEmployees.ItemsSource = _staffProject;

                _staffForAdd.Remove((USER)Employees.SelectedItem);
                Employees.ItemsSource = _staffForAdd;
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }

        private void DeleteStaff_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
