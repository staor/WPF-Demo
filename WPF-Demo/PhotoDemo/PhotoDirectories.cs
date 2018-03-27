using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Demo.PhotoDemo
{
    class PhotoDirectories:ObservableCollection<DirectoryInfo>
    {
        private string _path;
        private List<string> _listPath;
        public PhotoDirectories()
        {
            if (_listPath==null)
            {
                _listPath = new List<string>();
            }
        }
        public void AddDirectory(string path)
        { 
            if (Directory.Exists(path))
            {
                _path = path;
                if (_listPath.Contains(_path))
                {
                    return;
                }
                _listPath.Add(_path);
                Add(new DirectoryInfo(path));
            }            
        }
        public void RemoveDirectory(string path)
        {
            _listPath.Remove(path);
            //RemoveA(path)
        }
    }
}
