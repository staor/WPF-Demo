using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Demo.DataBindingDemo
{
    class SqlDataHelper
    {
        //定义连接字符串和链接对象
        private static string _conStr = "server=.;database=myProduct;integrated security=SSPI;";
        private static SqlConnection _con = null;

        public static string ConStr
        {
            get { return _conStr; }
            set { _conStr = value; }
        }
        private static SqlConnection Con
        {
            get
            {
                if (_con==null)
                {
                    _con = new SqlConnection(_conStr);
                }
                return _con;
            }
        }

        //判断是否能连接到数据库
        public static bool IsConncetedDataBase()
        {
            bool IsConnceted = false;
            try
            {
                Con.Open();
                IsConnceted = true;
            }
            catch (Exception)
            {
                return IsConnceted;
            }
            finally
            {
                Con.Close();                
            }
            return IsConnceted;
        }
        //判断是否连接到表
        public static int IsConnectedTable(string tableName)
        {
            int count = -1;
            string sqlstr = "select count(*) from "+tableName;
            SqlCommand com = new SqlCommand(sqlstr, Con);

            try
            {
                Con.Open();
                //MessageBox.Show("已连接SQL-server");
                 count= (int)com.ExecuteScalar();
                //MessageBox.Show("成功读取已有( {0} )条记录", count.ToString());

            }
            catch (Exception)
            {
                //MessageBox.Show($"未能连接数据表{tableName},请检查");
            }
            finally
            {
                Con.Close();
            }
            return count; //如查询表记录有异常，再次发生异常数-1;
        }

        //传入Select语句，获得返回SqlDataAdapter，属于连接方式
        public static SqlDataAdapter GetAdapter(string sqlstr)
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(sqlstr,Con);
                return da;
            }
            catch (DataException)
            {
                return null;
            }
            finally
            {
                Con.Close();
            }
        }//传入Select语句，获得返回数据，属于连接方式
        public static SqlDataReader GetReader(string sqlstr)
        {
            SqlCommand cmd = new SqlCommand(sqlstr, Con);
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (DataException)
            {
                return null;

            }
            finally
            {
                cmd.Dispose();
            }
        }
        //传入参数及存储过程，获得返回SqlDataReader数据，属于连接方式
        public static SqlDataReader GetProceReader(String sqlstr)
        {
            SqlCommand cmd = new SqlCommand(sqlstr, Con);
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (DataException)
            {
                return null;

            }
            finally
            {
                cmd.Dispose();
            }
        }

        //传入select语句，获得返回数据集，属于断开方式
        public static DataSet GetDataset(string sqlstr, string name)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter myAdapter = new SqlDataAdapter(sqlstr, Con);
                //DataSet ds = new DataSet();
                myAdapter.Fill(ds, name);
                return ds;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
        }

        //传入DML语句,进行数据的添加，删除和修改，包括调用无范围数据集的存储过程
        public static int ExecuteSql(string sqlstr)
        {
            SqlCommand cmd = new SqlCommand(sqlstr, Con);
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                int r = cmd.ExecuteNonQuery();
                Con.Close();
                return r;
            }
            catch (Exception e)
            {
                //产生操作异常的处理
                throw new Exception(e.Message);

            }
        }
        //传入DML语句及参数集合,进行数据的添加，删除和修改，包括调用无范围数据集的存储过程
        public static int ExecuteSql(string sqlstr, string[] paramnames, string[] paramvalues)
        {
            SqlCommand cmd = new SqlCommand(sqlstr, Con);
            for (int i = 0; i < paramnames.Length; i++)
            {
                SqlParameter sp = new SqlParameter(paramnames[i], paramvalues[i]);
                cmd.Parameters.Add(sp);
            }
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                int r = cmd.ExecuteNonQuery();
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
            }
        }

        //传入一组DML语句，执行事务
        public static int ExecuteSqltrans(string[] sqlstrs)
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            SqlTransaction ts = Con.BeginTransaction();//设置事务，要做全做，要不都不做
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.Transaction = ts;
            try
            {
                //执行每条sql语句
                for (int i = 0; i < sqlstrs.Length; i++)
                {
                    cmd.CommandText = sqlstrs[i];
                    cmd.ExecuteNonQuery();
                }
                ts.Commit();//全部语句都执行成功，事物提交
                return 1;
            }
            catch (Exception)
            {
                ts.Rollback(); //中间有语句执行异常，事物回滚
                return 0;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
            }
        }

        //批量数据使用SqlBulk插入
        public static void BulkInertData(string targetDT, DataTable sourceDT, string[] parameters,string[] paraValues)
        {

            try
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(Con);
                bulkCopy.DestinationTableName = targetDT;
                bulkCopy.BatchSize = sourceDT.Rows.Count;
                if (parameters!=null&&paraValues!=null&&parameters.Length==paraValues.Length)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        bulkCopy.ColumnMappings.Add(parameters[i], paraValues[i]);
                    }
                }                       
                Con.Open();

                if (sourceDT != null && sourceDT.Rows.Count != 0)
                {
                    bulkCopy.WriteToServer(sourceDT);
                    bulkCopy.Close(); //关闭实例，否则偶尔出异常
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Con.Close();                
            }

        }
    }
}
