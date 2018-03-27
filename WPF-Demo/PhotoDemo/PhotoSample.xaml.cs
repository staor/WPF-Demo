using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using WPF_Demo.PhotoDemo;

namespace WPF_Demo.PhotoDemo
{
    /// <summary>
    /// PhotoDemo.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoSample : Page
    {
        private ObservableCollection<string> _listDirectory=new ObservableCollection<string>();
        private ObservableCollection<Uri> _photoCollection=new ObservableCollection<Uri>();
        //private ObservableCollection<Photo> _photoes = new ObservableCollection<Photo>();//ImageFrame不能用于异步多线程
        private object lockObject=new object(); // 用于线程间同步的对象
        public PhotoSample()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 选择资源管理器中的目录Form窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectPhotoDirectory_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Interop.HwndSource source = PresentationSource.FromVisual(this) as System.Windows.Interop.HwndSource;
            System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
            System.Windows.Forms.DialogResult result = dlg.ShowDialog(win);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string dir = dlg.SelectedPath;
                if (_listDirectory.Contains(dir))
                {
                    return;
                }
                _listDirectory.Add(dir);
            }
        }
        //页面加载
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ListBox绑定
            _listDirectory.Add(@"..\..\sampleImages");
            listPhotoDirectory.ItemsSource = _listDirectory;
            //异步绑定
            Binding binding = new Binding
            {
                Source = _photoCollection,
                IsAsync = true                 
            };
            lbPhotoes.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            // 这一句很关键，开启集合的异步访问支持
            BindingOperations.EnableCollectionSynchronization(_photoCollection, lockObject);
        }
        //双击鼠标打开对应图片进行浏览
        private void lbPhotoes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowPhotoViewerWindow();
        }        
        //编辑图片
        private void EditPhoto(object sender, RoutedEventArgs e)
        {
             PiantPhoto();
        }

        //异步加载目录下图片集
        private void listPhotoDirectory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = listPhotoDirectory.SelectedItem as string;
            if (Directory.Exists(path))
            {
                _photoCollection.Clear();
                DirectoryInfo di = new DirectoryInfo(path);                
                Task.Run(() =>  // 异步加载数据
                {
                    lock (lockObject) //锁定对象
                    {
                        //di.GetFiles()只能设置一种类型参数
                        var files = di.GetFiles().Where(s => 
                          s.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) |
                          s.FullName.EndsWith("*.png",StringComparison.OrdinalIgnoreCase) | 
                          s.FullName.EndsWith("*.bmp", StringComparison.OrdinalIgnoreCase));
                        foreach (var item in files) 
                        {
                            Uri uri = new Uri(item.FullName);
                            _photoCollection.Add(uri);
                        }

                    }
                });
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowPhotoViewerWindow();
        }
        // 显示照片查看窗口
        private void ShowPhotoViewerWindow()
        {
            Uri uri = lbPhotoes.SelectedItem as Uri;
            if (uri != null)
            {
                PhotoView pv = new PhotoView
                {
                    //parentPage = this, //不需要引用父页，以防忘记释放引起bug
                    photoUri = uri
                };
                pv.ShowDialog();
                UpdateCollection(uri);
            }
        }
        //使用系统自带图片编辑，对原始图片修改
        private void PiantPhoto()
        {
            Uri uri = lbPhotoes.SelectedItem as Uri;
            if (uri != null)
            {
                string filePath = string.Format("\"{0}\"", uri.OriginalString);
                ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
                processStartInfo.FileName = "mspaint.exe"; //调用系统自带绘图工具
                processStartInfo.Arguments = filePath;
                Process process = System.Diagnostics.Process.Start(processStartInfo);
                process.EnableRaisingEvents = true;
                process.WaitForExit();

                UpdateCollection(uri);
            }
        }
        //更新集合的选定子项内容
        public void UpdateCollection(Uri uri)
        {
            GC.Collect(); //解决PhotoView窗口关闭后再次打开同一图片文件偶尔显示不更新问题（集合对应的图片实际已更新显示）
            int index = _photoCollection.IndexOf(uri);
            _photoCollection.Remove(uri);
            //_photoCollection.Add(uri);  
            _photoCollection.Insert(index, uri);//重新加载编辑后的原始图片
        }
        ////右键通过可视化树寻找知道类型父类目标。
        //private void lbPreviewMouseRightButtonDown_MouseDown(object sender, MouseButtonEventArgs e)
        //{
            //ListBoxItem lbi = VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem;
            //lbi.IsSelected = true;
            //lbi.Focus();
            //MessageBox.Show(lbi.ToString());
        //}
        //static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        //{
        //    while (source != null && source.GetType() != typeof(T))
        //        source = VisualTreeHelper.GetParent(source);
        //    return source;
        //}
    }
}
