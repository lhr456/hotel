using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOTEL;
using MySql.Data.MySqlClient;
using MysqlSystem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace HOTEL
{
     interface IRepository
    {
        // 添加客房服务记录
        int AddRoomService(RoomService roomService);
        // 根据客房编号查询该客房的所有服务记录
        List<RoomService> GetRoomServicesByRoomId(int roomId);
        // 查询所有客房服务记录
        List<RoomService> GetAllRoomServices();

        void QueryRoomById(int roomId, ref string roomType, ref double roomPriceday, ref double roomPricehour);
        // 添加修改房间状态的方法
        void UpdateRoomStatus(int roomId, string newRoomStatus);
    }
    public class RoomRepository : IRepository
    {

        public  void QueryRoomById(int roomId, ref string roomType,ref double roomPriceday,ref double roomPricehour)
        {
            string sql = "SELECT room_type, room_price_per_day,room_price_per_hour FROM rooms WHERE room_id = @roomId";
            MySqlParameter parameter = new MySqlParameter("@roomId", roomId);
           

            try
            {
                DataTable result = SqlHelper.Instance.ExecuteDataTable(sql, new MySqlParameter[] { parameter });
                if (result.Rows.Count > 0)
                {
                     roomType = result.Rows[0]["room_type"].ToString();
                    roomPriceday = Convert.ToDouble(result.Rows[0]["room_price_per_day"]);
                    roomPricehour = Convert.ToDouble(result.Rows[0]["room_price_per_day"]);

                }
                else
                {
                    Console.WriteLine($"No room found with ID: {roomId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        int IRepository.AddRoomService(RoomService roomService)
        {
            throw new NotImplementedException();
        }

        public List<RoomService> GetAllRoomServices()
        {
            List<RoomService> roomServices = new List<RoomService>();
            string sql = "SELECT * FROM rooms";
            try
            {
                DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        RoomService service = new RoomService
                        {

                            RoomId = Convert.ToInt32(row["room_id"]),
                            roomtype = row["room_type"].ToString(),
                            ServicePriceDay = Convert.ToDecimal(row["room_price_per_day"]),
                            ServicePriceHour = Convert.ToDecimal(row["room_price_per_hour"]),
                            status = row["room_status"].ToString()
                        };
                        roomServices.Add(service);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取所有客房服务记录时出现错误: {ex.Message}");
                // 可以在这里添加更完善的错误处理逻辑，比如记录日志等
            }
            return roomServices;
        }

        public List<RoomService> GetRoomServicesByRoomId(int roomId)
        {
            List<RoomService> roomServices = new List<RoomService>();
            string sql = "SELECT * FROM room_services WHERE room_id = @room_id";
            MySqlParameter parameter = new MySqlParameter("@room_id", MySqlDbType.Int32);
            parameter.Value = roomId;
            try
            {
                DataTable dataTable = SqlHelper.Instance.ExecuteDataTable(sql, new MySqlParameter[] { parameter });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        RoomService service = new RoomService
                        {
                            RoomId = Convert.ToInt32(row["room_id"]),
                            roomtype = row["room_type"].ToString(),
                            ServicePriceDay = Convert.ToDecimal(row["room_price_per_day"]),
                            ServicePriceHour = Convert.ToDecimal(row["room_price_per_hour"]),
                            status = row["room_status"].ToString()
                        };
                        roomServices.Add(service);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"根据客房编号 {roomId} 获取客房服务记录时出现错误: {ex.Message}");
                // 可以在这里添加更完善的错误处理逻辑，比如记录日志等
            }
            return roomServices;
        }

        public void UpdateRoomStatus(int roomId, string newRoomStatus)
        {
            string sql = "UPDATE Rooms SET room_status = @newRoomStatus WHERE room_id = @roomId";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@roomId", roomId),
                new MySqlParameter("@newRoomStatus", newRoomStatus)
            };

            try
            {
                // 调用SqlHelper类中的方法（假设名为ExecuteNonQuery）来执行UPDATE语句
                int affectedRows = SqlHelper..ExecuteNonQuery(sql, parameters);
                if (affectedRows == 0)
                {
                    Console.WriteLine($"没有找到对应房间ID为 {roomId} 的房间，无法更新状态。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新房间状态时出现错误: {ex.Message}");
            }
        }
    }


}
