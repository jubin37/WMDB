   M S B A M L   `   `   `  ����  WMDB"    WMDB.UserControls.UCReadData   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � -   5        $��MainGrid��5        � RowDefinitions  5   
   �� 5      $��5* �6   �� 5	      $	��30* �6   �� 5
      $��5* �6   �� 5      $	��60* �6   .+�5   
   �� -   5        $��labelSelection��5         
LabelStyle#��[  6&   $��Select the file type��6K   6l   � 5   
   -   5        $��UserSelectionGrid��5      $��1��6(    � ColumnDefinitions 5      �� 5      $	��25* �6#   �� 5      $	��25* �6#   �� 5      $	��25* �6#   �� 5      $	��25* �6#   .+�5      �� -   5         ��Name$ ButtonPdfReader��5        RoundedButton#��[ 6.   
������ 
������ $��30q�6�   6�   �� 5      -   5        $ ButtonExcelReader��5      #��[ 60   
������ 
������ $��30q�6�   $��1��6�   �� 5      -   5        $ ButtonWordReader��5      #��[ 6/   
������ 
������ $��30q�6�   $��2��6�   �� 5      -   5        $ ButtonTextReader��5      #��[ 6/   
������ 
������ $��35q�6�   $��3��6�   5      � 5   
   -   5        $��
StatusGrid��5      $��2��6!   .+�5      �� -	   5        $��LabelStatus��5        LabelRequerStyle#��[ 6'   $��Center=�6Q   $��Center=�6u   6�   5      � 5   
   -
   5        $��DataDisplayGrid��5      $��3��6&   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��FileReaderDataGrid��5   )      
IsReadOnly . $y�Hidden=�6C   �� 5      -   5        $��
RichTxtBox��5      $y�Hidden=�6,   . �5       /� -   5         �Name$ FlowDocument��5       65   5!      (  "System.Windows.Controls.WebBrowser  5"      -   5        $��	wbBrowser��5"      $y�Hidden=�6*   6@   5#      5$      5%      