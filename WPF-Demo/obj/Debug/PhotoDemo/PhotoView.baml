   M S B A M L   `   `   `  ����C  ?WPF-Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WPF_Demo.PhotoDemoWPF_Demo.PhotoDemo  5   	   "    WPF_Demo.PhotoDemo.PhotoView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:WPF_Demo.PhotoDemo     5   	   -   5          � Title$  	PhotoView��5   	   $	��600��6   $	��800��6(    � 	Resources 5	         TfGroup �� ScaleX �� ScaleY T� X T� Y��%t   &	        V� 5
   
   .~�5      �� $ 1Q�6   $ 1Q�6(   T� 5      $ 0Q�6!   $ 0Q�6'   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��140 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� $��1��6   $��0��6!   $��Stretch=�6.   $��Stretch=�6L   z�5      T� 5      $ 5Q�6%   $ 5Q�6+    J� BitmapEffect 5      �� 5      	 �� Radius$	 8Q�6#   5      �� 5"      -   5        
 ��Name$
 mainScrollv��5"      $��Center=�60   $��Center=�6M   $��Disabled=�6h   $��Disabled=�5#       � Cursor$ SizeAll��6>   $��0q�6O   |�.  $��0��6l   $��1��6y   .��5$      �� -   5        $
 Hand��5$      $��Center=�5'      $��Center=�6:   .��5(      �� -   5        $��	imageView��5(      #z�[  6)    �� BitmapScalingMode$ NearestNeighbor=�6T   5)      5*      �� 5,   
   $��0��6   $��0��6!    �� Padding$ 4q�6.   ._�5-      �� .+�5.      �� -   5        $��
lablelName��5.   @   $��5q�6   $��Center=�6#   6@   �� 5/      -   5        $��	5,10,5,10q�5/      $
��Crop��6,   {�.  6M   �� 50      -   5        $��	5,10,5,10q�50      $��Rotate��6,   6=   �� 51      -   5        $��ckBlackWhite��51      $��	5,10,5,10q�6/   $��BlackAndWhite��6B   6x   �� 52      -	   5        $��	5,10,5,10q�52      $��Default��6,   6>   �� 53      -
   5        $��	5,10,5,10q�53      $
��Save��6,   6;   �� 54      -   5        $��	5,10,5,10q�54      $
��Exit��6,   6;   55      56      �� 57   
   $��0��6   $��1��6%    �� Orientation$ 
Horizontal=�62   $��2��6K   .+�58      �� -   5        $��
tbFullName��58      59      5:      5;      