   M S B A M L   `   `   `  ����F  BBillBarista, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BillBaristaBillBarista  5   	       BillBarista.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BillBarista     5   	   $��18(�5      $��RightToLeft=�6(     � Title$  Bill Barista��5   	   $	��350��6   $	��800��6+    � WindowState$ 	Maximized=�67    � 	Resources 5	      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%�   '        '�� L     �� 5
   
   "  6   .�5      ��  � ��$ Center=�6;   5      �� 5   
   " ��6   .�5      ��  � ��$ 36��6*   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5       $
��auto �6   �� 5!      $
��auto �6   �� 5"      $
��auto �6   �� 5#      $��* �6   �� 5$      $
��auto �6   �� 5%      $��20 �6   .+�5(   
   �� $��3��6   $��1��6    $��4q�6-   $L��Fpack://application:,,,/BillBarista;component/resources/BillBarista.png��5)      $��Right=�5*      $��4��6,   $��2��6=   $	��400��5+      $	��100��6   $	��100��6.   �� 5-   
   -   5         ��Name$ LoadMontlyFile��50      $ ��טען קובץ חודשי��5-      $��1��5.      $��1��6!   $��4q�5/      51      �� 53   
   -   5        $ LoadCatalogFile��56      $ ��טען קובץ קטלוג��53      $��1��54      $��2��6!   $��4q�55      57      �� 59   
   -   5        	 ��Name$	 ChooseVendor��59      $��1��5:      $��3��6#   $��4q�5;      
 �� SelectedIndex$
 1��5<      $��36��5=      .@�5?      �� .��6   בחר ספק6$   �� 5@      .��6   	Segment6$   5A      �� 5C   
   -   5        $ RunJob��5D      $	��!Go��5C      $��2��5E      $��1��6!   $��3��5F      $��1��6"   $��70��5G      $	��100��5H      $��25,4,4,4q�5I      6#   &   System.Windows.Controls.DataGrid  5U   
   -   5          Name$ Errors��5U         AutoGenerateColumns .     RowHeaderWidth$ 0Q�6?   $��1��5V      $��5��6#   $��4��60   $��4q�6D      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6O      CanUserAddRows .     Columns 5X      4  .System.Windows.Controls.DataGridTemplateColumn  5Z      ,  &System.Windows.Controls.DataGridColumn   Header$ מס' חשבונית��6)      CellTemplate 5[      �� 5\      .R�5]      �� $��LeftToRight=�6(   $��WrapWithOverflow=�5^   (   $��Center=�5_   (   ��5`   (   �� *InvoiceNumber+5a      5c        5e      $ גיליון��6)    5f      �� 5g      .R�5h      �� $��WrapWithOverflow=�5i   (   $��Center=�5j   (   ��5h   (   �� *
RecordType+5j   (   5k      5m        5o      $ שדה��6)    5p      �� 5q      .R�5r      �� $��LeftToRight=�6(   $��WrapWithOverflow=�5s   (   $��Center=�5t   (   ��5u   (   �� *	FieldName+5v      5x        5z      $ 
הבעיה��6)    5{      �� 5|      .R�5}      �� $��Center=�6(   $��WrapWithOverflow=�5~   (   ��5   (   �� *Issue+5�      5�        5�      $ ערך נוכחי��6)    5�      �� 5�      .R�5�      �� $��LeftToRight=�6(   $��Center=�5�   (   $��WrapWithOverflow=�5�   (   ��5�   (   �� *CurrentValue+5�      5�      5�      �� 5�   
   $0��*נבנה על ידי ישראל כהנוב��6   $��1��5�      $��6��6$   $��4q�61   $��3��5�      �� 5�   
   $��4��6   $��6��6$   $��4q�61   $��Right=�6<   �� 5�      $��http://www.google.com?�6   .�6<   לתמיכה לחץ כאן5�      5�      5�      5�      