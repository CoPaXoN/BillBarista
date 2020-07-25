using BillBarista.Objects;
using BillBarista.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace BillBarista
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Errors.ItemsSource = ErrorsModel.errors;

        }

        string monthlyFilePath = "";
        MonthlyExcelFile monthlyExcelFile;
        ExcelFile catalogExcelFile;

        private string checkPathsExist(string error)
        {
            error = "";
            if (monthlyFilePath == "")
            {
                error = "קובץ חודשי לא נבחר";
            }
            if (error != "")
            {
                error += Environment.NewLine;
            }
            if (catalogPath == "")
            {
                error += "קובץ קטלוג לא נבחר";
            }
            return error;
        }

        //int runJobCounter = 0;
        private void RunJob_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            error = checkPathsExist(error);

            if (error != "")
            {
                MessageBox.Show(error);
                return;
            }

            //runJobCounter++;
            //if(runJobCounter == 1)
            //{
            //    RunJobFunc();
            //}
            //else
            //{
            //    Fix();
            //}

            RunJobFunc();
            
        }

        private async void RunJobFunc()
        {
            RunJob.IsEnabled = false;
            Task<bool> runJobAsync = new Task<bool>(RunJobAsync);
            runJobAsync.Start();
            bool result = await runJobAsync;
            RunJob.IsEnabled = true;
            Errors.Items.Refresh();
        }

        
        private bool RunJobAsync()
        {
            ErrorsModel.errors.Clear();

            monthlyExcelFile = new MonthlyExcelFile(monthlyFilePath);

            //create copy
            monthlyExcelFile.SaveAs(MonthlyExcelFile.tempPath);

            //close original
            monthlyExcelFile.CleanUp();

            //point to copy
            monthlyExcelFile = new MonthlyExcelFile(MonthlyExcelFile.tempPath);

            //load catalog
            catalogExcelFile = new ExcelFile(catalogPath);

            //export to txt file
            new ExportToText(monthlyExcelFile, catalogExcelFile);



            return true;
        }

        //private bool Fix()
        //{
        //    updateCorrectValue();
        //    foreach(Error error in ErrorsModel.errors)
        //    {
        //        if(error.CorrectValue != "" && error.CorrectValue != null)
        //        {
        //            if (error.WorksheetIndex == Sheets.InvoicesSheetNumber)
        //            {
        //                monthlyExcelFile.workbook.Sheets[Sheets.InvoicesSheetNumber].Cells[error.Row, error.Column] = error.CorrectValue;
        //            }

        //            if (error.WorksheetIndex == Sheets.OrdersSheetNumber)
        //            {
        //                monthlyExcelFile.workbook.Sheets[Sheets.OrdersSheetNumber].Cells[error.Row, error.Column] = error.CorrectValue;
        //            }
                    
        //            if (error.WorksheetIndex == Sheets.LinesSheetNumber)
        //            {
        //                monthlyExcelFile.workbook.Sheets[Sheets.LinesSheetNumber].Cells[error.Row, error.Column] = error.CorrectValue;
        //            }
        //        }
        //    }
        //    //export to txt file
        //    new ExportToText(monthlyExcelFile, catalogExcelFile);
        //    return true;
        //}

        private async void LoadMontlyFile_Click(object sender, RoutedEventArgs e)
        {
            filter = "XLSX Files (*.xlsx)|*.xlsx";
            Task<string> getFilePath = new Task<string>(GetFilePath);
            getFilePath.Start();
            monthlyFilePath = await getFilePath;

            if (monthlyFilePath == "")
            {
                //MessageBox.Show("לא נטען בהצלחה");
                return;
            }
        }

        string catalogPath = "";
        private async void LoadCatalogFile_Click(object sender, RoutedEventArgs e)
        {
            filter = "XLSX Files (*.xlsx)|*.xlsx";
            Task<string> getFilePath = new Task<string>(GetFilePath);
            getFilePath.Start();
            catalogPath = await getFilePath;
        }

        string filter;
        public string GetFilePath()
        {
            //open file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return "";
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateCorrectValue()
        {
            int row = 0;
            foreach (Error error in Errors.Items)
            {
                ContentPresenter contentPresnster = Errors.Columns[5].GetCellContent(error) as ContentPresenter;
                var contentTemplate = contentPresnster.ContentTemplate;
                TextBox textBox = contentTemplate.FindName("correctValue", contentPresnster) as TextBox;
                if (textBox.Text != "")
                {
                    ErrorsModel.errors[row].CorrectValue = textBox.Text;
                }
                row++;
            }
            Errors.Items.Refresh();
        }
    }
}
