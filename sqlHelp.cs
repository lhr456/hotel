using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MysqlSystem
{
    public class SqlHelper
    {
        // 私有静态实例变量，用于保存唯一的实例
        public static volatile SqlHelper _instance;
        // 用于线程同步的锁对象
        private static object _lock = new object();

        // 私有构造函数，接收数据库连接字符串参数
        private SqlHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        // 数据库连接字符串属性
        public string ConnectionString { get; private set; }

        // 公共静态获取实例的方法，通过传入连接字符串获取实例（实现单例模式的关键方法）
        public static SqlHelper Instance(string connectionString)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlHelper(connectionString);
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                _currentTransaction = connection.BeginTransaction();
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            if (_currentTransaction != null)
            {
                try
                {
                    _currentTransaction.Commit();
                    _currentTransaction = null;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("提交事务出错: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (_currentTransaction != null)
            {
                try
                {
                    _currentTransaction.Rollback();
                    _currentTransaction = null;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("回滚事务出错: " + ex.Message);
                }
            }
        }

        // 当前事务对象
        private MySqlTransaction _currentTransaction;

        /// <summary>
        /// 执行非查询语句（如INSERT、UPDATE、DELETE等），关联当前事务（如果存在）
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parameters">参数数组（可选）</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    if (_currentTransaction != null)
                    {
                        command.Transaction = _currentTransaction;
                    }
                    try
                    {
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("执行非查询语句出错: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回单个结果值（如 COUNT(*)、SUM(...) 等聚合函数结果），关联当前事务（如果存在）
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parameters">参数数组（可选）</param>
        /// <returns>查询到的单个结果值（转换为 object 类型）</returns>
        public object ExecuteScalar(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    if (_currentTransaction != null)
                    {
                        command.Transaction = _currentTransaction;
                    }
                    try
                    {
                        connection.Open();
                        return command.ExecuteScalar();
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("执行查询获取单个值语句出错: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回一个 DataTable 结果集，关联当前事务（如果存在）
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parameters">参数数组（可选）</param>
        /// <returns>查询得到的 DataTable 数据</returns>
        public DataTable ExecuteDataTable(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    if (_currentTransaction != null)
                    {
                        command.Transaction = _currentTransaction;
                    }
                    DataTable dataTable = new DataTable();
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                        return dataTable;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("执行查询获取 DataTable 语句出错: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回一个 DataSet 结果集（包含多个 DataTable，适用于多表查询等复杂情况），关联当前事务（如果存在）
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parameters">参数数组（可选）</param>
        /// <returns>查询得到的 DataSet 数据</returns>
        public DataSet ExecuteDataSet(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    if (_currentTransaction != null)
                    {
                        command.Transaction = _currentTransaction;
                    }
                    DataSet dataSet = new DataSet();
                    try
                    {
                        connection.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet);
                        }

                        return dataSet;
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("执行查询获取 DataSet 语句出错: " + ex.Message);
                    }
                }
            }
        }
    }
}