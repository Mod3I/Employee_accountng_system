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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace VOLGA_EAS_SIMPLE.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPageWithSort.xaml
    /// </summary>
    public partial class MainPageWithSort : Page
    {
        private VOLGA_EAS_DBEntities1 _context = new VOLGA_EAS_DBEntities1();
        public MainPageWithSort()
        {
            InitializeComponent();

            var currentProject = VOLGA_EAS_DBEntities1.GetContext().PROJECTS.ToList();
            LVProjects.ItemsSource = currentProject;
            CheckNoNull.IsChecked = true;
        }

        private void UpdateProjects()
        {
            var currentProject = VOLGA_EAS_DBEntities1.GetContext().PROJECTS.ToList();

            currentProject = currentProject.Where(p => p.PROJECT_NAME.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            //if (CheckNoNull.IsChecked.Value)
            //    currentProject = currentProject.Where(p => p.PROJECT_DISCRIPTION.Contains();

            LVProjects.ItemsSource = currentProject.OrderBy(p => p.PROJECT_NAME).ToList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProjects();
        }

        private void CheckNoNull_Checked(object sender, RoutedEventArgs e)
        {
            //UpdateProjects();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPage(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var projectsForRemoving = LVProjects.SelectedItems.Cast<PROJECT>().ToList();

            if (projectsForRemoving.Count() > 0)
            {
                if (MessageBox.Show($"Вы точно желаете удаить следующие {projectsForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        VOLGA_EAS_DBEntities1.GetContext().PROJECTS.RemoveRange(projectsForRemoving);
                        VOLGA_EAS_DBEntities1.GetContext().SaveChanges();
                        MessageBox.Show("Элементы удалены!");

                        LVProjects.ItemsSource = VOLGA_EAS_DBEntities1.GetContext().PROJECTS.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите элементы для удаления");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPage((sender as Button).DataContext as PROJECT));
        }

        private void WordExport_Click(object sender, RoutedEventArgs e)
        {
            var allProjects = _context.PROJECTS.ToList().OrderBy(p => p.PROJECT_NAME).ToList();

            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            foreach (var project in allProjects)
            {
                Word.Paragraph projectParagraph = document.Paragraphs.Add();
                Word.Range projectRange = projectParagraph.Range;

                projectRange.Text = project.PROJECT_NAME;
                projectParagraph.set_Style("Заголовок");
                projectRange.InsertParagraphAfter();
            }
        }

        private void ExcelExport_Click(object sender, RoutedEventArgs e)
        {
            var allProjects = _context.PROJECTS.ToList().OrderBy(p => p.PROJECT_NAME).ToList();

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = allProjects.Count();

            int startRowIndex = 1;

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            for (int i = 0; i < allProjects.Count(); i++)
            {
                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name = allProjects[i].PROJECT_NAME;

                worksheet.Cells[1][startRowIndex] = "Название проекта";
                worksheet.Cells[2][startRowIndex] = "Описание проекта";

                startRowIndex++;

                worksheet.Cells[1][startRowIndex] = allProjects[i].PROJECT_NAME;
                worksheet.Cells[2][startRowIndex] = allProjects[i].PROJECT_DISCRIPTION;

                startRowIndex = 1;
            }
            application.Visible = true;
        }

        //private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (Visibility == Visibility.Visible)
        //    {
        //        VOLGA_EAS_DBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        //        LVProjects.ItemsSource = VOLGA_EAS_DBEntities.GetContext().PROJECTS.ToList();
        //    }
        //}
    }
}
