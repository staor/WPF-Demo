﻿#pragma checksum "..\..\..\PhotoDemo\PhotoDemo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B16C9FFB65B952DCA084236B7F26DC87"
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


namespace WPF_Demo.PhotoViewDemo {
    
    
    /// <summary>
    /// PhotoDemo
    /// </summary>
    public partial class PhotoDemo : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\PhotoDemo\PhotoDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPF_Demo.PhotoViewDemo.PhotoDemo photoDemoPage;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\PhotoDemo\PhotoDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listPhotoDirectory;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\PhotoDemo\PhotoDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider ZoomSlider;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\PhotoDemo\PhotoDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbPhotoes;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\PhotoDemo\PhotoDemo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miEdit;
        
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
            System.Uri resourceLocater = new System.Uri("/WPF-Demo;component/photodemo/photodemo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PhotoDemo\PhotoDemo.xaml"
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
            this.photoDemoPage = ((WPF_Demo.PhotoViewDemo.PhotoDemo)(target));
            
            #line 7 "..\..\..\PhotoDemo\PhotoDemo.xaml"
            this.photoDemoPage.Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 65 "..\..\..\PhotoDemo\PhotoDemo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectPhotoDirectory_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.listPhotoDirectory = ((System.Windows.Controls.ListBox)(target));
            
            #line 70 "..\..\..\PhotoDemo\PhotoDemo.xaml"
            this.listPhotoDirectory.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listPhotoDirectory_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 74 "..\..\..\PhotoDemo\PhotoDemo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditPhoto);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ZoomSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 6:
            this.lbPhotoes = ((System.Windows.Controls.ListBox)(target));
            
            #line 81 "..\..\..\PhotoDemo\PhotoDemo.xaml"
            this.lbPhotoes.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lbPhotoes_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.miEdit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 86 "..\..\..\PhotoDemo\PhotoDemo.xaml"
            this.miEdit.Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
