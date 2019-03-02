using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPEX_Car_Rental_System
{
    public class RentalManager
    {
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        public List<Review> Reviews { get; set; }

        public RentalManager()
        {
            Orders = new List<Order>();
            Reviews = new List<Review>();
        }

        //Show List of all cars which are avialable 
        public IReadOnlyList<Car> DisplayAll(List<Car> Cars)
        {
            var query = (from car in Cars
                         where car.Status == true
                         select car).ToList();
            return query;
        }

        //Show the car which is selected in avialable car list for rent
        public IReadOnlyList<Car> DisplaySelected(string CarID, List<Car> Cars)
        {
            var query = (from car in Cars
                         where car.Status == true && car.Id == CarID
                         select car).ToList();
            return query;
        }

        //Order confirm and make a new objct of Order
        public void AddOrder(string carID, string carModel, DateTime pickupDate, DateTime dropOffDate, decimal totalPrice, string userName, bool paid, decimal price, List<Car> Cars)
        {
            var orderCount = Orders.Count() + 1;
            var orderId = Convert.ToString(orderCount);
            var order = new Order(orderId, carID, carModel, pickupDate, dropOffDate, totalPrice, userName, paid, price);
            Orders.Add(order);

            //Change the status(true)  of car when user rent the car
            var query = (from car in Cars
                         where car.Id == carID
                         select car).ToList();
            foreach (var ele in query)
            {
                ele.Status = false;
            }
        }

        //Show order all of List from List<Odre>
        public IReadOnlyList<Order> ShowOrder()
        {
            var queryList = (from order in Orders
                             orderby order.Paid descending, order.OrderId ascending
                             select order).ToList();

            return queryList;
        }

        //Show the order which is selected by carID from List<Odre>
        public IReadOnlyList<Order> ShowOrderForReturn(string orderId)
        {
            var ReturQuery = (from order in Orders
                              where order.OrderId == orderId && order.Paid == false
                              select order).ToList();
            return ReturQuery;
        }

        //Change the status of Paid element when user return the car and pay 
        public void EditOrder(string orderId)
        {
            var query = (from order in Orders
                         where order.OrderId == orderId
                         select order).ToList();
            foreach (var ele in query)
            {
                ele.Paid = true;
            }
        }

        //Change the status(true)  of car when user return the car and pay  
        public void PaidOrder(string carID, List<Car> Cars)
        {
            var query = (from car in Cars
                         where car.Id == carID
                         select car).ToList();
            foreach (var ele in query)
            {
                ele.Status = true;
            }
        }

        //Add new commnet 
        public void AddComment(string orderId, string carId, string model, String userName, String usercomment, DateTime date)
        {
            var comment = new Review(orderId, carId, model, userName, usercomment, date);
            Reviews.Add(comment);
        }

        //Show all comments from List<Review>
        public IReadOnlyList<Review> ShowReviews()
        {

            var carReviews = (from commnet in Reviews
                              orderby commnet.OrderId ascending
                              select commnet).ToList();

            return carReviews;
        }

        //Show all comments from List<Review>
        public IReadOnlyList<Review> ShowSearchedReview(string carID)

        {
            var carReviews = (from commnet in Reviews
                              where commnet.CarId == carID
                              select commnet).ToList();

            return carReviews;
        }
    }
}


