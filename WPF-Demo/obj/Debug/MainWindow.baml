   M S B A M L   `   `   `  ����C  ?WPF-Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WPF_DemoWPF_Demo  5   	   7 clr-namespace:WPF_Demo.PhotoDemoWPF_Demo.PhotoDemo  5   	       WPF_Demo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WPF_Demo     5   	   /pd clr-namespace:WPF_Demo.PhotoDemo     5   	     � Title$  
MainWindow��5	   	   $	��650��6   $	��850��6)    � 	Resources 5
      ��%    5      .��5      N�  5   
      ExamplesDataTemplate �� ItemsSource �� XPath � ToolTip��%�   &	        �� 5       5   '   �� $ Children/Example��.R�5      �� $��18(�6:   ��6   �� $ @Title�� 6H   �� $ Description��5      5      .��5   
   ]� -   5        $��	mainPanel��5      .+�5      �� $
��Left=�6   
����Zj�$��0,0,2,0q�6C   ._�5      9�  9� ExpandDirection$
 Left=�6   ��5      �� 5      $
��18pt(�6$   目�� 5      录5      .��5      �� $
��Left=�6!   .+�5      R� -   5        $��
myPageList��5   #   -  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation$ Cycle=�5   #   
������ 
������ #���   5!   #   ��5"      �� 5#   "     ExampleData �� Source# [ 6*   $ *��6Q   5%      5&      5'      5(       � 5*      -   5        $��myFrame��5*        � Source$  PhotoDemo\PhotoSample.xaml?�6$   	  � NavigationUIVisibility$	 Hidden=�5+      $��Stretch=�5-      5/      50      51      