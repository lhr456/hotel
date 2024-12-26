using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL
{
    using System;
    using System.Collections.Generic;
    using MySql.Data.MySqlClient;
    using MysqlSystem;

    namespace HotelManagement
    {
        // 实体类
        public class Customer
        {
            public int? customer_id { get; set; }
            public int? guest_id { get; set; }
            public string customer_contact { get; set; }
            public string id_sh { get; set; }
        }

        // 接口
        public interface ICustomerRepository
        {
            // 获取所有客户记录
            List<Customer> GetAllCustomers();
            // 根据客户ID获取客户记录
            Customer GetCustomerById(int? customerId);
            // 添加客户记录
            int AddCustomer(Customer customer);
            // 更新客户记录
            void UpdateCustomer(Customer customer);
            // 删除客户记录
            void DeleteCustomer(int? customerId);
        }

        // 接口实现类
        // TODO
        public class CustomerRepository : ICustomerRepository
        {
            // 获取所有客户记录
            public List<Customer> GetAllCustomers()
            {
                string sql = "SELECT * FROM hotelmanagement.customers";
                MySqlParameter[] parameters = null;
                List<Customer> customers = new List<Customer>();
                try
                {
                    var dataTable = SqlHelper.Instance.ExecuteDataTable(sql, parameters);
                    foreach (var row in dataTable.Rows)
                    {
                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"获取所有客户记录时出错: {ex.Message}");
                }
                return customers;
            }

            // 根据客户ID获取客户记录
            public Customer GetCustomerById(int? customerId)
            {
                string sql = "SELECT * FROM hotelmanagement.customers WHERE customer_id = @customerId";
                MySqlParameter parameter = new MySqlParameter("@customerId", customerId);
                try
                {
                    var dataTable = SqlHelper.Instance.ExecuteDataTable(sql, new MySqlParameter[] { parameter });
                    if (dataTable.Rows.Count > 0)
                    {
                        var row = dataTable.Rows[0];
                        return new Customer
                        {
                            customer_id = row["customer_id"] == DBNull.Value ? (int?)null : (int)row["customer_id"],
                            guest_id = row["guest_id"] == DBNull.Value ? (int?)null : (int)row["guest_id"],
                            customer_contact = row["customer_contact"].ToString(),
                            id_sh = row["id_sh"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"根据客户ID获取客户记录时出错: {ex.Message}");
                }
                return null;
            }

            // 添加客户记录
            public int AddCustomer(Customer customer)
            {
                string sql = "INSERT INTO hotelmanagement.customers (guest_id, customer_contact, id_sh) VALUES (@guest_id, @customer_contact, @id_sh)";
                MySqlParameter[] parameters = {
                new MySqlParameter("@guest_id", customer.guest_id),
                new MySqlParameter("@customer_contact", customer.customer_contact),
                new MySqlParameter("@id_sh", customer.id_sh)
            };
                try
                {
                    return SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"添加客户记录时出错: {ex.Message}");
                    return -1;
                }
            }

            // 更新客户记录
            public void UpdateCustomer(Customer customer)
            {
                string sql = "UPDATE hotelmanagement.customers SET guest_id = @guest_id, customer_contact = @customer_contact, id_sh = @id_sh WHERE customer_id = @customer_id";
                MySqlParameter[] parameters = {
                new MySqlParameter("@customer_id", customer.customer_id),
                new MySqlParameter("@guest_id", customer.guest_id),
                new MySqlParameter("@customer_contact", customer.customer_contact),
                new MySqlParameter("@id_sh", customer.id_sh)
            };
                try
                {
                    SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"更新客户记录时出错: {ex.Message}");
                }
            }

            // 删除客户记录
            public void DeleteCustomer(int? customerId)
            {
                string sql = "DELETE FROM hotelmanagement.customers WHERE customer_id = @customerId";
                MySqlParameter parameter = new MySqlParameter("@customerId", customerId);
                try
                {
                    SqlHelper.Instance.ExecuteNonQuery(sql, new MySqlParameter[] { parameter });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"删除客户记录时出错: {ex.Message}");
                }
            }
        }
    }
}
