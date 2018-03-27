using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Demo.DataBindingDemo
{
    public class Category : INotifyPropertyChanged
    {
        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CategoryName"));
            }
        }

        // For DataGridComboBoxColumn example.
        private int _categoryID;
        public int CategoryID
        {
            get { return _categoryID; }
            set
            {
                _categoryID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CategoryID"));
            }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Products"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            
                PropertyChanged?.Invoke(this, e);
        }

        public Category(string categoryName, ObservableCollection<Product> products)
        {
            _categoryName = categoryName;
            _products = products;
        }

        public Category(string categoryName, int categoryID)
        {
            _categoryName = categoryName;
            _categoryID = categoryID;
        }

    }
}
