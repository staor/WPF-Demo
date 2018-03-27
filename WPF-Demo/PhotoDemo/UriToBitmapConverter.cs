using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPF_Demo.PhotoDemo
{
    class UriToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri = value as Uri;
            BitmapImage bmp = new BitmapImage();
            bmp.DecodePixelHeight = 200; //确定解码高度，不同时设置宽度
            bmp.BeginInit(); //初始化开始
            bmp.CreateOptions = BitmapCreateOptions.DelayCreation; //延迟，必要时创建
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.UriSource = uri;
            bmp.EndInit(); //初始化结束
            BitmapFrame bf = BitmapFrame.Create(bmp.Clone());//克隆内存图像，当此方法完成后解绑IO资源bmp
            return bf;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
