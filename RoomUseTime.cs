using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MysqlSystem;
using System.Data;
using Sunny.UI;

namespace HotelManagement
{
    // 实体类
    public class RoomUseTime
    {
        public int? room_use_time_id { get; set; }
        public int? room_id { get; set; }
        public int? guest_id { get; set; }
        public DateTime? check_in_time { get; set; }
        public DateTime? check_out_time { get; set; }
        public int? usage_duration { get; set; }
        public bool? is_duration_in_hours { get; set; }
      
        public double? total_cost { get; set; }
        public string payment_status { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }

    // 接口
    public interface IRoomUseTimeRepository
    {
        // 获取所有房间使用时间记录
        List<RoomUseTime> GetAllRoomUseTimes();
        // 根据房间使用时间记录ID获取记录
        RoomUseTime GetRoomUseTimeById(int? roomUseTimeId);
        // 添加房间使用时间记录
        int AddRoomUseTime(RoomUseTime roomUseTime);
        // 更新房间使用时间记录
        void UpdateRoomUseTime(RoomUseTime roomUseTime);
        // 删除房间使用时间记录
        void DeleteRoomUseTime(int? roomUseTimeId);
        List<RoomUseTime> GetRoomUseTimeRecordsBeforeNow();
    }

    // 接口实现类
    public class RoomUseTimeRepository : IRoomUseTimeRepository
    {
        public List<RoomUseTime> GetRoomUseTimeRecordsBeforeNow()
        {
            string sql = "SELECT room_id, check_in_time, check_out_time, total_cost FROM hotelmanagement.room_use_time WHERE check_out_time <NOW();";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@currentTime", DateTime.Now)
            };
            List<RoomUseTime> recordList = new List<RoomUseTime>();

            using (DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql, parameters))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    RoomUseTime record = new RoomUseTime();
                    record.room_id = row["room_id"] == DBNull.Value ? (int?)null : (int)row["room_id"];
                    record.check_in_time = row["check_in_time"] == DBNull.Value ? (DateTime?)null : (DateTime)row["check_in_time"];
                    record.check_out_time = row["check_out_time"] == DBNull.Value ? (DateTime?)null : (DateTime)row["check_out_time"];
                    record.total_cost = row["total_cost"].ToString().ToDouble();

                    recordList.Add(record);
                }
            }

            return recordList;
        }
        public int AddRoomUseTime(RoomUseTime roomUseTime)
        {
            string sql = "INSERT INTO hotelmanagement.room_use_time (room_id, guest_id, check_in_time, check_out_time, usage_duration, is_duration_in_hours, total_cost, payment_status, created_at, updated_at) VALUES (@room_id, @guest_id, @check_in_time, @check_out_time, @usage_duration, @is_duration_in_hours, @total_cost, @payment_status, @created_at, @updated_at)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@room_id", roomUseTime.room_id),
                new MySqlParameter("@guest_id", roomUseTime.guest_id),
                new MySqlParameter("@check_in_time", roomUseTime.check_in_time),
                new MySqlParameter("@check_out_time", roomUseTime.check_out_time),
                new MySqlParameter("@usage_duration", roomUseTime.usage_duration),
                new MySqlParameter("@is_duration_in_hours", roomUseTime.is_duration_in_hours),
               
                new MySqlParameter("@total_cost", roomUseTime.total_cost),
                new MySqlParameter("@payment_status", roomUseTime.payment_status),
                new MySqlParameter("@created_at", roomUseTime.created_at),
                new MySqlParameter("@updated_at", roomUseTime.updated_at)
            };
            try
            {
                return SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"添加房间使用时间记录时出错: {ex.Message}");
                return -1;
            }
        }

        // 更新房间使用时间记录
        public void UpdateRoomUseTime(RoomUseTime roomUseTime)
        {
            string sql = "UPDATE hotelmanagement.room_use_time SET room_id = @room_id, guest_id = @guest_id, check_in_time = @check_in_time, check_out_time = @check_out_time, usage_duration = @usage_duration, is_duration_in_hours = @is_duration_in_hours, room_service_ids = @room_service_ids, total_cost = @total_cost, payment_status = @payment_status, created_at = @created_at, updated_at = @updated_at WHERE room_use_time_id = @room_use_time_id";
            MySqlParameter[] parameters = {
                new MySqlParameter("@room_use_time_id", roomUseTime.room_use_time_id),
                new MySqlParameter("@room_id", roomUseTime.room_id),
                new MySqlParameter("@guest_id", roomUseTime.guest_id),
                new MySqlParameter("@check_in_time", roomUseTime.check_in_time),
                new MySqlParameter("@check_out_time", roomUseTime.check_out_time),
                new MySqlParameter("@usage_duration", roomUseTime.usage_duration),
                new MySqlParameter("@is_duration_in_hours", roomUseTime.is_duration_in_hours),
               
                new MySqlParameter("@total_cost", roomUseTime.total_cost),
                new MySqlParameter("@payment_status", roomUseTime.payment_status),
                new MySqlParameter("@created_at", roomUseTime.created_at),
                new MySqlParameter("@updated_at", roomUseTime.updated_at)
            };
            try
            {
                SqlHelper.Instance.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新房间使用时间记录时出错: {ex.Message}");
            }
        }

        // 删除房间使用时间记录
        public void DeleteRoomUseTime(int? roomUseTimeId)
        {
            string sql = "DELETE FROM hotelmanagement.room_use_time WHERE room_use_time_id = @roomUseTimeId";
            MySqlParameter parameter = new MySqlParameter("@roomUseTimeId", roomUseTimeId);
            try
            {
                SqlHelper.Instance.ExecuteNonQuery(sql, new MySqlParameter[] { parameter });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除房间使用时间记录时出错: {ex.Message}");
            }
        }

        public List<RoomUseTime> GetAllRoomUseTimes()
        {
            throw new NotImplementedException();
        }

        public RoomUseTime GetRoomUseTimeById(int? roomUseTimeId)
        {

            string sql = "SELECT * FROM hotelmanagement.room_use_time WHERE use_time_id = @roomUseTimeId";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@roomUseTimeId", roomUseTimeId)
            };

            using (DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql, parameters))
            {
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    RoomUseTime roomUseTime = new RoomUseTime();
                    roomUseTime.room_use_time_id = row["use_time_id"] == DBNull.Value ? (int?)null : (int)row["use_time_id"];
                    roomUseTime.room_id = row["room_id"] == DBNull.Value ? (int?)null : (int)row["room_id"];
                    roomUseTime.guest_id = row["guest_id"] == DBNull.Value ? (int?)null : (int)row["guest_id"];
                    roomUseTime.check_in_time = row["check_in_time"] == DBNull.Value ? (DateTime?)null : (DateTime)row["check_in_time"];
                    roomUseTime.check_out_time = row["check_out_time"] == DBNull.Value ? (DateTime?)null : (DateTime)row["check_out_time"];
                    roomUseTime.usage_duration = row["usage_duration"] == DBNull.Value ? (int?)null : (int)row["usage_duration"];
                    roomUseTime.is_duration_in_hours = row["is_duration_in_hours"] == DBNull.Value ? (bool?)null : (bool)row["is_duration_in_hours"];
                  
                  
                    roomUseTime.created_at = row["created_at"] == DBNull.Value ? (DateTime?)null : (DateTime)row["created_at"];
                    roomUseTime.updated_at = row["updated_at"] == DBNull.Value ? (DateTime?)null : (DateTime)row["updated_at"];
                    return roomUseTime;
                }
            }
            return null;
        }
    }
}