using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WPF_Demo.PhotoDemo;

namespace WPF_Demo.PhotoDemo
{
    /// <summary>
    /// PhotoView.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoView : Window
    {
        public PhotoSample parentPage { get; set; }
        public Uri photoUri { get; set; }  //存储图片Uri
        private BitmapSource bitmapSource; //临时存储图片
        private bool IsBlackAndWhite;      //是否有灰色效果
        private int intAngle;              //记录旋转角度
        private bool mouseDown;
        private Point mouseXY;
        private double min = 0.1, max = 3.0;//最小、最大缩放倍数

        public PhotoView()
        {
            InitializeComponent();
        }

        private void Crop_Click(object sender, RoutedEventArgs e)
        {

        }

        //旋转图片，并记录旋转角度
        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            intAngle += 90;
            if (intAngle == 360)
            {
                intAngle = 0; 
            }
            BitmapSource bs = imageView.Source as BitmapSource;
            CachedBitmap cache = new CachedBitmap(bs, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            imageView.Source = BitmapFrame.Create(new TransformedBitmap(bs, new RotateTransform(90)));
            
        }

        //黑白效果及恢复，并旋转对应角度
        private void BlackAndWhite_Click(object sender, RoutedEventArgs e)
        {

            if (!IsBlackAndWhite)
            {
                BitmapSource bs = imageView.Source as BitmapSource;
                imageView.Source= BitmapFrame.Create(new FormatConvertedBitmap(bs, PixelFormats.Gray8, BitmapPalettes. Gray256, 1.0));
                IsBlackAndWhite = true;
            }
            else
            {
                imageView.Source = BitmapFrame.Create(new TransformedBitmap(bitmapSource, new RotateTransform(intAngle)));
                IsBlackAndWhite = false;

            }
        }

        //获取原始图片
        private void Defualt_Click(object sender, RoutedEventArgs e)
        {
            ckBlackWhite.IsChecked = false;
            imageView.Source = bitmapSource;

            //回归原变换值
            var group = imageView.FindResource("TfGroup") as TransformGroup;
            var transform = group.Children[0] as ScaleTransform;
            transform.ScaleX = 1;
            transform.ScaleY = 1;
            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = 0;
            transform1.Y = 0;
        }

        //保存图片到原位置
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Resualt = MessageBox.Show("确定保存修改？","提示！", MessageBoxButton.YesNo);
            if (Resualt==MessageBoxResult.Yes)
            {
                bitmapSource = null;
                bitmapSource = imageView.Source as BitmapSource;
                JpegBitmapEncoder jbe = new JpegBitmapEncoder();
                jbe.Frames.Add(BitmapFrame.Create(bitmapSource));
                FileStream fs = new FileStream(photoUri.OriginalString, FileMode.Create,FileAccess.ReadWrite);
                jbe.Save(fs);                  //图片保存
                fs.Close();
                MessageBox.Show("保存成功");
                //parentPage.UpdateCollection(photoUri); //某种概率IO错误，在本窗口保存及父页更新集合同时处理时偶尔触发  
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        //窗口加载图片
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string uri = photoUri.OriginalString;
            if (File.Exists(uri))
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = photoUri;
                bi.EndInit();
                //BitmapFrame bf = BitmapFrame.Create(photoUri);
                bitmapSource = bi.Clone();
                //bi = null;  //必须设为null，释放IO资源，否则保存时报错
                imageView.Source = bitmapSource;
                tbFullName.Text = uri;
                lablelName.Content = uri.Substring(uri.LastIndexOf(@"\") + 1);
            }

            setViewSize();
        }

        private void ContentControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img==null)
            {
                return;
            }
            img.CaptureMouse();
            mouseDown = true;
            mouseXY = e.GetPosition(img);  //记录鼠标按下坐标
        }

        private void ContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img==null)
            {
                return;
            }
            img.ReleaseMouseCapture();
            mouseDown = false;
        }

        private void ContentControl_MouseMove(object sender, MouseEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            if (mouseDown)               //判断鼠标是否左键按下
            {
                Domousemove(img, e);     //否则一直执行移动方法
            }
            
        }

        private void ContentControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var img = sender as ContentControl;
            if (img==null)
            {
                return;
            }
            var point = e.GetPosition(img);
            var group = imageView.FindResource("TfGroup") as TransformGroup;
            var delta = e.Delta * 0.001;
            DowheelZoom(group, point, delta);
        }
        private void Domousemove(ContentControl img,MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)  //判断左键是否按下
            {
                return;
            }
            var group = imageView.FindResource("TfGroup") as TransformGroup; //获取图片空间的转换集合
            var transform = group.Children[1] as TranslateTransform;
            var position = e.GetPosition(img);
            transform.X -= mouseXY.X - position.X;   // 平移 鼠标按下点至移动点的距离
            transform.Y -= mouseXY.Y - position.Y;
            mouseXY = position;
        }
        private void DowheelZoom(TransformGroup group,Point point,double delta)
        {
            var pointToContent = group.Inverse.Transform(point);
            var transform = group.Children[0] as ScaleTransform;
            if (transform.ScaleX+delta<min|transform.ScaleX+delta>max)
            {
                return;
            }
            transform.ScaleX += delta;
            transform.ScaleY += delta;
            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = -1 * ((pointToContent.X * transform.ScaleX) - point.X);  //缩放后平移
            transform1.Y = -1 * ((pointToContent.Y * transform.ScaleY) - point.Y);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            setViewSize();      //解决Scroll尺寸改变后图像活动区域不一致问题
        }

        //重新设置滚动条控件新尺寸
        private void setViewSize()
        {
            mainScrollv.Width = this.ActualWidth;
            mainScrollv.Height = this.ActualHeight - 50;
        }
    }    
}
