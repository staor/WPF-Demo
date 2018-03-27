using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Demo.DataBindingDemo;

namespace WPF_Demo.DataBindingDemo
{
    class StoreDb
    {
        #region StoreDb字段、初始化、属性
        private bool IsCreateTable = false; //确定数据库、表是否正常运转
        private DataSet _xmlDataSet=null;   //存储XML数据
        private DataSet _sqlDataSet = null; //存储SQL数据
        private DataView _sqlDataView = null;
        private SqlDataAdapter _sqlDataAdapter = null;//连接数据库更新数据的Adapter
        string sqlStr = "";                 //存取数据SQL字符串
        public StoreDb()
        {
        }       
        public DataSet SqlDataSet
        {
            get
            {
                if (_sqlDataSet==null)
                {
                    _sqlDataSet = new DataSet();
                    sqlStr =
    "select ProductID,ModelNumber,ModelName,UnitCost,ProductImage,CategoryID,Description from Products";
                    _sqlDataAdapter = SqlDataHelper.GetAdapter(sqlStr);
                    SqlCommandBuilder cb = new SqlCommandBuilder(_sqlDataAdapter);//很重要，自动生成命令
                    _sqlDataAdapter.Fill(_sqlDataSet, "Products");
                    DataTable dt = _sqlDataSet.Tables["Products"];
                    dt.PrimaryKey = new DataColumn[] { dt.Columns["ProductID"] };//需设置主键，dt.Update才能更新

                    //临时获取Categories数据（从数据库里）
                    string sqlStr2 ="select CategoryID,CategoryName from Categories";
                    SqlDataAdapter _sqlDataAdapter2 = SqlDataHelper.GetAdapter(sqlStr2);
                    _sqlDataAdapter2.Fill(_sqlDataSet, "Categories");
                }
                return _sqlDataSet;
            }            
        }
        public DataSet XMLDataSet
        {
            get
            {
                if (_xmlDataSet==null)
                {
                    try
                    {
                        _xmlDataSet = StoreDbDatabase.ReadDataSet();
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("无法加载有效XML数据集");
                    }
                    
                }
                return _xmlDataSet;
            }
        }
        public DataView SqlDataView
        {
            get
            {
                if (_sqlDataView == null)
                {
                    _sqlDataView =new DataView(SqlDataSet.Tables["Products"]); //DataTable.DefaultView不是线程安全
                }
                return _sqlDataView;
            }
        }
        public ObservableCollection<Category> a;
        #endregion

        #region XML数据操作
        public Product GetProduct(int ID)
        {
            DataRow productRow = XMLDataSet.Tables["Products"].Select("ProductID= " + ID.ToString())[0];
            Product product = new Product((int)productRow["ProductID"], (string)productRow["ModelNumber"], (string)productRow["ModelName"],
                (decimal)productRow["UnitCost"], (string)productRow["Description"], (int)productRow["CategoryID"],
                (string)productRow["CategoryName"], (string)productRow["ProductImage"]);
            return product;
        }
        public ICollection<Product> GetProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            foreach (DataRow dr in XMLDataSet.Tables["Products"].Rows)
            {
                products.Add(new Product((int)dr["ProductID"],(string)dr["ModelNumber"], (string)dr["ModelName"], (decimal)dr["UnitCost"],
                    (string)dr["Description"], (int)dr["CategoryID"], (string)dr["CategoryName"], (string)dr["ProductImage"]));
            }
            return products;
        }
        //插入SqlData新数据行
        
