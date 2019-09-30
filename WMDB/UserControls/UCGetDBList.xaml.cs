using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace WMDB.UserControls
{
    /// <summary>
    /// Interaction logic for UCGetDBList.xaml
    /// </summary>
    public partial class UCGetDBList : UserControl
    {
        public UCGetDBList()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        GetSetValues GSV = new GetSetValues();
            
       protected struct GetSetValues
        {
           public string _databaseName;
            public string _tableName;
            public string _columnName;
            public string _columnValue;
            public string _sqlQueryToView;
        };


        public void BindData()
        {
            string sql = "SELECT name FROM sys.databases order by name";
            GetValuesFromDB(sql);
            if (FillComboBox(cmbDBName, false) == true)
            {
                UserSelectionGrid.Visibility = Visibility.Visible;
            }
            else
            {               
                UserSelectionGrid.Visibility = Visibility.Hidden;
            }
        }

        public void NavigationBarFunction(string DoWork)
        {
            switch (DoWork)
            {
                case "Refresh":
                    GetValuesFromDB(GSV._sqlQueryToView);
                    break;

                case "ViewQuery":
                    LabelStatus.Content = GSV._sqlQueryToView;
                LabelStatus.Foreground = Brushes.Green;
                break;

                case "CopyQuery":
                if (GSV._sqlQueryToView != "")
                {
                    System.Windows.Forms.Clipboard.SetText(GSV._sqlQueryToView);
                    LabelStatus.Content = "Copied to Clip Board";
                    LabelStatus.Foreground = Brushes.Green;
                }
                else {
                    LabelStatus.Content = "No querries found";
                    LabelStatus.Foreground = Brushes.Red;
                }
                    break;
            }
        }

        private void GetValuesFromDB(string sql)
        {
            try
            {
                GSV._sqlQueryToView = sql;
                string cs = @"Server = (local); Database =''; Trusted_Connection = Yes; ";
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                dt = new DataTable();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                LabelStatus.Content = "Error in Function GetValuesFromDB " + ex.Message;
            }
            finally
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ViewSQLInDataGrid();
                    }
                    else
                    {
                        SqlDetailsGrid.Visibility = Visibility.Hidden;
                        LabelStatus.Content = "No values found";
                        LabelStatus.Foreground = Brushes.DarkRed;
                    }
                }
                else
                {
                    SqlDetailsGrid.Visibility = Visibility.Hidden;
                    LabelStatus.Content = "No values found";
                    LabelStatus.Foreground = Brushes.DarkRed;
                }
            }
        }

        public Boolean FillComboBox(ComboBox combobox, Boolean GetColumnNames)
        {
            Boolean ComboBoxUpdated = true;
            try
            {
                LabelStatus.Content = "";
                if (GetColumnNames == true)
                {
                    int i = 0;
                    string[] DtColumnNames = new string[dt.Columns.Count];
                    foreach (DataColumn dc in dt.Columns)
                    {
                        DtColumnNames[i] = dc.ColumnName.ToString();
                        i++;
                    }
                    combobox.ItemsSource = DtColumnNames;
                    combobox.Visibility = Visibility.Visible;
                }
                else
                {
                    combobox.ItemsSource = dt.DefaultView;
                    combobox.DisplayMemberPath = dt.Columns[0].ToString();
                    combobox.SelectedValuePath = dt.Columns[0].ToString();
                    combobox.Visibility = Visibility.Visible;
                }
                ComboBoxUpdated = true;
            }
            catch (Exception ex)
            {
                ComboBoxUpdated = false;
                SqlDetailsGrid.Visibility = Visibility.Hidden;
                LabelStatus.Content = ex.Message;
                LabelStatus.FontSize = 10;
                LabelStatus.Foreground = Brushes.DarkRed;
            }
            return ComboBoxUpdated;
        }

        private void cmbDBName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDBName.SelectedValue != null)
            {
                GSV._databaseName = cmbDBName.SelectedValue.ToString();
                string sql = "SELECT name FROM " + GSV._databaseName + ".sys.tables order by name";
                GetValuesFromDB(sql);
                if (FillComboBox(cmbTableName, false) == true)
                {
                    TableNameGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    TableNameGrid.Visibility = Visibility.Hidden;
                }
            }
            ColumnNamesGrid.Visibility = Visibility.Hidden;
            ColumnValuesGrid.Visibility = Visibility.Hidden;
        }

        private void cmbTableName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTableName.SelectedValue != null)
            {
                GSV._tableName = cmbTableName.SelectedValue.ToString();
                string sql = "SELECT * FROM [" + GSV._databaseName + "].[dbo].[" + GSV._tableName + "]";
                GetValuesFromDB(sql);
                if (FillComboBox(cmbColumnNames, true) == true)
                {
                    ColumnNamesGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    ColumnNamesGrid.Visibility = Visibility.Hidden;
                }
            }
            ColumnValuesGrid.Visibility = Visibility.Hidden;
        }

        private void cmbColumnNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbColumnNames.SelectedValue != null)
            {
                GSV._columnName = cmbColumnNames.SelectedValue.ToString();
                string sql = "SELECT distinct(" + GSV._columnName + ") FROM [" + GSV._databaseName + "].[dbo].[" + GSV._tableName + "] order by " + GSV._columnName;
                GetValuesFromDB(sql);
                if (FillComboBox(cmbColumnValue, false) == true)
                {
                    ColumnValuesGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    ColumnValuesGrid.Visibility = Visibility.Hidden;
                }
            }
        }

        private void cmbColumnValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbColumnValue.SelectedValue != null)
            {
                GSV._columnValue = cmbColumnValue.SelectedValue.ToString();
                string sql = "SELECT * FROM [" + GSV._databaseName + "].[dbo].[" + GSV._tableName + "] where " + GSV._columnName + "='" + GSV._columnValue + "'";
                GetValuesFromDB(sql);
            }
        }

        public void ViewSQLInDataGrid()
        {
            SqlDataGrid.ItemsSource = dt.DefaultView;
            SqlDetailsGrid.Visibility = Visibility.Visible;
        }
    }
}