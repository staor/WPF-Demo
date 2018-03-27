using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Demo.PhotoDemo
{
    public class PhotoList: ObservableCollection<Photo>
    {
        private DirectoryInfo _directory;

        public PhotoList()
        {
            
        }
        public String DirectoryStr
        {
            
            set
            {
                if (Directory.Exists(value))
                {
                    _directory = new DirectoryInfo(value);
                    Update();
                }            
                
            }
            get { return _directory.FullName; }
        }
        public PhotoList(string directoryStr)
        {
            _directory =new DirectoryInfo( directoryStr);
        }
        public void LoadImagePath(string path)
        {
            _directory = new DirectoryInfo(path);
            Update();
        }

        //更新目录下文件
        private void Update()
        {
            if (_directory==null)
            {
                return;
            }
            Clear();
            foreach (var item in _directory.EnumerateFiles("*.jpg",SearchOption.TopDirectoryOnly))
            {                
                Add(new Photo(item.FullName));
            }
        }
    }
}
