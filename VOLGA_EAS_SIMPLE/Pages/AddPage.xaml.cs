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

        private List<USER> gtij = new List<USER>();

        private List<USER> gtij1 = new List<USER>();

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
            var us = VOLGA_EAS_DBEntities1.GetContext().USERS.ToList();
            var us1 = VOLGA_EAS_DBEntities1.GetContext().USERS.ToList();
            var staff = VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Where(p => p.PROJECT == _currentProject.PROJECT_ID).ToList();

            //List<USER> gtij = new List<USER>();
            gtij1.AddRange(us);

            foreach (var vf in us)
            {
                
                foreach (var frf in staff)
                {

                    if (vf.USER_ID.Equals(frf.USER))
                    {
                        //currentStaff.PROJECT = _currentProject.PROJECT_ID;
                        //currentStaff.USER = vf.USER_ID;
                        //VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Add(currentStaff);
                        gtij.Add(vf);
                        us1.Remove(vf);
                    }
                    
                }
            }

            gtij1.AddRange(us1);
            Employees.ItemsSource = us1.ToList();
            LVEmployees.ItemsSource = gtij.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            var endStaff = LVEmployees.Items[0].ToString();

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
                //VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Add();

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
            _selectedUser = (USER)Employees.SelectedItem;
            _currentStaff.PROJECT = _currentProject.PROJECT_ID;
            _currentStaff.USER = _selectedUser.USER_ID;
            VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Add(_currentStaff);
            UpdateEmployees();
        }

        private void DeleteStaff_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
