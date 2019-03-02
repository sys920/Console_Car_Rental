using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPEX_Car_Rental_System
{
    public class Order
    {
        public string OrderId { get; set; }
        public string CarId { get; set; }
        public string Model { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public decimal Totalprice { get; set; }
        public string UserName { get; set; }
        public bool Paid { get; set; }
        public decimal Price { get; set; }

        public Order(string orderId, string carId, string model, DateTime pickupDate, DateTime dropOffDate, decimal totalPrice, String userName, bool paid, decimal price)
        {
            OrderId = orderId;
            CarId = carId;
            Model = model;
            PickupDate = pickupDate;
            DropOffDate = dropOffDate;
            Totalprice = totalPrice;
            UserName = userName;
            Paid = paid;
            Price = price;
        }
    }
}
