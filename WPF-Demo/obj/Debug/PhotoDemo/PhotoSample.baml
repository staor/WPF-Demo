   M S B A M L   `   `   `  ����C  ?WPF-Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WPF_Demo.PhotoDemoWPF_Demo.PhotoDemo  5      $    WPF_Demo.PhotoDemo.PhotoSample   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:WPF_Demo.PhotoDemo     5      -   5          N�Name$  photoDemoPage��5   .    N� Title$ 	PhotoDemo��5	       � 	Resources 5
      -  'WPF_Demo.PhotoDemo.UriToBitmapConverter   uriToBitmapConverter1  +WPF_Demo.PhotoDemo.DirectoryToNameConverter  directoryNameConverter/  )WPF_Demo.PhotoDemo.UriToFileNameConverter  uriToFileNameConverter  PhotoDirectoryDataTemplate �� 	Converter � ToolTip  PhotoCollectionDataTemplate �� IsAsync  stylePhotoList �� 
TargetType �� Property �� Value	 �� 
TargetType
 K� IsItemsHost � Orientation � 
ItemHeight �� ElementName �� Path � 	ItemWidth��%`  &	        &	      &	 &     &	 9     7  &	     7   &	 �      5   
   6%     5   
   6)     5   
   6'   �� 5   
   .R�5      � 
������ $��Stretch=�6,   .+�5      �� $��2q�6   $
��12pt(�5      $
��Bold"�6,   ��5   '   �� 8    5      �� 5      ��6$   �� 5      5      5      �� 5   
   .R�5      �� $��2q�6   ._�5      �� $	��200��6   ��6$   ��  . 8    5      �� 5       ��6%   �� 5#      5$      5%      �� 5'   
   " ��6'   .�5(      ��  � ��
 �����6+   �� 5)       � �� 5*      �� 5+      "	 ��6&   .R�5,      � $��5q�6$   
 . $ 
Horizontal=�6B   $	��Top=�5/   $   $��Stretch=�6<    5-   $   �� $ 
ZoomSlider��$ Value� 5.   $   �� $ 
ZoomSlider��$ Value�5/   <   50      52      54      56      .��57      �  � ColumnDefinitions 58   
   �� 59      $	��160 �6   �� 5:      $
��auto �6   �� 5;      $��* �6    � RowDefinitions 5=   
   �� 5>      $
��auto �6   �� 5?      .+�5A   
   �� -   5        $��8,0,8,0q�5A      .��6E   打开图像目录6M   �� 5B   
   $��1��6   $��2��6'   $��5��68   $��Stretch=�6B   �� 5C   
   $��0��6   $��1��6'   $
��Auto=�64   .��5D      �� -   5        $��listPhotoDirectory��5D      $��Stretch=�60   $��Stretch=�6N   #��[ 5E      5G      5H      ]� 5I   
   $��2��6   $��0��6$   .+�5J      �� -   5        $
��Left=�5J      $��	10,0,10,0q�6+   $��60��6>   .��6[   编辑6_   �� 5K      -   5        $��
ZoomSlider��5K   ,   $��Right=�6   $	��220��6>    � Minimum$ 40Q�6J    � Maximum$	 200Q�6W    �� TickFrequency$ 40Q�5L       �� TickPlacement$ BottomRight=�6(    � LargeChange$ 80Q�6D    � SmallChange$ 40Q�6U    � Value$	 160Q�6f   5M      �� 5N   
   $��2��6   $��1��6'   .��5O      �� -   5        $��	lbPhotoes��5O      #��[ 6'   #��[ 5P      $��3q�5Q       �� SelectionMode$ Single=�6!    � ContextMenu 5T      �� 5U      .@�5V      m� -   5        $��miEdit��5V   #   $��查看��61   6=   5W      5Y      5Z      5\      5]      