
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Linq;
using System.Text;

namespace 彩牛通
{
    public class SqlHelper
    {

        private static SqlConnection connection;
        /// <summary>
        /// 获取连接数据库对象
        /// </summary>
        public static SqlConnection GetConnection
        {
            get
            {
                string strConn = "server=202.119.168.66;database=CNTDataBase;user=sa;pwd=admin1341156974.";
                if (connection == null)
                {
                    connection = new SqlConnection(strConn);
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Closed)
                {
                    connection = new SqlConnection(strConn);
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }
        //创建SqlCommand对象 sql语句不带参数
        public static SqlCommand Command(string strSql)
        {
            using (SqlCommand cmd = new SqlCommand(strSql, GetConnection))
            {
                return cmd;
            }
        }
        //创建SqlCommand对象 sql语句带参数
        public static SqlCommand Command(string strSql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(strSql, GetConnection);
            if (values != null) cmd.Parameters.AddRange(values);
            return cmd;
        }
        //返回前向只读的结果集对象 sql语句不带参数(查询)
        public static SqlDataReader ExecuteReader(string strSql)
        {
            SqlCommand cmd = Command(strSql);
            return cmd.ExecuteReader();
        }
        //返回前向只读的结果集对象 sql语句带参数(查询)
        public static SqlDataReader ExecuteReader(string strSql, params SqlParameter[] values)
        {
            SqlCommand cmd = Command(strSql, values);
            return cmd.ExecuteReader();
        }
        //返回第一行第一列的值 sql语句不带参数(查询)
        public static object ExecuteScalar(string strSql)
        {
            SqlCommand cmd = Command(strSql);
            return cmd.ExecuteScalar();
        }
        //返回第一行第一列的值 sql语句带参数(查询)
        public static object ExecuteScalar(string strSql, params SqlParameter[] values)
        {
            SqlCommand cmd = Command(strSql, values);
            return cmd.ExecuteScalar();
        }
        //返回受影响的行数 sql语句不带参数(更新 添加 删除)
        public static int ExecuteNonQuery(string strSql)
        {
            SqlCommand cmd = Command(strSql);
            return cmd.ExecuteNonQuery();
        }
        //返回受影响的行数 sql语句不带参数(更新 添加 删除)
        public static int ExecuteNonQuery(string strSql, params SqlParameter[] values)
        {
            SqlCommand cmd = Command(strSql, values);
            return cmd.ExecuteNonQuery();
        }
        //断开连接方式的结果集 sql语句不带参数
        public static DataTable DataTable(string strSql)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(strSql, GetConnection);
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        //断开连接方式的结果集 sql语句带参数
        public static DataTable DataTable(string strSql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(strSql, GetConnection);
            adapter.SelectCommand.Parameters.AddRange(values);
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        ///使用存储过程的获得SQLCommand对象
        ///ProcName存储过程的名称
        public static SqlCommand ProcCommad(string ProcName)
        {
            SqlCommand comm = new SqlCommand(ProcName, GetConnection);
            comm.CommandType = CommandType.StoredProcedure;
            return comm;
        }
        ///使用带参数存储过程的获得SQLCommand对象
        ///ProcName存储过程的名称
        public static SqlCommand ProcCommad(string ProcName, SqlParameter[] param)
        {
            SqlCommand comm = new SqlCommand(ProcName, GetConnection);
            comm.Parameters.AddRange(param);
            comm.CommandType = CommandType.StoredProcedure;
            return comm;
        }
        //存储过程返回一个前向只读的结果集
        public static SqlDataReader ProExecuteReader(string ProName)
        {
            SqlCommand comd = ProcCommad(ProName);
            return comd.ExecuteReader();
        }
        //带参数存储过程返回一个前向只读的结果集
        public static SqlDataReader ProExecuteReader(string ProName, SqlParameter[] param)
        {
            SqlCommand comd = ProcCommad(ProName, param);
            return comd.ExecuteReader();
        }
        //不带参数的存储过程的实现受影响行数
        public static int ProExecuteNonQuery(string ProName)
        {
            SqlCommand comd = ProcCommad(ProName);
            return comd.ExecuteNonQuery();
        }
        ////带参数的存储过程的实现受影响行数
        public static int ProExecuteNonQuery(string ProName, SqlParameter[] param)
        {
            SqlCommand comd = ProcCommad(ProName, param);
            return comd.ExecuteNonQuery();
        }
        ///使用带参数获得DataSet对象
        public static DataSet DateSet(string strSql, SqlParameter[] param)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(strSql, GetConnection);
            adapter.SelectCommand.Parameters.AddRange(param);
            adapter.Fill(ds);
            return ds;
        }
        ///使用带参数获得DataSet对象
        public static DataSet DateSet(string strSql)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(strSql, GetConnection);
            adapter.Fill(ds);
            return ds;
        }


        /// <summary>
        /// 返回一个DataSet数据集
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据集</returns>
        public static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] commandParameters)
        {
            //创建一个SqlCommand对象，并对其进行初始化
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = GetConnection;
            PrepareCommand(cmd, GetConnection, null, cmdText, commandParameters);
            //创建SqlDataAdapter对象以及DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                //填充ds
                da.Fill(ds);
                // 清除cmd的参数集合 
                cmd.Parameters.Clear();
                //返回ds
                return ds;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }

        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">Sql连接</param>
        /// <param name="trans">Sql事务</param>
        /// <param name="cmdText">命令文本,例如：Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            //判断连接的状态。如果是关闭状态，则打开
            if (conn.State != ConnectionState.Open)
                conn.Open();
            //cmd属性赋值
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //是否需要用到事务处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            //添加cmd需要的存储过程参数
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


