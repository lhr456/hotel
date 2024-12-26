using MySql.Data.MySqlClient;
using MysqlSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement
{
    // 实体类
    public class CheckIn
    {
        public int check_in_id { get; set; }
        public int guest_id { get; set; }
        public int room_id { get; set; }
        public DateTime check_in_time { get; set; }
        public DateTime? check_out_time { get; set; }
        public string pay_method { get; set; }
    }

    // 接口
    public interface ICheckInRepository
    {
        // 添加入住记录
        int AddCheckIn(CheckIn checkIn);
        // 根据入住记录ID获取入住记录
        CheckIn GetCheckInById(int checkInId);
        // 获取所有入住记录
        List<CheckIn> GetAllCheckIns();
        // 根据房间ID获取入住记录
        List<CheckIn> GetCheckInsByRoomId(int roomId);
        // 根据客户ID获取入住记录
        List<CheckIn> GetCheckInsByCustomerId(int customerId);
        // 更新入住记录
        void UpdateCheckIn(CheckIn checkIn);
        // 删除入住记录
        void DeleteCheckIn(int checkInId);
    }

    // 接口实现
    public class CheckInRepository : ICheckInRepository
    {
        public CheckIn GetCheckInById(int checkInId)
        {
            string sql = "SELECT * FROM check_ins WHERE check_in_id = @checkInId";
            MySqlParameter[] parameters = {
                new MySqlParameter("@checkInId", checkInId)
            };

            try
            {
                DataTable dataTable = SqlHelper.ExecuteDataTable(sql, parameters);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return new CheckIn
                    {
                        check_in_id = Convert.ToInt32(row["check_in_id"]),
                        guest_id = Convert.ToInt32(row["customer_id"]),
                        room_id = Convert.ToInt32(row["room_id"]),
                        check_in_time = Convert.ToDateTime(row["check_in_time"]),
                        check_out_time = row["check_out_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["check_out_time"]),
                        pay_method = row["pay_method"].ToString()
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取入住记录时出现错误: {ex.Message}");
                return null;
            }
        }

        // 以下是其他接口方法的实现示例，你可以根据实际需求完善

        public int AddCheckIn(CheckIn checkIn)
        {
            string sql = "INSERT INTO check_ins ( guest_id,room_id, check_in_time, check_out_time, pay_method) VALUES (@guest_id,@room_id, @check_in_time, @check_out_time, @pay_method)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@guest_id", checkIn.room_id),
               
                new MySqlParameter("@room_id", checkIn.room_id),
                new MySqlParameter("@check_in_time", checkIn.check_in_time),
                new MySqlParameter("@check_out_time", checkIn.check_out_time),
                new MySqlParameter("@pay_method", checkIn.pay_method)
            };
            try
            {
                return SqlHelper.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"添加入住记录时出现错误: {ex.Message}");
                return -1; // 可以根据实际情况返回合适的错误码表示添加失败
            }
        }

        public List<CheckIn> GetAllCheckIns()
        {
            string sql = "SELECT * FROM check_ins";
            MySqlParameter[] parameters = null;
            try
            {
                DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql, parameters);
                List<CheckIn> checkIns = new List<CheckIn>();
                foreach (DataRow row in dataTable.Rows)
                {
                    checkIns.Add(new CheckIn
                    {
                        check_in_id = Convert.ToInt32(row["check_in_id"]),
                        guest_id = Convert.ToInt32(row["customer_id"]),
                        room_id = Convert.ToInt32(row["room_id"]),
                        check_in_time = Convert.ToDateTime(row["check_in_time"]),
                        check_out_time = row["check_out_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["check_out_time"]),
                        pay_method = row["pay_method"].ToString()
                    });
                }
                return checkIns;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取所有入住记录时出现错误: {ex.Message}");
                return new List<CheckIn>(); // 返回空列表表示获取失败
            }
        }

        public List<CheckIn> GetCheckInsByRoomId(int roomId)
        {
            string sql = "SELECT * FROM check_ins WHERE room_id = @roomId";
            MySqlParameter[] parameters = {
                new MySqlParameter("@roomId", roomId)
            };
            try
            {
                DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql, parameters);
                List<CheckIn> checkIns = new List<CheckIn>();
                foreach (DataRow row in dataTable.Rows)
                {
                    checkIns.Add(new CheckIn
                    {
                        check_in_id = Convert.ToInt32(row["check_in_id"]),
                        guest_id = Convert.ToInt32(row["customer_id"]),
                        room_id = Convert.ToInt32(row["room_id"]),
                        check_in_time = Convert.ToDateTime(row["check_in_time"]),
                        check_out_time = row["check_out_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["check_out_time"]),
                        pay_method = row["pay_method"].ToString()
                    });
                }
                return checkIns;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"根据房间ID获取入住记录时出现错误: {ex.Message}");
                return new List<CheckIn>();
            }
        }

        public List<CheckIn> GetCheckInsByCustomerId(int customerId)
        {
            string sql = "SELECT * FROM check_ins WHERE customer_id = @customerId";
            MySqlParameter[] parameters = {
                new MySqlParameter("@customerId", customerId)
            };
            try
            {
                DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql, parameters);
                List<CheckIn> checkIns = new List<CheckIn>();
                foreach (DataRow row in dataTable.Rows)
                {
                    checkIns.Add(new CheckIn
                    {
                        check_in_id = Convert.ToInt32(row["check_in_id"]),
                        guest_id = Convert.ToInt32(row["customer_id"]),
                        room_id = Convert.ToInt32(row["room_id"]),
                        check_in_time = Convert.ToDateTime(row["check_in_time"]),
                        check_out_time = row["check_out_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["check_out_time"]),
                        pay_method = row["pay_method"].ToString()
                    });
                }
                return checkIns;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"根据客户ID获取入住记录时出现错误: {ex.Message}");
                return new List<CheckIn>();
            }
        }

        public void UpdateCheckIn(CheckIn checkIn)
        {
            string sql = "UPDATE check_ins SET customer_id = @customer_id, room_id = @room_id, check_in_time = @check_in_time, check_out_time = @check_out_time, pay_method = @pay_method WHERE check_in_id = @check_in_id";
            MySqlParameter[] parameters = {
                new MySqlParameter("@check_in_id", checkIn.check_in_id),
                new MySqlParameter("@customer_id", checkIn.guest_id),
                new MySqlParameter("@room_id", checkIn.room_id),
                new MySqlParameter("@check_in_time", checkIn.check_in_time),
                new MySqlParameter("@check_out_time", checkIn.check_out_time),
                new MySqlParameter("@pay_method", checkIn.pay_method)
            };
            try
            {
                SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新入住记录时出现错误: {ex.Message}");
            }
        }

        public void DeleteCheckIn(int checkInId)
        {
            string sql = "DELETE FROM check_ins WHERE check_in_id = @checkInId";
            MySqlParameter[] parameters = {
                new MySqlParameter("@checkInId", checkInId)
            };
            try
            {
                SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除入住记录时出现错误: {ex.Message}");
            }
        }
    }
}