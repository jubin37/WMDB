using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;


namespace WMDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        GetSetValues GSV = new GetSetValues();
        
        struct GetSetValues
        {
            public string Database;
            public string Table;
            public GetSetValues(string database, string tablename)
            {
                Database = database;
                Table = tablename;
            }
        };

        public MainWindow()
        {
            InitializeComponent();
            HideGrid();
            SetButtonImage(btnClose, "/Images/close.png");
            SetButtonImage(btnMinimize, "/Images/minimize.png");
        }    

        private void GetValuesFromDB(string sql, Boolean GetLastEntry)
        {
            string cs = @"Server = (local); Database =''; Trusted_Connection = Yes; ";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            con.Open();
            dt = new DataTable();
            sda.Fill(dt);
            if (GetLastEntry == true)
            {
               
            }
        }

        private void btnGetDBName_Click(object sender, RoutedEventArgs e)
        {
            HideGrid();
            MyIspGrid.Visibility = Visibility.Hidden;
            string sql = "SELECT name FROM sys.databases";
            GetValuesFromDB(sql, false);
            cmbDBName.ItemsSource = dt.AsDataView();
            cmbDBName.DisplayMemberPath = dt.Columns[0].ToString();
            cmbDBName.SelectedValuePath = dt.Columns[0].ToString();
            cmbDBName.SelectedValue = dt.Columns[0].ToString();
            DBandTableGrid.Visibility = Visibility.Visible;
            DBNamesGrid.Visibility = Visibility.Visible;
        }
        
        private void cmbDBName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GSV.Database = cmbDBName.SelectedValue.ToString();          
            string sql = "SELECT name FROM " + GSV.Database + ".sys.tables";
            GetValuesFromDB(sql, false);
            cmbTableName.ItemsSource = dt.AsDataView();
            cmbTableName.DisplayMemberPath = dt.Columns[0].ToString();
            cmbTableName.SelectedValuePath = dt.Columns[0].ToString();
            cmbTableName.SelectedValue = dt.Columns[0].ToString();
            TableNameGrid.Visibility = Visibility.Visible;
            ViewSQLInDataGrid();
        }

        private void cmbTableName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GSV.Table = cmbTableName.SelectedValue.ToString();            
            string sql = "SELECT * FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "]";
            GetValuesFromDB(sql, true);
            int i = 0;        
            string[] DtColumnNames = new string[dt.Columns.Count];
            foreach (DataColumn dc in dt.Columns)
            {
                DtColumnNames[i] = dc.ColumnName.ToString();
                i++;
            }
            cmbColumnNames.ItemsSource = DtColumnNames;
            ColumnNamesAndValuesGrid.Visibility = Visibility.Visible;
            ColumnNamesGrid.Visibility = Visibility.Visible;
         
            ViewSQLInDataGrid();
        }
        
        private void cmbColumnNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedColumnName = cmbColumnNames.SelectedValue.ToString();
            string sql = "SELECT distinct(" + selectedColumnName + ") FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "]";
            GetValuesFromDB(sql, false);
            cmbColumnValue.ItemsSource = dt.AsDataView();
            cmbColumnValue.DisplayMemberPath = dt.Columns[0].ToString();
            cmbColumnValue.SelectedValuePath = dt.Columns[0].ToString();
            cmbColumnValue.SelectedValue = dt.Columns[0].ToString();
            ColumnValuesGrid.Visibility = Visibility.Visible;
            ViewSQLInDataGrid();
        }

        private void cmbColumnValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataRow[] rows = dt.Select("Column1 = 'this'");
        }
    
        public void ViewSQLInDataGrid()
        {
            SqlDataGrid.ItemsSource = dt.DefaultView;
            SqlDetailsGrid.Visibility = Visibility.Visible;
        }

        private void bgworker()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgbar.Value = e.ProgressPercentage;
        }
               
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnKnowISP_Click(object sender, RoutedEventArgs e)
        {
            SqlDetailsGrid.Visibility = Visibility.Hidden;
            MyIspGrid.Visibility = Visibility.Visible;
        }

        public void SetButtonImage(Button Btn, string ImageName, string ButtonText = "")
        {
            try
            {
                Button b = new Button();
                b = Btn;
                b.Content = "";
                if (ButtonText != "")
                {
                    b.Content = ButtonText;
                }
                b.Background = LoadImage(GetPath(ImageName));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public ImageBrush LoadImage(string ImagePath)
        {
            ImageBrush MyBrush = new ImageBrush();
            Image Img = new Image();
            Img.Source = new BitmapImage(new Uri(ImagePath));
            MyBrush.ImageSource = Img.Source;
            return MyBrush;
        }

        public string GetPath(string PathName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + PathName;
        }

        private void HideGrid()
        {
            DBandTableGrid.Visibility = Visibility.Hidden;
            DBNamesGrid.Visibility = Visibility.Hidden;
            TableNameGrid.Visibility = Visibility.Hidden;
            ColumnNamesAndValuesGrid.Visibility = Visibility.Hidden;
            ColumnNamesGrid.Visibility = Visibility.Hidden;
            ColumnValuesGrid.Visibility = Visibility.Hidden;
            SqlDetailsGrid.Visibility = Visibility.Hidden;
            MyIspGrid.Visibility = Visibility.Hidden;
        }
    }
}