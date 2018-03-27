using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPF_Demo.DataBindingDemo
{
    class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapSource bs=null;
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), (string)value);
            if (File.Exists(imagePath))
            {
                BitmapImage bi = new BitmapImage();
                bi.DecodePixelHeight = 50;
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.DelayCreation;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = new Uri(imagePath);
                bi.EndInit();
                bs = bi.Clone();
            }
            return bs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
