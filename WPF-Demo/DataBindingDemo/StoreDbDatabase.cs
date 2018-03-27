using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Demo.DataBindingDemo
{
    class StoreDbDatabase
    {
        public  DataTable GetProducts()
        {
            return ReadDataSet().Tables[0];
        }
        public DataSet GetCategoriesAndProducts()
        {
            return ReadDataSet();
        }
        internal static DataSet ReadDataSet()
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXmlSchema("store.xsd");
                ds.ReadXml("store.xml");
            }
            catch (Exception)
            {
                MessageBox.Show("无法读取有效的XML文件!");
            }            
            return ds;
        }
        
    }
}
