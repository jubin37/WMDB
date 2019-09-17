using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Security.Cryptography;

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
            public string ColumnName;
            public string ColumnValue;
        };

        public MainWindow()
        {
            InitializeComponent();
            SetButtonImage(btnClose, "/Images/close.png");
            SetButtonImage(btnMinimize, "/Images/minimize.png");
            SetButtonImage(btnHome, "/Images/Home.png");
            //https://www.aspsnippets.com/Articles/Encrypt-and-Decrypt-Username-or-Password-stored-in-database-in-ASPNet-using-C-and-VBNet.aspx
            //https://www.c-sharpcorner.com/blogs/how-to-encrypt-or-decrypt-password-using-asp-net-with-c-sharp1
            string pass = "µ¦´µ¦³’“”•";
            //EncodePasswordToBase64("Tester");
            //DecodeFrom64(pass.Trim());
            OnloadFunction();
        }

        public void OnloadFunction()
        {
            HideGrid();
            StartButtonsGrid.Visibility = Visibility.Visible;
            UserSelectionGrid.Visibility = Visibility.Hidden;
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
            try
            {
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
            if (FillComboBox(cmbDBName, false) == true)
            {
                StartButtonsGrid.Visibility = Visibility.Hidden;
                UserSelectionGrid.Visibility = Visibility.Visible;
            }
            else
            {
                StartButtonsGrid.Visibility = Visibility.Visible;
                UserSelectionGrid.Visibility = Visibility.Hidden;
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
                GSV.Database = cmbDBName.SelectedValue.ToString();
                string sql = "SELECT name FROM " + GSV.Database + ".sys.tables order by name";
                GetValuesFromDB(sql, false);
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
                GSV.Table = cmbTableName.SelectedValue.ToString();
                string sql = "SELECT * FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "]";
                GetValuesFromDB(sql, true);
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
                GSV.ColumnName = cmbColumnNames.SelectedValue.ToString();
                string sql = "SELECT distinct(" + GSV.ColumnName + ") FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "] order by " + GSV.ColumnName;
                GetValuesFromDB(sql, false);
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
                GSV.ColumnValue = cmbColumnValue.SelectedValue.ToString();
                string sql = "SELECT * FROM [" + GSV.Database + "].[dbo].[" + GSV.Table + "] where " + GSV.ColumnName + "='" + GSV.ColumnValue + "'";
                GetValuesFromDB(sql, false);
            }
        }

        public void ViewSQLInDataGrid()
        {
            SqlDataGrid.ItemsSource = dt.DefaultView;
            SqlDetailsGrid.Visibility = Visibility.Visible;
        }

        public void SetLabelValues(string Text, string color)
        {
            if (Text != "")
            {
                LabelStatus.Content = Text;
            }
            else
            {
                LabelStatus.Content = "";
            }
            if (color != "")
            {
                var converter = new System.Windows.Media.BrushConverter();
                var myBrush = (Brush)converter.ConvertFromString(color);
                LabelStatus.Foreground = myBrush;
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


        private void btnSearchDB_Click(object sender, RoutedEventArgs e)
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
        }


        public bool CheckValueExist(DataTable dt)
        {
            LabelStatus.Content = "";
            Boolean status = false;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    status = true;
                }
                else
                {
                    SqlDetailsGrid.Visibility = Visibility.Hidden;
                    LabelStatus.Content = "No values found";
                    LabelStatus.Foreground = Brushes.DarkRed;
                    status = false;
                }
            }
            else
            {
                SqlDetailsGrid.Visibility = Visibility.Hidden;
                LabelStatus.Content = "No values found";
                LabelStatus.Foreground = Brushes.DarkRed;
                status = false;
            }
            return status;
        }
        public void EncodePasswordToBase64(string password)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                    SetLabelValues(password, "#ff0000");
                }
            }
        }

        public void DecodeFrom64(string encodedData)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(encodedData);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    encodedData = Encoding.Unicode.GetString(ms.ToArray());
                    SetLabelValues(encodedData, "#ff0000");
                }
            }
        }

    }
}