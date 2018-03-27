using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Demo.DataBindingDemo
{
    public class Product : INotifyPropertyChanged
    {
        private int _productID;
        public int ProductID
        {
            get { return _productID; }
            set
            {
                _productID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProductID"));
            }
        }

        private string _modelNumber;
        public string ModelNumber
        {
            get { return _modelNumber; }
            set
            {
                _modelNumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelNumber"));
            }
        }

        private string _modelName;
        public string ModelName
        {
            get { return _modelName; }
            set
            {
                _modelName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelName"));
            }
        }

        private decimal _unitCost;
        public decimal UnitCost
        {
            get { return _unitCost; }
            set
            {
                _unitCost = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UnitCost"));
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }
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
        private string _productImagePath;
        public string ProductImagePath
        {
            get { return _productImagePath; }
            set
            {
                _productImagePath = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProductImagePath"));
            }
        }

        public Product(int productID, string modelNumber,string modelName,decimal unitCost,string description)
        {
            _productID = productID;
            _modelNumber = modelNumber;
            _modelName = modelName;
            _unitCost = unitCost;
            _description = description;
        }
        public Product(int productID, string modelNumber,string modelName,decimal unitCost,string description,string productImagePath)
            : this(productID,modelNumber, modelName, unitCost, description)
        {
            _productImagePath = productImagePath;
        }
        public Product(int productID, string modelNumber, string modelName, decimal unitCost, string description, int categoryID, string categoryName, string productImagePath)
            : this(productID, modelNumber, modelName, unitCost, description)
        {
            _categoryName = categoryName;
            _productImagePath = productImagePath;
            _categoryID = categoryID;
        }
        public override string ToString()
        {
            return _modelName+" (" + _modelName + ")";
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //临时存储测试用,并未存储在数据库
        private DateTime _dateAdded = DateTime.Today;
        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set
            {
                _dateAdded = value;
            }
        }
    }
}
