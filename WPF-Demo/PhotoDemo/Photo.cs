using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPF_Demo.PhotoDemo
{
    public class Photo
    {
        public string Path { get; }
        //public Uri Uri { get; }
        public BitmapFrame Image { get; set; }
        public Photo(string path)
        {
            Path = path;
            //Uri = new Uri(path);
            UriToBitmapFrame();
        }
        public override string ToString() => Path.Substring(Path.LastIndexOf(@"\")+1);
        private void UriToBitmapFrame()
        {
            
            BitmapImage bmp = new BitmapImage();
            bmp.DecodePixelHeight = 200; //确定解码高度，不同时设置宽度
            bmp.BeginInit(); //初始化开始
            bmp.CreateOptions = BitmapCreateOptions.DelayCreation; //延迟，必要时创建
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.UriSource = new Uri(Path);
            bmp.EndInit(); //初始化结束
            Image = BitmapFrame.Create(bmp.Clone()); //克隆内存中图像，方法结束自动释放IO资源。
            bmp = null;
        }
    }
}
