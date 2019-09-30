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
using System.Web;
using System.Net;

namespace WMDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            AppSpecific.SetButtonImage(btnClose, "/Images/close.png");
            AppSpecific.SetButtonImage(btnMinimize, "/Images/minimize.png");
            AppSpecific.SetButtonImage(btnHome, "/Images/Home.png");
            AppSpecific.SetButtonImage(btnRefresh, "/Images/Refresh.png");
            AppSpecific.SetButtonImage(btnViewQuery, "/Images/View.png");
            AppSpecific.SetButtonImage(btnCopyQuery, "/Images/Copy.png");
            OnloadFunction();
        }

        public void OnloadFunction()
        {
            HideGrid();
            StartButtonsGrid.Visibility = Visibility.Visible;  
        }

        private void HideGrid()
        {
            GetDBListGrid.Visibility = Visibility.Hidden;
            MyIspGrid.Visibility = Visibility.Hidden;
            StartButtonsGrid.Visibility = Visibility.Hidden;
            NavigationButonsGrid.Visibility = Visibility.Hidden;
            ReadDataGrid.Visibility = Visibility.Hidden;
        }

        //public ImageBrush BindImage(Image img, string ImagePath)
        //{
        //    ImageBrush MyBrush = new ImageBrush();
        //    img.Source = new BitmapImage(new Uri(ImagePath));
        //    MyBrush.ImageSource = img.Source;
        //    return MyBrush;
        //}

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            OnloadFunction();
        }

        private void btnGetDBName_Click(object sender, RoutedEventArgs e)
        {
            HideGrid();
            NavigationButonsGrid.Visibility = Visibility.Visible;
            GetDBListGrid.Visibility = Visibility.Visible;
            UCGetDBList.BindData();
        }        

        private void btnKnowISP_Click(object sender, RoutedEventArgs e)
        {
            HideGrid();
            NavigationButonsGrid.Visibility = Visibility.Visible;
            MyIspGrid.Visibility = Visibility.Visible;
            UCMyISP.BindData();
        }

        private void btnReadData_Click(object sender, RoutedEventArgs e)
        {
            HideGrid();
            NavigationButonsGrid.Visibility = Visibility.Visible;
            ReadDataGrid.Visibility = Visibility.Visible;
            UCReadData.BindData();
            //UserControls.UCReadData ReadData = new UserControls.UCReadData();
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
        
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {            
            UCGetDBList.NavigationBarFunction("Refresh");   
        }

        private void btnViewQuery_Click(object sender, RoutedEventArgs e)
        {
            UCGetDBList.NavigationBarFunction("ViewQuery");
        }

        private void btnCopyQuery_Click(object sender, RoutedEventArgs e)
        {
            UCGetDBList.NavigationBarFunction("CopyQuery");      
        }
        
    }
}