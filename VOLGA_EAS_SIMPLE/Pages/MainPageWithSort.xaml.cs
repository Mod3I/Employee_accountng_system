using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            var ProjectStaff = VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF;
            var projectsForRemoving = LVProjects.SelectedItems.Cast<PROJECT>().ToList();
            var ProjectStaffForRemoving = VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.ToList();

            var proj = _context.PROJECTS.Include(p => p.PROJECT_STAFF).Where(p => p.PROJECT_ID == 0);
            var gtigtj = VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF;
            

            if (projectsForRemoving.Count() > 0)
            {
                if (MessageBox.Show($"Вы точно желаете удаить следующие {projectsForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var gtji in projectsForRemoving)
                        {
                            foreach (var tgru in gtigtj)
                            {
                                if (tgru.PROJECT == gtji.PROJECT_ID)
                                    VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.Remove(tgru);
                            }
                        }
                        VOLGA_EAS_DBEntities1.GetContext().PROJECTS.RemoveRange(projectsForRemoving);
                        VOLGA_EAS_DBEntities1.GetContext().SaveChanges();
                        MessageBox.Show("Элементы удалены!");

                        LVProjects.ItemsSource = VOLGA_EAS_DBEntities1.GetContext().PROJECTS.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
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
            var allProjects = _context.PROJECTS.ToList().OrderBy(p => p.PROJECT_NAME);

            var allStaff = VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF;

            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            foreach (var project in allProjects)
            {
                Word.Paragraph projectParagraph = document.Paragraphs.Add();
                Word.Range projectRange = projectParagraph.Range;

                projectRange.Text = project.PROJECT_NAME;
                //projectParagraph.set_Style("Title");
                projectRange.InsertParagraphAfter();

                Word.Paragraph tabelParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tabelParagraph.Range;
                Word.Table projectsTable = document.Tables.Add(tableRange, allProjects.Count(), 3);
                projectsTable.Borders.InsideLineStyle = projectsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                projectsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                cellRange = projectsTable.Cell(1, 1).Range;
                cellRange.Text = "Название проекта";
                cellRange = projectsTable.Cell(1, 2).Range;
                cellRange.Text = "Описание проекта";
                cellRange = projectsTable.Cell(1, 3).Range;
                cellRange.Text = "Количество сотрудников";

                cellRange = projectsTable.Cell(2, 1).Range;
                cellRange.Text = project.PROJECT_NAME.ToString();
                cellRange = projectsTable.Cell(2, 2).Range;
                cellRange.Text = project.PROJECT_DISCRIPTION.ToString();
                cellRange = projectsTable.Cell(2, 3).Range;
                cellRange.Text = allStaff.Where(p => p.PROJECT == project.PROJECT_ID).Count().ToString();

                projectsTable.Rows[1].Range.Bold = 1;
                projectsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            }
            Word.Range opinion = document.Paragraphs.Add().Range;
            opinion.Text = "Подпись:___________М.П.";

            application.Visible = true;
        }

        private void ExcelExport_Click(object sender, RoutedEventArgs e)
        {
            var allProjects = _context.PROJECTS.ToList().OrderBy(p => p.PROJECT_NAME).ToList();
            var users = VOLGA_EAS_DBEntities1.GetContext().USERS.ToList();
            var staff = VOLGA_EAS_DBEntities1.GetContext().PROJECT_STAFF.ToList();

            List<USER> gtij = new List<USER>();

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = allProjects.Count();

            int startRowIndex = 1;

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            for (int i = 0; i < allProjects.Count(); i++)
            {
                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name = allProjects[i].PROJECT_NAME;
                worksheet.StandardWidth = 35;

                var currentStaff = staff.Where(p => p.PROJECT == allProjects[i].PROJECT_ID);
                List<USER> currentUsers = new List<USER>();

                foreach (var thisUser in users)
                {
                    foreach (var thisStaff in currentStaff)
                    {
                        if (thisUser.USER_ID.Equals(thisStaff.USER))
                        {
                            currentUsers.Add(thisUser);
                        }
                    }
                }

                worksheet.Cells[1][startRowIndex] = "Название проекта";
                worksheet.Cells[2][startRowIndex] = "Описание проекта";
                worksheet.Cells[3][startRowIndex] = "Сотрудники";

                startRowIndex++;

                worksheet.Cells[1][startRowIndex] = allProjects[i].PROJECT_NAME;
                worksheet.Cells[2][startRowIndex] = allProjects[i].PROJECT_DISCRIPTION;

                foreach (var thisUser in currentUsers)
                {

                    foreach (var thisStaff in currentStaff)
                    {
                        if (thisUser.USER_ID.Equals(thisStaff.USER))
                        {
                            worksheet.Cells[3][startRowIndex] = thisUser.USER_NAME;
                            startRowIndex++;
                        }

                    }
                }

                currentUsers.Clear();
                startRowIndex++;

                worksheet.Cells[1][startRowIndex] = "Подпись:";
                worksheet.Cells[2][startRowIndex] = "М.П.";

                //for (int j = 3; j < currentStaff.Count() + 3; j++)
                //{
                //    for (int k = 0; k < currentStaff.Count(); k++)
                //    {
                //        worksheet.Cells[j][startRowIndex] = currentUsers.ToList()[k];
                //    }
                //}

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
