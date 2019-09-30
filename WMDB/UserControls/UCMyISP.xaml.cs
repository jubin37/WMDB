using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace WMDB.UserControls
{
    /// <summary>
    /// Interaction logic for UCMyISP.xaml
    /// </summary>
    public partial class UCMyISP : UserControl
    {
        public UCMyISP()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            MyIspWB.Visibility = Visibility.Visible;
            labelPleaseWait.Visibility = Visibility.Hidden;
            //MyIspWB.Navigate("https://www.iptrackeronline.com/locate-ip-on-map-mini.php?lang=1");
            MyIspWB.Source = new Uri("https://www.iptrackeronline.com/locate-ip-on-map-mini.php?lang=1");
        }

        private void MyIspWB_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            labelPleaseWait.Visibility = Visibility.Hidden;
            MyIspWB.Visibility = Visibility.Visible;
        }

        private void MyIspWB_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            MyIspWB.Visibility = Visibility.Hidden;
            labelPleaseWait.Visibility = Visibility.Visible;
        }
    }
}
