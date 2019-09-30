using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WMDB.UserControls
{
    /// <summary>
    /// Interaction logic for UCEncryptDecrypt.xaml
    /// </summary>
    public partial class UCEncryptDecrypt : UserControl
    {

        private void btnDecryptEncrypt_Click(object sender, RoutedEventArgs e)
        {
            //HideGrid();
            //StartButtonsGrid.Visibility = Visibility.Hidden;
            //NavigationButonsGrid.Visibility = Visibility.Visible;
            UserInputGrid.Visibility = Visibility.Visible;
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string textboxvalue = txtSelection.Text.ToString();
            DecryptText(textboxvalue);
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string textboxvalue = txtSelection.Text.ToString();
            EncryptText(textboxvalue);
        }

        public void EncryptText(string openText)
        {
            try
            {
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(openText);
                string encodedText = Convert.ToBase64String(bytesToEncode);
                LabelStatus.FontSize = 22;
                LabelStatus.Content = "Encoded Text: " + encodedText;
            }
            catch (Exception ex)
            {
                LabelStatus.Content = ex.Message;
            }
        }

        public void DecryptText(string encryptedText)
        {
            try
            {
                byte[] decodedBytes = Convert.FromBase64String(encryptedText);
                string decodedText = Encoding.UTF8.GetString(decodedBytes);
                LabelStatus.FontSize = 22;
                LabelStatus.Content = "Decoded Text: " + decodedText;
            }
            catch (Exception ex)
            {
                LabelStatus.Content = ex.Message;
            }
        }

    }
}
