using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Documents;
using System.IO;

namespace WMDB.UserControls
{
    /// <summary>
    /// Interaction logic for UCReadData.xaml
    /// </summary>
    public partial class UCReadData : UserControl
    {
        public UCReadData()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            AppSpecific.SetButtonImage(ButtonExcelReader, "/Images/Excel-icon.png");
            AppSpecific.SetButtonImage(ButtonPdfReader, "/Images/pdf-icon.png");
            AppSpecific.SetButtonImage(ButtonWordReader, "/Images/Word-icon.png");
            AppSpecific.SetButtonImage(ButtonTextReader, "/Images/notepad-icon.png");
        }

        private void ButtonPdfReader_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            OpenFileDialog openpdf = new OpenFileDialog();
            openpdf.DefaultExt = ".pdf";
            openpdf.Filter = "PDF files(.pdf)|*.pdf";
            openpdf.ValidateNames = true;
            openpdf.Multiselect = false;
            try
            {
                if (openpdf.ShowDialog() == true)
                {
                    LabelStatus.Content = "Selected file: " + openpdf.FileName.ToString();

                    using (PdfReader pdfreader = new PdfReader(openpdf.FileName))
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i <= pdfreader.NumberOfPages; i++)
                        {
                            sb.Append(PdfTextExtractor.GetTextFromPage(pdfreader, i));
                        }

                        Paragraph para = new Paragraph();
                        para.Inlines.Add(sb.ToString());
                        FlowDocument.Blocks.Add(para);
                        RichTxtBox.Document = FlowDocument;
                        RichTxtBox.Visibility = Visibility.Visible;
                        pdfreader.Close();
                    }
                    FileReaderDataGrid.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                LabelStatus.Content = "Error message: " + ex.Message;
                FileReaderDataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonExcellReader_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "Microsoft Excel(.xlsx, .xls)|*.xlsx;*.xls";
            Nullable<bool> result = openfile.ShowDialog();
            if (result == true)
            {
                LabelStatus.Content = "Selected file: " + openfile.FileName.ToString();
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(openfile.FileName.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                string strCellData = "";
                double douCellData;
                int rowCnt = 0;
                int colCnt = 0;


                DataTable dt = new DataTable();
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    string strColumn = "";
                    strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                    dt.Columns.Add(strColumn, typeof(string));
                }

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    string strData = "";
                    for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                    {
                        try
                        {
                            strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += strCellData + "|";
                        }
                        catch (Exception ex)
                        {
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += douCellData.ToString() + "|";
                        }
                    }
                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }
                FileReaderDataGrid.ItemsSource = dt.DefaultView;
                FileReaderDataGrid.Visibility = Visibility.Visible;
                excelBook.Close(true, null, null);
                excelApp.Quit();
            }
        }
              

        private void ButtonWordReader_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            OpenFileDialog OpenWord = new OpenFileDialog();
            OpenWord.DefaultExt = ".doc";
            OpenWord.Filter = "Microsoft Word(.docx, .doc)|*.docx;*.doc";
            OpenWord.ValidateNames = true;
            OpenWord.Multiselect = false;
            try
            {
                if (OpenWord.ShowDialog() == true)
                {
                    LabelStatus.Content = "Selected file: " + OpenWord.FileName.ToString();

                    //string FileName = OpenWord.FileName;
                    //string[] SplitHTML = FileName.Split('.');
                    //string NameNoExt = SplitHTML[0];
                    //string FileAsHtml = NameNoExt + ".html";
                    //Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    //Microsoft.Office.Interop.Word.Document _doc = wordApp.Documents.Open(FileName);
                    //_doc.SaveAs2(FileAsHtml, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML);
                    //_doc.Close(false);
                    //wordApp.Quit();
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(_doc);
                    //wb.Visibility = Visibility.Visible;
                    //wb.Navigate(FileName);

                    StringBuilder text = new StringBuilder();
                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                    object miss = System.Reflection.Missing.Value;
                    object path = OpenWord.FileName;
                    object readOnly = true;
                    Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);

                    for (int i = 0; i < docs.Paragraphs.Count; i++)
                    {
                        text.Append(" \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString());
                    }
                    Paragraph para = new Paragraph();
                    para.Inlines.Add(text.ToString());
                    FlowDocument.Blocks.Add(para);
                    RichTxtBox.Document = FlowDocument;
                    RichTxtBox.Visibility = Visibility.Visible;
                    FileReaderDataGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    LabelStatus.Content = "";
                }
            }
            catch (Exception ex)
            {
                LabelStatus.Content = "Error message: " + ex.Message;
                FileReaderDataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonTextReader_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            OpenFileDialog OpenText = new OpenFileDialog();
            OpenText.DefaultExt = ".txt";
            OpenText.Filter = "Text File (.txt)|*.txt";
            OpenText.ValidateNames = true;
            OpenText.Multiselect = false;
            try
            {
                if (OpenText.ShowDialog() == true)
                {
                    LabelStatus.Content = "Selected file: " + OpenText.FileName.ToString();
                    string text = System.IO.File.ReadAllText(OpenText.FileName);
                    Paragraph para = new Paragraph();
                    para.Inlines.Add(text.ToString());
                    FlowDocument.Blocks.Add(para);
                    RichTxtBox.Document = FlowDocument;
                    RichTxtBox.Visibility = Visibility.Visible;
                    FileReaderDataGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    LabelStatus.Content = "";
                }
            }
            catch (Exception ex)
            {
                LabelStatus.Content = "Error message: " + ex.Message;
                FileReaderDataGrid.Visibility = Visibility.Hidden;
            }
        }
        private void HideAllGrids() 
        {
            FileReaderDataGrid.Visibility = Visibility.Hidden;
            RichTxtBox.Visibility = Visibility.Hidden;
            wbBrowser.Visibility = Visibility.Hidden;
            LabelStatus.Content = "";
        }

        public string ReadPdfFile(string fileName)
        {
            StringBuilder text = new StringBuilder();
            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            return text.ToString();
        }
    }
}