        /// <summary>
        /// ACCESS高效分页
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页容量</param>
        /// <param name="strKey">主键</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="queryString">查询字符串，支持联合查询</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的DataTable</returns>
        public static DataTable ExecutePager(int pageIndex, int pageSize, string strKey, string showString, string queryString, string whereString, string orderString, out int pageCount, out int recordCount)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = strKey + " asc ";
            SqlConnection m_Conn = GetConnection;

            try
            {
                m_Conn.Open();
            }
            catch { }
            // string myVw = string.Format(" ( {0} ) tempVw ", queryString);
            string myVw = string.Format("{0}", queryString);
            SqlCommand cmdCount = new SqlCommand(string.Format(" select count(*) as recordCount from {0} {1}", myVw, whereString), m_Conn);

            recordCount = Convert.ToInt32(cmdCount.ExecuteScalar());

            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            SqlCommand cmdRecord;
            if (pageIndex == 1)//第一页
            {
                cmdRecord = new SqlCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, whereString, orderString), m_Conn);
            }
            else if (pageIndex > pageCount)//超出总页数
            {
                cmdRecord = new SqlCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString), m_Conn);
            }
            else
            {
                int pageLowerBound = pageSize * pageIndex;
                int pageUpperBound = pageLowerBound - pageSize;
                string recordIDs = recordID(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, strKey, myVw, whereString, orderString), pageUpperBound);
                string queryStringend = string.Format("select {0} from {1} where {2} in ({3}) order by {4} ", showString, myVw, strKey, recordIDs, orderString);
                cmdRecord = new SqlCommand(queryStringend, m_Conn);

            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdRecord);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            m_Conn.Close();
            m_Conn.Dispose();
            return dt;

        }



        /// <summary>
        /// ACCESS高效分页：当表的主键是字符串类型时候
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页容量</param>
        /// <param name="strKey">主键</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="queryString">查询字符串，支持联合查询</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的DataTable</returns>
        public static DataTable ExecutePagerWhenPrimaryIsString(int pageIndex, int pageSize, string strKey, string showString, string queryString, string whereString, string orderString, out int pageCount, out int recordCount)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = strKey + " asc ";
            SqlConnection m_Conn = GetConnection;

            try
            {
                m_Conn.Open();
            }
            catch { }
            string myVw = string.Format(" ( {0} ) tempVw ", queryString);
            SqlCommand cmdCount = new SqlCommand(string.Format(" select count(*) as recordCount from {0} {1}", myVw, whereString), m_Conn);

            recordCount = Convert.ToInt32(cmdCount.ExecuteScalar());

            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            SqlCommand cmdRecord;
            if (pageIndex == 1)//第一页
            {
                string sql = string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, whereString, orderString);
                cmdRecord = new SqlCommand(sql, m_Conn);
            }
            else if (pageIndex > pageCount)//超出总页数
            {
                string sql = string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString);
                cmdRecord = new SqlCommand(sql, m_Conn);
            }
            else
            {
                int pageLowerBound = pageSize * pageIndex;
                int pageUpperBound = pageLowerBound - pageSize;
                string recordIDs = recordIDString(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, strKey, myVw, whereString, orderString), pageUpperBound);
                string queryStringend = string.Format("select {0} from {1} where {2} in ({3}) order by {4} ", showString, myVw, strKey, recordIDs, orderString);
                cmdRecord = new SqlCommand(queryStringend, m_Conn);

            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdRecord);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            m_Conn.Close();
            m_Conn.Dispose();
            return dt;

        }

        /// <summary>
        /// 分页使用
        /// </summary>
        /// <param name="query"></param>
        /// <param name="passCount"></param>
        /// <returns></returns>
        private static string recordID(string query, int passCount)
        {
            SqlConnection m_Conn = GetConnection;

            try
            {
                m_Conn.Open();
            }
            catch { }
            SqlCommand cmd = new SqlCommand(query, m_Conn);
            string result = string.Empty;
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (passCount < 1)
                    {
                        try
                        {
                            result += "," + dr.GetInt32(0);
                        }
                        catch (Exception)
                        {

                            result += "," + dr.GetString(0);
                        }

                    }
                    passCount--;
                }
            }
            // m_Conn.Close();
            // m_Conn.Dispose();
            return result.Substring(1);

        }

        /// <summary>
        /// 分页使用:主键是字符串类型时候
        /// </summary>
        /// <param name="query"></param>
        /// <param name="passCount"></param>
        /// <returns></returns>
        private static string recordIDString(string query, int passCount)
        {
            SqlConnection m_Conn = GetConnection;

            try
            {
                m_Conn.Open();
            }
            catch { }
            SqlCommand cmd = new SqlCommand(query, m_Conn);
            string result = string.Empty;
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (passCount < 1)
                    {
                        result += ",'" + dr.GetString(0) + "'";
                    }
                    passCount--;
                }
            }
            //  m_Conn.Close();
            // m_Conn.Dispose();
            return result.Substring(1);

        }


    }
}