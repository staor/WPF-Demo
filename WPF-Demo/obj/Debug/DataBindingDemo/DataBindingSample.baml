   M S B A M L   `   `   `  ����C  ?WPF-Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WPF_Demo.DataBindingDemoWPF_Demo.DataBindingDemo  5      0    *WPF_Demo.DataBindingDemo.DataBindingSample   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:WPF_Demo.DataBindingDemo     5      -   5          N� Title$  DataBindingSample��5	       J� CommandBindings 5
      )  #System.Windows.Input.CommandBinding  5   
   -   5        0   *WPF_Demo.DataBindingDemo.DataBindingSample   SaveSqlDataCommand   Command# Z 5      5       � 	Resources 5      1  +WPF_Demo.DataBindingDemo.ImagePathConverter   imagePathConverter��%   &	          5   
   6#   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��0��6%    �� Orientation$ 
Horizontal=�62   $��8q�6K   .+�5      �� -   5        $��	10,0,10,0q�5      $��显示行细节��6*   6U   �� 5      -   5        $��	10,0,10,0q�5      $��显示分组��6*   6X   5      �� 5!   
   $��0��6   $��1��6%   $��8q�62   $	��100��6=   .+�5"      �� $��加载数据源��6   .��5#      �� .+�5$      	� -   5        $��	rbSqlData��5$   "   $��	0,10,0,10q�63   {�.  $��	SqlServer��6X   6l   	� 5%      -   5        $��	rbXmlData��5%   "   $��	0,10,0,10q�63   $	��XML��6F   6T   5&      5'      �� 5(      $��数据库存储��6   .��5)      �� .+�5*      �� -   5        $��	0,10,0,10q�5*      $��连接SqlServer��60   6F   �� 5+      -   5        $��btSaveSqlData��5+      $��	0,10,0,10q�62   $��存储SqlServer��6E   5,      5/      51      �� 52      $��数据库操作��6   .��53      �� .+�54      �� -	   5        $��btAddNew��54      $��0,5,0,5q�6-   $��	新增行��6>   6L   �� 55      -
   5        $��btDeleteRow��55      $��0,5,0,5q�60   $��	删除行��6A   6O   �� 56      ��6    ��  �� RelativeSource �� 	 �� AncestorType$
	 PageL�
 �� AncestorLevel$
 1�� �� Path$ HasChangedData�57      59      5:      &   System.Windows.Controls.DataGrid  5<   
   -   5          Name$ gridProducts��5<      $��1��6)   $��1��69   $��5q�6F      AutoGenerateColumns .     RowDetailsVisibilityMode$ 	Collapsed=�5=   /      CanUserAddRows .     RowStyle 5A      �� 5B       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5C      -   5        5D       �� 
GroupStyle 5G      (  "System.Windows.Controls.GroupStyle  5H         ContainerStyle 5I      �� 5J      " ��6    .�5K      ��  �� Property � �� �� Value 5L   "   �� 5M   &    �� 
TargetType" ��66   .R�5N   *   9�  9� 
IsExpanded . ��5O   .   �� 5P   2   ��6<   �� $
 Name�.��5R   .   �� 5S   +   5T   '   5V      5W         Panel 5Y      �� 5Z      .R�5[      >  8System.Windows.Controls.Primitives.DataGridRowsPresenter  5\      5^         RowDetailsTemplate 5a      �� 5b      .R�5c      �� $��3q�6    �� Padding$ 3q�6(   
�����F�$��3q�6L    �� CornerRadius$ 4��6`   ._�5d      �� $
��Wrap=�6A   $��12(�6U   $	��300��5e   z    �� TextAlignment$
 Left=�6�   ��5d   $   �� *Description+��5e   $   ��  �� *TemplatedParent+$ ActualWidth� � ToolTip 5f      �� 5g   "   ��6,   �� *Description+5i      5j      5k         Columns 5n      0  *System.Windows.Controls.DataGridTextColumn  5p      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ ID��6%     	  
IsReadOnly  . 1
  +System.Windows.Controls.DataGridBoundColumn! 
  Binding! 61   �� *	ProductID+6c     5q      $ Product��6%   " 	  Width5  /System.Windows.Controls.DataGridLengthConverter$	" 175 66   ! 6B   �� *	ModelName+6b     5r      $ Price��6%   ! 64   �� *
UnitCost+# �� StringFormat$# {0:C}��6h     5s      $ Model Number��6%   ! 6;   �� *ModelNumber+6]   4  .System.Windows.Controls.DataGridComboBoxColumn  5x      -   5        categoryColumn��$ CategoryName��5x   A   $   SelectedValuePath$$ 
CategoryID��5y   )   %   DisplayMemberPath$% CategoryName��6H   &   SelectedValueBinding& 5x   W   �� $ 
CategoryID�5y   k   4  .System.Windows.Controls.DataGridTemplateColumn  5�      -   5        
dateColumn��$ 
Data Added��5�   =   '   CellTemplate' 5�      �� 5�      .R�5�      �� $��Center=�6(   ��6C   �� $ 	DateAdded�$# {0:d}��6{   5�      (   CellEditingTemplate( 5�      �� 5�      .R�5�      (  "System.Windows.Controls.DatePicker  )   SelectedDate) 6)   �� $ 	DateAdded�6S   5�      5�      5�      5�      5�      