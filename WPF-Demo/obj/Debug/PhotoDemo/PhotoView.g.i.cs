﻿#pragma checksum "..\..\..\PhotoDemo\PhotoView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B798C51CBE0951787A8A482E4AF7748"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WPF_Demo.PhotoDemo;


namespace WPF_Demo.PhotoDemo {
    
    
    /// <summary>
    /// PhotoView
    /// </summary>
    public partial class PhotoView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\PhotoDemo\PhotoView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer mainScrollv;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\PhotoDemo\PhotoView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageView;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\PhotoDemo\PhotoView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lablelName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\PhotoDemo\PhotoView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ckBlackWhite;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\PhotoDemo\PhotoView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbFullName;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WPF-Demo;component/photodemo/photoview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PhotoDemo\PhotoView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 7 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((WPF_Demo.PhotoDemo.PhotoView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((WPF_Demo.PhotoDemo.PhotoView)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Window_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainScrollv = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 3:
            
            #line 36 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.ContentControl)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ContentControl_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 37 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.ContentControl)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ContentControl_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.ContentControl)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.ContentControl_MouseMove);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.ContentControl)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.ContentControl_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 4:
            this.imageView = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.lablelName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            
            #line 47 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Crop_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 48 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Rotate_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ckBlackWhite = ((System.Windows.Controls.CheckBox)(target));
            
            #line 49 "..\..\..\PhotoDemo\PhotoView.xaml"
            this.ckBlackWhite.Checked += new System.Windows.RoutedEventHandler(this.BlackAndWhite_Click);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\PhotoDemo\PhotoView.xaml"
            this.ckBlackWhite.Unchecked += new System.Windows.RoutedEventHandler(this.BlackAndWhite_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 50 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Defualt_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 51 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 52 "..\..\..\PhotoDemo\PhotoView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.tbFullName = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

