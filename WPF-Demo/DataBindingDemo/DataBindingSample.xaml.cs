using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace WPF_Demo.DataBindingDemo
{
    /// <summary>
    /// DataBindingSample.xaml 的交互逻辑
    /// </summary>
    public partial class DataBindingSample : Page
    {
        StoreDb storeDb = null;
        bool HasChangedData = false; //记录DataTable源数据是否变动，由行、单元格编辑时触发判断
        public DataBindingSample()
        {
            InitializeComponent();
            storeDb = new StoreDb();
        }
        #region 路由命令
        //路由命令-
        public static RoutedCommand SaveSqlDataCommand = new RoutedCommand();
        //更新SqlData数据库执行方法
        private void SaveSqlDataCommand_Executed(object sender,ExecutedRoutedEventArgs e)
        {
            if (storeDb.SqlDataSet.Tables.Contains("Products"))
            {
                bool IsUpdate = storeDb.UpdateSqlData();
                if (IsUpdate)
                {
                    HasChangedData = false;
                    MessageBox.Show("更新到数据库成功!");
                }
            }
        }
        //命令源可否执行方法
        private void SaveSqlDataCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = HasChangedData;
        }
        #endregion

        //设置行细节展示
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            gridProducts.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
        }
        //取消行细节展示
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            gridProducts.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
        }
        //检查数据库连接情况及处理流程
        private void btConncetSQL_Click(object sender, RoutedEventArgs e)
        {
            if (storeDb.ConnectionSQLserver())
            {
                rbSqlData.IsEnabled = true;
            }
        }
        //更新DataSet到SqlServer
        private void btSaveSQL_Click(object sender, RoutedEventArgs e)
        {
            if (rbSqlData.IsChecked==true)
            {
                if (storeDb.HasChangedDataRow())
                {
                    bool IsUpdated = storeDb.UpdateSqlData();
                    if (IsUpdated)
                    {
                        MessageBox.Show("更新到数据库成功!");
                    }
                }
                else
                {
                    MessageBox.Show("数据未变动");
                }
            }
                
        }
        //设置分组样式
        private void cbGroupSytle_Checked(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(gridProducts.ItemsSource);
            if (view!=null)
            {
                view.GroupDescriptions.Add(new PropertyGroupDescription("ModelNumber"));
            }

        }
        //取消分组
        private void cbGroupSytle_UnChecked(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(gridProducts.ItemsSource);
            if (view != null)
            {
                view.GroupDescriptions.Clear();
            }
        }
        //添加新行，ID号自动+1
        private void btAddNew_Click(object sender, RoutedEventArgs e)
        {
            if (storeDb.SqlDataSet.Tables.Contains("Products"))
            {
                storeDb.AddNewSqlRow(); //在DataTable直接添加新行（自增ID）
                gridProducts.Focus();
                gridProducts.SelectedIndex = gridProducts.Items.Count - 1;//只要GridData是可编辑，则默认最后一行为新增空白
                gridProducts.ScrollIntoView(gridProducts.SelectedItem, gridProducts.Columns[1]);
                //gridProducts.CanUserAddRows = false;
            }            
        }
        //删除选定行
        private void btDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (gridProducts.SelectedItem!=null)
            {
                if (MessageBoxResult.Yes==MessageBox.Show("确定删除选定行？","提示！",MessageBoxButton.YesNo))
                {
                    List<DataRowView> listDR = new List<DataRowView>();
                    foreach (DataRowView item in gridProducts.SelectedItems)
                    {
                        DataRowView drv = item as DataRowView;
                        listDR.Add(drv);
                        
                    }
                    foreach (DataRowView item in listDR)
                    {
                        storeDb.SqlDataView.Table.Rows.Remove(item.Row);
                    }         
                }
            }
        }
        //加载XML数据源
        private void rbLoadXMLData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gridProducts.DataContext = null;
                gridProducts.ItemsSource = storeDb.GetProducts();
                categoryColumn.ItemsSource = storeDb.GetCategories();
                btAddNew.IsEnabled = false;
                btDeleteRow.IsEnabled = false;
                dateColumn.IsReadOnly = false;
                dateColumn.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                MessageBox.Show("无法加载XML数据");
                rbXmlData.IsChecked = false;
            }
            
        }
        //加载SqlServer数据源
        private void rbLoadSqlData_Click(object sender, RoutedEventArgs e)
        {            
            gridProducts.ItemsSource = null;
            gridProducts.ItemsSource = storeDb.SqlDataView;
            categoryColumn.ItemsSource = storeDb.GetCategories();
            btAddNew.IsEnabled = true;
            btDeleteRow.IsEnabled = true;
            dateColumn.IsReadOnly = true;
            dateColumn.Visibility = Visibility.Hidden;
        }
        //页加载
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            
            btAddNew.IsEnabled = false;
            btDeleteRow.IsEnabled = false;
        }
        //DataGridRow左键事件,一次点击对应元素，直接作用对应元素点击效果
        private void dataGridRow_PreviewMouseLeftButtonDown(object sender,MouseButtonEventArgs e)
        {
            //CheckBox cb = VisualUpwardSearch<CheckBox>(e.OriginalSource as DependencyObject) as CheckBox;
            //if (cb!=null)
            //{
            //    cb.IsChecked = (bool)cb.IsChecked? true:false;
            //}
            //ComboBox comb = VisualUpwardSearch<ComboBox>(e.OriginalSource as DependencyObject) as ComboBox;
            //if (comb != null)
            //{
            //    comb.IsDropDownOpen = comb.IsDropDownOpen ?true: false;
            //}

        }
        //寻找VisualTree指定子类类型        
        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }
        //检查单元格编辑情况，最后判断源DataRow是否有变动
        private void gridProducts_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //HasChangedData = storeDb.HasChangedDataRow();
        }
        //检查行编辑情况，最后判断源DataRow是否有变动
        private void gridProducts_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //HasChangedData = storeDb.HasChangedDataRow();
        }

    }
}
