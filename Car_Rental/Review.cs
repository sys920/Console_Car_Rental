using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPEX_Car_Rental_System
{
    public class Review
    {
        public string OrderId { get; set; }
        public string CarId { get; set; }
        public string Model { get; set; }
        public string UserName { get; set; }
        public string UserComment { get; set; }
        public DateTime Date { get; set; }

        public Review(string orderId, string carId, string model, String userName, String userComment, DateTime date)
        {
            OrderId = orderId;
            CarId = carId;
            Model = model;
            UserName = userName;
            UserComment = userComment;
            Date = date;

        }
    }
}
