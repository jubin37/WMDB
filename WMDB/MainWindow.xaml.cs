using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


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

        public MainWindow()
        {
            InitializeComponent();
            HideGrid();
           
        }

        private void HideGrid()
        {
            DBandTableGrid.Visibility = Visibility.Hidden;
            DBNamesGrid.Visibility = Visibility.Hidden;
            TableNameGrid.Visibility = Visibility.Hidden;
            ColumnNamesGrid.Visibility = Visibility.Hidden;
            AllColumnNamesGrid.Visibility = Visibility.Hidden;
            ColumnNameGrid.Visibility = Visibility.Hidden;
            LabelValue.Visibility = Visibility.Hidden;
            SqlDataGrid.Visibility = Visibility.Hidden;
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

        private void cmbDBName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedDBname = cmbDBName.SelectedValue.ToString();
            GSV.Database = selectedDBname;
            string sql = "SELECT name FROM " + GSV.Database + ".sys.tables";
            GetValuesFromDB(sql);
            cmbTableName.ItemsSource = dt.AsDataView();
            cmbTableName.DisplayMemberPath = dt.Columns[0].ToString();
            cmbTableName.SelectedValuePath = dt.Columns[0].ToString();
            cmbTableName.SelectedValue = dt.Columns[0].ToString(); ;
            TableNameGrid.Visibility = Visibility.Visible;     
        }

        private void cmbTableName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedtablename = cmbTableName.SelectedValue.ToString();
            GSV.Table = selectedtablename;
            string sql = "SELECT * FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "]";
            GetValuesFromDB(sql);
            SqlDataGrid.Visibility = Visibility.Visible;
            SqlDataGrid.ItemsSource = dt.DefaultView;
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

      }
}
