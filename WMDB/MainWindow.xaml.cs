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
        }

        private void btnGetDBName_Click(object sender, RoutedEventArgs e)
        {
            HideGrid();
            string cs = @"Server = (local); Database =''; Trusted_Connection = Yes; ";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT name FROM master.sys.databases", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            con.Open();
            sda.Fill(ds);

            cmbDBName.ItemsSource = ds.Tables[0].DefaultView;
            cmbDBName.DisplayMemberPath = ds.Tables[0].Columns["name"].ToString();
            cmbDBName.SelectedValuePath = ds.Tables[0].Columns["name"].ToString();
            cmbDBName.SelectedValue = "Select Below DB";
            DBandTableGrid.Visibility = Visibility.Visible;
            DBNamesGrid.Visibility = Visibility.Visible;
        }

        private void GetTableNames(DataSet dataSet, string tablename)
        {
            if (tablename != "")
            {
                foreach (DataTable table in dataSet.Tables)
                {

                    if (table.Columns[0].ColumnName == "Person")
                    {
                        MessageBox.Show("Got");
                    }

                    else { MessageBox.Show("Not Got"); }

                    //string gettable = Convert.ToString(ds.Tables["Table"].Rows[0][tablename]);

                    //string name = Convert.ToString(ds.Tables[0].Rows[0][tablename]);
                    //if (table.Rows.Contains(tablename))
                    //{
                    //    MessageBox.Show("Got");
                    //}
                    //else { MessageBox.Show("Not Got"); }
                }
            }
        }

        private void cmbDBName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string[] Gh = new string[ds.Tables[0].Rows.Count];
            foreach (DataTable table in ds.Tables)
            {
                int i = 0;
                foreach (DataRow dr in table.Rows)
                {
                    //Gh[i] = dr["name"].ToString();
                    //i++;
                    if(dr["name"].ToString() == cmbDBName.SelectedValue.ToString())
                    {
                        MessageBox.Show(cmbDBName.SelectedValue.ToString());
                    }
                }
            }

            //GetTableNames(ds, cmbDBName.SelectedValue.ToString());
            cmbTableName.ItemsSource = ds.Tables[0].DefaultView;
            cmbTableName.DisplayMemberPath = ds.Tables[0].Columns["name"].ToString();
            cmbTableName.SelectedValuePath = ds.Tables[0].Columns["name"].ToString();
            cmbTableName.SelectedValue = "Select Below DB";
            TableNameGrid.Visibility = Visibility.Visible;
        }

        private void cmbTableName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
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