        public ICollection<Product> GetProductsSlow()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            return GetProducts();
        }

        public ICollection<Category> GetCategories()
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            if (XMLDataSet!=null&&XMLDataSet.Tables.Contains("Categories"))
            {
                foreach (DataRow dr in XMLDataSet.Tables["Categories"].Rows)
                {
                    categories.Add(new Category(dr["CategoryName"].ToString(), (int)dr["CategoryID"]));
                }
            }
            else if (SqlDataSet!=null && SqlDataSet.Tables.Contains("Categories"))
            {
                foreach (DataRow dr in SqlDataSet.Tables["Categories"].Rows)
                {
                    categories.Add(new Category(dr["CategoryName"].ToString(), (int)dr["CategoryID"]));
                }
            }
            else
            {
                MessageBox.Show("没有Categories数据");
            }
            return categories;
        }

        public ICollection<Category> GetCategoriesAndProducts()
        {
            DataRelation relationCategoryProduct = XMLDataSet.Relations[0];

            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            foreach (DataRow categoryRow in XMLDataSet.Tables["Categores"].Rows)
            {
                ObservableCollection<Product> products = new ObservableCollection<Product>();
                foreach (DataRow productRow in categoryRow.GetChildRows(relationCategoryProduct))
                {
                    products.Add(new Product((int)productRow["ProductID"],productRow["ModelNumber"].ToString(), productRow["ModelName"].ToString(),
                        (decimal)productRow["UnitCost"], productRow["Description"].ToString()));
                }
                categories.Add(new Category(categoryRow["CategoryName"].ToString(), products));
            }
            return categories;
        }

        public ICollection<Product> GetProductsFilteredWithLinq(decimal minimumCost)
        {
            ICollection<Product> products = GetProducts();
            IEnumerable<Product> matches = from product in products
                                           where product.UnitCost >= minimumCost
                                           select product;
            return new ObservableCollection<Product>(matches.ToList());
        }
        #endregion

        #region SqlData操作
        //新增DataTable的新行DataRow
        public void AddNewSqlRow()
        {
            DataTable dt = SqlDataSet.Tables["Products"];
            int lastProductID = (int)dt.Rows[dt.Rows.Count - 1]["ProductID"];
            DataRow newRow = dt.NewRow();
            newRow["ProductID"] = lastProductID + 1;
            dt.Rows.Add(newRow);
        }

        //创建数据表Products、Categories
        public void CreateTable()
        {
            string sqlCreateProducts = @"Create table Products(
                ProductID int not null PRIMARY KEY,                
                ModelNumber nvarchar(50),
                ModelName nvarchar(50),
                ProductImage nvarchar(200),
                UnitCost decimal,
                Description nvarchar(Max),
                CategoryID int foreign key references Categories(CategoryID)
                )";
            string sqlcreateCategories = @"Create table Categories(
                CategoryID int not null  PRIMARY KEY,
                CategoryName nvarchar(50) not null unique
                )";
            
            try
            {
                SqlDataHelper.ExecuteSql(sqlcreateCategories);
                SqlDataHelper.ExecuteSql(sqlCreateProducts);
                MessageBox.Show("创建Products、Categories数据表成功！");
                IsCreateTable = true; //创建成功标识
            }
            catch (Exception)
            {
                MessageBox.Show("创建Products、Categories数据表失败！");                
            }
            
        }

        //插入数据表内容,大批量数据使用SqlBulk插入
        public void SqlBulKData()
        {
            DataTable categoryDT = XMLDataSet.Tables["Categories"];
            DataTable productDT = XMLDataSet.Tables["Products"];
            string[] cParas = new string[] { "CategoryID", "CategoryName" };  
            //传输源表列对应目标列名称匹配
            string[] parameters = new string[] {
                "ProductID", "ModelNumber", "ModelName", "ProductImage", "UnitCost", "Description", "CategoryID1" };
            string[] paraValues = new string[] {
                "ProductID", "ModelNumber", "ModelName", "ProductImage", "UnitCost", "Description", "CategoryID" };
            //DataTable productDT = XMLDataSet.Tables["Products"].DefaultView.ToTable(false,
            //    new string[] { "ProductID","ModelNumber","ModelName",
            //        "ProductImage","UnitCost","Description" ,"CategoryID1"});
            //DataTable productDT = XMLDataSet.Tables["Products"];
            try
            {
                SqlDataHelper.BulkInertData("Categories", categoryDT, cParas, cParas);
                SqlDataHelper.BulkInertData("Products", productDT, parameters, paraValues);
            }
            catch (Exception)
            {
                MessageBox.Show("批量插入数据出错，请检查源及目标数据表的对应列名！");
                return ;
            }
            
        }
        //更新DataSet数据到SqlServer中
        public bool UpdateSqlData()
        {
            bool IsUpdate = false;
            try
            {
                _sqlDataAdapter.Update(SqlDataSet,"Products"); //更新DataTable到数据库
                _sqlDataSet.Tables["Products"].AcceptChanges(); //还原DataRow状态
                IsUpdate = true;
            }
            catch (Exception)
            {
                MessageBox.Show("SqlDataAdapter更新数据库失败。");
            }
            return IsUpdate;
        }
        //数据表中行是否有更改
        public bool HasChangedDataRow()
        {
            bool haRowChanged = false;
            DataTable dt = SqlDataSet.Tables["Products"];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState!=DataRowState.Unchanged)
                {
                    haRowChanged = true;
                    break;
                }
            }
            return haRowChanged;
        }

        //检查数据库连接情况并流程处理
        public bool ConnectionSQLserver()
        {
            bool HasSqlData = false;  //是否数据库存有数据
            if (!SqlDataHelper.IsConncetedDataBase())
            {
                MessageBox.Show("数据库连接未成功，请检查是否创建myProduct数据库！",
                    "提示：", MessageBoxButton.OK);
            }
            int count1 = SqlDataHelper.IsConnectedTable("Products");
            int count2 = SqlDataHelper.IsConnectedTable("Categories");
            if (count1 == -1 | count2 == -1)
            {
                MessageBoxResult result = MessageBox.Show("数据表未创建，是否创建数据表？",
                    "提示：", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    CreateTable();
                    if (IsCreateTable)
                    {
                        count1 = 0;  //数据表创建成功，记录数据为0
                    }                    
                }
            }
            if (count1 == 0)
            {
                MessageBoxResult result = MessageBox.Show("数据表未有数据，是否导入XML数据？",
                    "提示：", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    SqlBulKData();
                    count1 = SqlDataHelper.IsConnectedTable("Products");
                }
            }
            if (count1>0)
            {
                HasSqlData = true;
                MessageBox.Show("SqlServer数据库的记录条数：" + count1);
            }
            return HasSqlData;
        }
        #endregion
    }
}
