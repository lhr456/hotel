using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL
{
    public class RoomService
    {
        public string roomtype { get; set; }
        public int RoomId { get; set; }
       
        public decimal ServicePriceHour { get; set; }
        public decimal ServicePriceDay { get; set; }
        public string status { get; set; }
    }
}
