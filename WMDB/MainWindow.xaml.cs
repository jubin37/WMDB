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
            OnloadFunction();
        }

        public void OnloadFunction()
        {
            HideGrid();
            StartButtonsGrid.Visibility = Visibility.Visible;
            UserSelectionGrid.Visibility = Visibility.Hidden;
            SetButtonImage(btnClose, "/Images/close.png");
            SetButtonImage(btnMinimize, "/Images/minimize.png");
            SetButtonImage(btnHome, "/Images/Home.png");
        }

        public ImageBrush BindImage(Image img, string ImagePath)
        {
            ImageBrush MyBrush = new ImageBrush();
            img.Source = new BitmapImage(new Uri(ImagePath));
            MyBrush.ImageSource = img.Source;
            return MyBrush;
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            OnloadFunction();
        }

        private void btnGetDBName_Click(object sender, RoutedEventArgs e)
        {
            HideGrid();
            MyIspGrid.Visibility = Visibility.Hidden;
            string sql = "SELECT name FROM sys.databases order by name";
            GetValuesFromDB(sql, false);
            if (CheckValueExist(dt))
            {
                cmbDBName.ItemsSource = dt.AsDataView();
                cmbDBName.DisplayMemberPath = dt.Columns[0].ToString();
                cmbDBName.SelectedValuePath = dt.Columns[0].ToString();
                cmbDBName.SelectedValue = dt.Columns[0].ToString();
                SetLabelValues(LabelStatus, "", "");
                StartButtonsGrid.Visibility = Visibility.Hidden;
                UserSelectionGrid.Visibility = Visibility.Visible;
                ViewSQLInDataGrid();
            }
            else
            {
                SetLabelValues(LabelStatus, "No values Found", "#ff0000");
            }

        }

        public bool CheckValueExist(DataTable dt)
        {
            Boolean status = false;
            if (dt != null)
            {
                if (dt.Rows.Count > 1)
                {
                    status = true; 
                }
                else
                { 
                    SqlDetailsGrid.Visibility = Visibility.Hidden; 
                    status = false; 
                }                
            }
            else 
            { 
                SqlDetailsGrid.Visibility = Visibility.Hidden; 
                status = false; 
            }
            return status;
        }

        private void cmbDBName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GSV.Database = cmbDBName.SelectedValue.ToString();
            string sql = "SELECT name FROM " + GSV.Database + ".sys.tables order by name";
            GetValuesFromDB(sql, false);
            if (CheckValueExist(dt))
            {
                cmbTableName.ItemsSource = dt.AsDataView();
                cmbTableName.DisplayMemberPath = dt.Columns[0].ToString();
                cmbTableName.SelectedValuePath = dt.Columns[0].ToString();
                cmbTableName.SelectedValue = dt.Columns[0].ToString();
                SetLabelValues(LabelStatus, "", "");
                TableNameGrid.Visibility = Visibility.Visible;
                ViewSQLInDataGrid();
            }
            else
            {
                SetLabelValues(LabelStatus, "No values Found", "#ff0000");
            }
        }

        private void cmbTableName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GSV.Table = cmbTableName.SelectedValue.ToString();
            string sql = "SELECT * FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "]";
            GetValuesFromDB(sql, true);
            if (CheckValueExist(dt))
            {
                int i = 0;
                string[] DtColumnNames = new string[dt.Columns.Count];
                foreach (DataColumn dc in dt.Columns)
                {
                    DtColumnNames[i] = dc.ColumnName.ToString();
                    i++;
                }
                cmbColumnNames.ItemsSource = DtColumnNames;
                SetLabelValues(LabelStatus, "", "");
                ColumnNamesGrid.Visibility = Visibility.Visible;
                ViewSQLInDataGrid();
            }
            else
            {
                SetLabelValues(LabelStatus, "No values Found", "#ff0000");
            }
        }

        private void cmbColumnNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedColumnName = cmbColumnNames.SelectedValue.ToString();
            string sql = "SELECT distinct(" + selectedColumnName + ") FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "] order by " + selectedColumnName;
            GetValuesFromDB(sql, false);
            if (CheckValueExist(dt))
            {
                cmbColumnValue.ItemsSource = dt.AsDataView();
                cmbColumnValue.DisplayMemberPath = dt.Columns[0].ToString();
                cmbColumnValue.SelectedValuePath = dt.Columns[0].ToString();
                //cmbColumnValue.SelectedValue = dt.Columns[0].ToString();
                SetLabelValues(LabelStatus, "", "");
                ColumnValuesGrid.Visibility = Visibility.Visible;
                ViewSQLInDataGrid();
            }
            else
            {
                SetLabelValues(LabelStatus, "No values Found", "#ff0000");
            }
        }

        private void cmbColumnValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        public void ViewSQLInDataGrid()
        {
            SqlDataGrid.ItemsSource = dt.DefaultView;
            SqlDetailsGrid.Visibility = Visibility.Visible;
        }

        public void SetLabelValues(Label Label, string Text, string color)
        {
            if (Text != "")
            {
                Label.Content = Text;
            }
            else { 
                Label.Content = ""; 
            }
            if (color != "")
            {
                var converter = new System.Windows.Media.BrushConverter();
                var myBrush = (Brush)converter.ConvertFromString(color);
                Label.Foreground = myBrush;
            }
        }

        private void btnKnowISP_Click(object sender, RoutedEventArgs e)
        {
            SqlDetailsGrid.Visibility = Visibility.Hidden;
            MyIspWB.Source = new Uri("https://www.iptrackeronline.com/locate-ip-on-map-mini.php?lang=1");
            MyIspGrid.Visibility = Visibility.Visible;
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {

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
            SqlDetailsGrid.Visibility = Visibility.Hidden;
            MyIspGrid.Visibility = Visibility.Hidden;

            //DBandTableGrid.Visibility = Visibility.Hidden;
            //DBNamesGrid.Visibility = Visibility.Hidden;
            //TableNameGrid.Visibility = Visibility.Hidden;
            //ColumnNamesAndValuesGrid.Visibility = Visibility.Hidden;
            //ColumnNamesGrid.Visibility = Visibility.Hidden;
            //ColumnValuesGrid.Visibility = Visibility.Hidden;

        }


    }
}