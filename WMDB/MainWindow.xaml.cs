using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
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
        GetSelectedValues GSV = new GetSelectedValues();


        struct GetSelectedValues
        {
            public string Database;
            public string Table;
            public GetSelectedValues(string database, string tablename)
            {
                Database = database;
                Table = tablename;
            }
        };

        public MainWindow()
        {
            InitializeComponent();
            HideGrid();
            CloseBtnfn();
            MinimizeBtnfn();
        }


        public void CloseBtnfn() 
        {  
            var imagepath = new Uri(GetImagePath("/Images/close.png"));
            var bitmap = new BitmapImage(imagepath);
            CloseBtn.Source = bitmap;}

        public void MinimizeBtnfn()
        {
            var imagepath = new Uri(GetImagePath("/Images/min.png"));
            var bitmap = new BitmapImage(imagepath);
            MinimizeBtn.Source = bitmap;
        }

        public ImageBrush LoadImage(string ImagePath, Image Imagename)
        {
            ImageBrush MyBrush = new ImageBrush();
            Image Img = new Image();
            Img.Source = new BitmapImage(new Uri(ImagePath));
            MyBrush.ImageSource = Img.Source;
            return MyBrush;
        }


        public string GetImagePath(string ImagePath)
        {
            return AppDomain.CurrentDomain.BaseDirectory + ImagePath;
        }
        private void HideGrid()
        {
            DBandTableGrid.Visibility = Visibility.Hidden;
            DBNamesGrid.Visibility = Visibility.Hidden;
            TableNameGrid.Visibility = Visibility.Hidden;
            ColumnNamesGrid.Visibility = Visibility.Hidden;
            SqlDataGrid.Visibility = Visibility.Hidden;
        }

        private void GetValuesFromDB(string sql)
        {
            string cs = @"Server = (local); Database =''; Trusted_Connection = Yes; ";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            con.Open();
            dt = new DataTable();
            sda.Fill(dt);
        }

        private void btnGetDBName_Click(object sender, RoutedEventArgs e)
        {
            HideGrid();
            string sql = "SELECT name FROM sys.databases";
            GetValuesFromDB(sql);
            cmbDBName.ItemsSource = dt.AsDataView();
            cmbDBName.DisplayMemberPath = dt.Columns[0].ToString();
            cmbDBName.SelectedValuePath = dt.Columns[0].ToString();
            cmbDBName.SelectedValue = dt.Columns[0].ToString();
            DBandTableGrid.Visibility = Visibility.Visible;
            DBNamesGrid.Visibility = Visibility.Visible;
        }



        private void cmbDBName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedDBname = cmbDBName.SelectedValue.ToString();
            GSV.Database = selectedDBname;
            string sql = "SELECT name FROM " + GSV.Database + ".sys.tables";
            GetValuesFromDB(sql);
            cmbTableName.ItemsSource = dt.AsDataView();
            cmbTableName.DisplayMemberPath = dt.Columns[0].ToString();
            cmbTableName.SelectedValuePath = dt.Columns[0].ToString();
            cmbTableName.SelectedValue = dt.Columns[0].ToString();
            TableNameGrid.Visibility = Visibility.Visible;
        }

        private void cmbTableName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedtablename = cmbTableName.SelectedValue.ToString();
            GSV.Table = selectedtablename;
            //string sql = "Use " + GSV.Database + "@ GO@ select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='[" + GSV.Table + "]'";
            string sql = "SELECT * FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "]";

            GetValuesFromDB(sql);
            int i = 0;
        
            string[] DtColumnNames = new string[dt.Columns.Count];

            foreach (DataColumn dc in dt.Columns)
            {
                DtColumnNames[i] = dc.ColumnName.ToString();
                i++;
            }

            cmbAllColumnNames.ItemsSource = DtColumnNames;
            //cmbAllColumnNames.SelectedValue = DtColumnNames[0]; 
            ColumnNamesGrid.Visibility = Visibility.Visible;
            AllColumnNamesGrid.Visibility = Visibility.Visible;
            SqlDataGrid.ItemsSource = dt.DefaultView;
            SqlDataGrid.Visibility = Visibility.Visible;
            //ViewSQLInDataGrid();
        }

        public void ViewSQLInDataGrid()
        {
            string sql = "SELECT * FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "]";
            GetValuesFromDB(sql);
            SqlDataGrid.ItemsSource = dt.DefaultView;
            SqlDataGrid.Visibility = Visibility.Visible;
        }

        private void cmbColumnName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void cmbLabelValue_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void cmbAllColumnNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void cmbLabelValue_SelectionChanged_1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void cmbColumnName_SelectionChanged_1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

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
    }
}
