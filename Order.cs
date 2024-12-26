using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysqlSystem;
using System.Data;
using Sunny.UI;
using System.Data.SqlClient;
namespace HOTEL
{
    public class Order
    {
        public int order_id { get; set; }
        public int guest_id { get; set; }
        public int room_id { get; set; }
        public string order_date { get; set; }
        public string check_in_date { get; set; }
        public string check_out_date { get; set; }
        public int num_guests { get; set; }
        public double total_amount { get; set; }
        public string timetype { get; set; }
        public string order_status { get; set; }
        public string roomtype { get; set; }
    }
    public interface IOrderRepository
    {
        void UpdateOrderStatus(int roomId);
        // 获取所有订单
        List<Order> GetAllOrders();

        // 根据订单ID获取订单
        Order GetOrderById(int orderId);

        // 添加订单
        void AddOrder(Order order);

        // 更新订单
        void UpdateOrder(Order order);

        // 删除订单
        void DeleteOrder(int orderId);
    }
    public class OrderRepository : IOrderRepository
    {
        public void UpdateOrderStatus(int roomId)
        {
            string sql = "UPDATE hotelmanagement.orders SET order_status = '已完成' WHERE room_id = @roomId";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@roomId", roomId)
            };

            try
            {
                SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"更新订单状态出错: {ex.Message}");
            }

        }

        public List<Order> GetAllOrders()
        {
            string sql = "SELECT * FROM orders";
            List<Order> orderList = new List<Order>();
            try
            {
                using (DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql, null))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Order order = new Order();
                        order.order_id = row["order_id"].ToString().ToInt();
                        order.guest_id = row["guest_id"].ToString().ToInt();
                        order.room_id = row["room_id"].ToString().ToInt();
                        order.order_date = row["order_date"] == DBNull.Value ? null : row["order_date"].ToString();
                        order.check_in_date = row["check_in_date"] == DBNull.Value ? null : row["check_in_date"].ToString();
                        order.check_out_date = row["check_out_date"] == DBNull.Value ? null : row["check_out_date"].ToString();
                        order.num_guests = row["num_guests"].ToString().ToInt();
                        order.total_amount = row["total_amount"].ToString().ToInt();
                        order.timetype = row["timetype"].ToString();
                        order.order_status = row["order_status"].ToString();
                        order.roomtype = row["roomtype"].ToString();

                        orderList.Add(order);



                       
                    }
                }
                return orderList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取所有订单信息时出错: {ex.Message}");
                return new List<Order>();
            }
        }

        public Order GetOrderById(int orderId)
        {
            return null;
        }

        public void AddOrder(Order order)
        {
            try
            {
                string sql = "INSERT INTO orders (order_id,guest_id, room_id, order_date, check_in_date, check_out_date, num_guests, total_amount, timetype, order_status, roomtype) VALUES (@order_id,@guest_id, @room_id, @order_date, @check_in_date, @check_out_date, @num_guests, @total_amount, @timetype, @order_status, @roomtype)";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@order_id", order.order_id),
                    new MySqlParameter("@guest_id", order.guest_id),
                    new MySqlParameter("@room_id", order.room_id),
                    new MySqlParameter("@order_date", order.order_date),
                    new MySqlParameter("@check_in_date", order.check_in_date),
                    new MySqlParameter("@check_out_date", order.check_out_date),
                    new MySqlParameter("@num_guests", order.num_guests),
                    new MySqlParameter("@total_amount", order.total_amount),
                    new MySqlParameter("@timetype", order.timetype),
                    new MySqlParameter("@order_status", order.order_status),
                    new MySqlParameter("@roomtype", order.roomtype)
                };
     
                int affectedRows = SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
            }catch(Exception ex)
            {
                throw new Exception("数据库操作出错", ex);
            }  
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }
    }

      
    
}
