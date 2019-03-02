using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPEX_Car_Rental_System
{
    class Program
    {
        private static RentalManager RentalManager { get; set; }

        static void Main(string[] args)
        {
            // 7 Cars Test Object
            var Cars = new List<Car>
            {
                new Car() {Id="100", Brand="Nissan", Model="Maxima ", Price= 72.99m, Feature="Premium, AT, GPS, Free Infant seat, AC", Status=true },
                new Car() {Id="200",Brand="Toyota", Model="Yaris", Price=79.99m, Feature="Economy,MT, AC", Status=true},
                new Car() {Id="300", Brand="Toyota", Model="RAV4 ", Price=91.99m, Feature="Midsize SUV,AT, GPS, AC", Status=true},
                new Car() {Id="400", Brand="Volkswagen", Model="Passat", Price=83.99m, Feature="Standard, AT, GPS, Ski rack,AC", Status=true},
                new Car() {Id="500", Brand="Chrysler", Model="C300", Price=113.99m, Feature="Premium, AT, AC", Status=true},
                new Car() {Id="600", Brand="Volkswagen", Model="Golf ", Price=109.99m, Feature="Premium, AT, AC,Smoke Free", Status=true},
                new Car() {Id="700", Brand="Dodge", Model="Grand Caravan", Price=137.99m, Feature="Premium van, AT, RoofRack, AC", Status=true},
            };

            // Make new object for managing rental system
            RentalManager = new RentalManager();

            var rentalSystemLoop = false;

            //Rental system while Loop 
            while (rentalSystemLoop != true)
            {

                // Query for display all cars available for rental
                var carsQuery = RentalManager.DisplayAll(Cars);

                //Display main menu 
                Console.Clear();
                Console.WriteLine("==================================================================");
                Console.WriteLine("                        [Car Rental system]                       ");
                Console.WriteLine("==================================================================");
                Console.WriteLine("1 - Rent a Car");
                Console.WriteLine("2 - Ordered List (Return the Car)");
                Console.WriteLine("3 - Reviews");
                Console.WriteLine("0 - Exit App");
                Console.WriteLine("==================================================================");
                Console.Write("Type number you want: ");
                var userKeyBoardInput = Console.ReadLine();
                //Type menu for rental
                if (userKeyBoardInput == "1")
                {
                    Console.Clear();
                    Console.WriteLine("==================================================================");
                    Console.WriteLine("                       [Choose a Car]                             ");
                    Console.WriteLine("==================================================================");
                    Console.WriteLine(string.Format("{0,5} | {1,-15 } | {2,-15} | {3,-7} |", "CarId", "Brand", "Model", "Price"));
                    Console.WriteLine("==================================================================");

                    //Show all cars available     
                    foreach (var car in carsQuery)
                    {
                        Console.WriteLine(string.Format("{0,5} | {1,-15 } | {2,-15} | {3,-7} |", car.Id, car.Brand, car.Model, "$" + car.Price));
                    }
                    Console.WriteLine("==================================================================");

                    //Type the car ID that a user want to rent 
                    Console.Write("Type CarId you want , type '0'or Enter go to the menu :");
                    var carIdInput = Console.ReadLine();

                    if (carIdInput != "0" && carIdInput != "")
                    {
                        var rentLogicloop = false;
                        while (rentLogicloop != true)
                        {
                            Console.Clear();
                            decimal rentPrice = 0;
                            string carModel = "";
                            string carId = "";
                            var carReserveinput = "0";

                            var carRentQuery = RentalManager.DisplaySelected(carIdInput, Cars);

                            if (carRentQuery.Count() == 0)
                            {
                                Console.WriteLine("==================================================================");
                                Console.WriteLine("           Sorry, there is no Result you typed                    ");
                                Console.WriteLine("==================================================================");
                                Console.Write("Press any key to menu...");
                                Console.ReadLine();
                                rentLogicloop = true;

                            }
                            else
                            {
                                Console.WriteLine("==================================================================");

                                Console.WriteLine("                          [Detail Features]                        ");
                                Console.WriteLine("==================================================================");
                                foreach (var car in carRentQuery)
                                {

                                    Console.WriteLine(string.Format("{0,-11} : {1,-20}", "ID ", car.Id));
                                    Console.WriteLine(string.Format("{0,-11} : {1,-20}", "Brand ", car.Brand));
                                    Console.WriteLine(string.Format("{0,-11} : {1,-20}", "Model ", car.Model));
                                    Console.WriteLine(string.Format("{0,-11} : {1,-20}", "Feature ", car.Feature));
                                    Console.WriteLine(string.Format("{0,-11} : {1,-20}", "Price ", "$" + car.Price));

                                    //Passing car infomation for order confirm
                                    rentPrice = car.Price;
                                    carModel = car.Model;
                                    carId = car.Id;

                                    // Load User reviews 
                                    var carReviews = RentalManager.ShowSearchedReview(car.Id);

                                    if (carReviews.Count() != 0)
                                    {
                                        Console.WriteLine($"User Reviews : ");
                                        foreach (var commnet in carReviews)
                                        {
                                            Console.WriteLine($"-{commnet.UserComment} (by {commnet.UserName}){commnet.Date.ToString("MM/dd/yyyy")} ");
                                        }
                                    }
                                    Console.WriteLine("==================================================================");

                                    //Type the date
                                    Console.WriteLine("1 - Rent this");
                                    Console.WriteLine("0 - Cancel and go to the main menu");
                                    carReserveinput = Console.ReadLine();
                                }
                            }

                            //Type username and dates of rent for rent

                            if (carReserveinput == "1")
                            {
                                Console.WriteLine("==================================================================");
                                Console.Write("Type your name :");
                                var userName = Console.ReadLine();

                                Console.WriteLine("Type the Date you rent(MM/dd/yyyy) :");
                                Console.Write("From:");


                                //Checking validation the date pickup
                                bool inputDateValid = false;
                                DateTime pickupDate = DateTime.MinValue;
                                while (inputDateValid != true)
                                {
                                    if (DateTime.TryParse(Console.ReadLine(), out pickupDate))
                                    {
                                        inputDateValid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Type the Date you rent(MM/dd/yyyy) :");
                                        Console.Write("From:");
                                    }
                                }

                                Console.WriteLine("Type the Date you return(MM/dd/yyyy) :");
                                Console.Write("  To:");

                                //Checking validation the date dropOffDate
                                inputDateValid = false;
                                DateTime dropOffDate = DateTime.MinValue;
                                while (inputDateValid != true)
                                {
                                    if (DateTime.TryParse(Console.ReadLine(), out dropOffDate))
                                    {
                                        inputDateValid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Type the Date you rent(MM/dd/yyyy) :");
                                        Console.Write("To:");
                                    }
                                }

                                var numberOfDays = (dropOffDate - pickupDate).TotalDays;
                                var numberOfDayLoop = false;

                                while (numberOfDayLoop != true)
                                {
                                    if (numberOfDays > 30 || numberOfDays <= 0)
                                    {
                                        Console.WriteLine("Can't exeed over 30days or less than 1day for rent, type again!");
                                        Console.WriteLine("Type the Date you return(MM/dd/yyyy) :");
                                        Console.Write("  To:");

                                        //Checking validation the date dropOffDate within 30day
                                        inputDateValid = false;
                                        dropOffDate = DateTime.MinValue;
                                        while (inputDateValid != true)
                                        {
                                            if (DateTime.TryParse(Console.ReadLine(), out dropOffDate))
                                            {
                                                inputDateValid = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Can't exeed over 30days for rent, type again!");
                                                Console.WriteLine("Type the Date you return(MM/dd/yyyy) :");
                                                Console.Write("  To:");
                                            }
                                        }

                                        numberOfDays = (dropOffDate - pickupDate).TotalDays;
                                    }
                                    else
                                    {
                                        numberOfDayLoop = true;
                                    }
                                }

                                var totalPrice = Convert.ToInt32(numberOfDays) * rentPrice;

                                //Display the Price for rent
                                Console.WriteLine("==================================================================");

                                Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Day(s) ", Convert.ToInt32(numberOfDays) + "day(s)"));

                                Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Total Price  ", "$" + totalPrice));

                                //Type order confirm
                                Console.WriteLine("==================================================================");
                                Console.WriteLine("1 - Confirm");
                                Console.WriteLine("0 - Cancel and go to the main menu");
                                Console.WriteLine("==================================================================");
                                var orderConfirmInput = Console.ReadLine();

                                if (orderConfirmInput == "1")
                                {

                                    RentalManager.AddOrder(carId, carModel, pickupDate, dropOffDate, totalPrice, userName, false, rentPrice, Cars);

                                    Console.WriteLine("order confirmed !");

                                    Console.WriteLine("==================================================================");
                                    Console.Write("Press any key to menu...");
                                    Console.ReadLine();

                                    rentLogicloop = true;
                                }
                                else if (orderConfirmInput == "0")
                                {
                                    rentLogicloop = true;
                                }

                            }
                            else if (carReserveinput == "0")
                            {
                                rentLogicloop = true;
                            }
                        }
                    }

                }
                //Returning the car from the oder List
                else if (userKeyBoardInput == "2")
                {
                    Console.Clear();
                    Console.WriteLine("==================================================================");
                    Console.WriteLine("                          [Oders List]                            ");
                    Console.WriteLine("==================================================================");
                    Console.WriteLine(string.Format("{0,5} | {1,-12 } | {2,-8} | {3,-10} | {4,-15} |", "Id", "ReturnDate", "Payment", "Model", "UserName"));
                    Console.WriteLine("==================================================================");

                    var orders = RentalManager.ShowOrder();
                    var counter = 0;

                    if (orders.Count() == 0)
                    {
                        Console.WriteLine("                          No Result                            ");
                    }

                    foreach (var ele in orders)
                    {
                        counter = counter + 1;
                        var payment = (ele.Paid == false) ? "NotPaid" : "Paid";
                        if (payment == "NotPaid")
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(string.Format("{0,5} | {1,-12 } | {2,-8} | {3,-10} | {4,-15} |", ele.OrderId, ele.DropOffDate.ToString("MM/dd/yyy"), payment, ele.Model, ele.UserName));
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine(string.Format("{0,5} | {1,-12 } | {2,-8} | {3,-10} | {4,-15} |", ele.OrderId, ele.DropOffDate.ToString("MM/dd/yyy"), payment, ele.Model, ele.UserName));
                        }
                    }

                    Console.WriteLine("==================================================================");
                    Console.WriteLine(" Type the 'ID' to return the car, type '0'or Enter go to the menu");
                    var returnCarIdInput = Console.ReadLine();

                    if (returnCarIdInput != "0" && returnCarIdInput != "")
                    {

                        var orderDetail = RentalManager.ShowOrderForReturn(returnCarIdInput);

                        Console.Clear();
                        Console.WriteLine("==================================================================");

                        Console.WriteLine("                          [Oders List]                            ");
                        Console.WriteLine("==================================================================");

                        if (orderDetail.Count() == 0)
                        {
                            Console.WriteLine("              Sorry, there is no Result you typed                 ");
                            Console.WriteLine("==================================================================");
                            Console.Write("Press any key to menu...");
                            Console.ReadLine();
                        }
                        else
                        {
                            foreach (var ele in orderDetail)
                            {
                                //Check rental days wheather it  is early or late at now  
                                var numberOfDays = Convert.ToInt32((DateTime.Now - ele.DropOffDate).TotalDays) - 1;

                                //Check return date before pickupdate
                                var rentCancel = Convert.ToInt32((ele.PickupDate - DateTime.Now).TotalDays);

                                Console.WriteLine(string.Format("{0,-17} : {1,-20}", "ID ", ele.OrderId));
                                Console.WriteLine(string.Format("{0,-17} : {1,-20}", "User Name ", ele.UserName));
                                Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Model ", ele.Model));
                                Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Pick-up ", ele.PickupDate.ToString("MM/dd/yyyy")));
                                Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Drop Off ", ele.DropOffDate.ToString("MM/dd/yyyy")));

                                var refundOfEalryReturn = 0;
                                var numberOfEarlyRetunDay = 0;
                                var overChargeOfDelayReturn = 0;
                                var numberOfDelayRetunDay = 0;
                                var penaltyOver30Days = 0;
                                decimal grandTotal = 0;

                                // Delay days 
                                if (numberOfDays < 0)
                                {
                                    //When user return car early 
                                    refundOfEalryReturn = numberOfDays * Convert.ToInt32(ele.Price);
                                    numberOfEarlyRetunDay = Math.Abs(numberOfDays);
                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Refund: ", "$" + Math.Abs(overChargeOfDelayReturn)));
                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Early Day(s): ", Math.Abs(numberOfEarlyRetunDay) + " day(s)"));

                                }

                                else if (numberOfDays >= 0 && numberOfDays < 300)
                                {
                                    overChargeOfDelayReturn = numberOfDays * Convert.ToInt32(ele.Price);
                                    numberOfDelayRetunDay = numberOfDays;
                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Delay Charge: ", "$" + overChargeOfDelayReturn));
                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Delay Day(s): ", numberOfDelayRetunDay + " day(s)"));

                                }
                                else if (numberOfDays > 300)
                                {
                                    //Delay over 30days
                                    numberOfDelayRetunDay = numberOfDays;
                                    overChargeOfDelayReturn = numberOfDelayRetunDay * Convert.ToInt32(ele.Price);
                                    penaltyOver30Days = 200;

                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Delay Day(s): ", "$" + numberOfDelayRetunDay) + " day(s)");
                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Charge for over 30days: ", "$" + penaltyOver30Days));
                                }

                                grandTotal = ele.Totalprice + refundOfEalryReturn + overChargeOfDelayReturn + penaltyOver30Days;

                                //The order is canceled when user return car early than you pickup date.
                                if (rentCancel > 0)
                                {
                                    grandTotal = 0;

                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Total Amount ", "$" + grandTotal));
                                    Console.WriteLine("Your order hasn't start yet!");
                                }
                                else
                                {
                                    grandTotal = ele.Totalprice + refundOfEalryReturn + overChargeOfDelayReturn + penaltyOver30Days;
                                    Console.WriteLine(string.Format("{0,-17} : {1,-20}", "Total Amount ", "$" + grandTotal));
                                }


                                Console.WriteLine("==================================================================");
                                Console.WriteLine("1 - Paid");
                                Console.WriteLine("0 - Cancel and go to the main menu");
                                var paidInput = Console.ReadLine();

                                if (paidInput == "1")
                                {
                                    Console.WriteLine("Do you wnat to commnet? Leave a  message or enter to skip");

                                    RentalManager.PaidOrder(ele.CarId, Cars);
                                    RentalManager.EditOrder(ele.OrderId);

                                    var TooDay = DateTime.Now;
                                    var usercomment = Console.ReadLine();

                                    if (usercomment == "")
                                    {
                                        usercomment = string.Empty;
                                    }
                                    else
                                    {
                                        RentalManager.AddComment(ele.OrderId, ele.CarId, ele.Model, ele.UserName, usercomment, TooDay);
                                    }
                                    Console.Clear();
                                    Console.WriteLine("==================================================================");
                                    Console.WriteLine("                  Thank you for visiting                          ");
                                    Console.WriteLine("==================================================================");
                                    Console.Write("Press any key to menu...");
                                    Console.ReadLine();
                                }
                            }
                        }
                    }
                }

                // Show all reviews 
                else if (userKeyBoardInput == "3")
                {
                    Console.Clear();
                    Console.WriteLine("===================================================================================");
                    Console.WriteLine("                              [List of User Reviews]                               ");
                    Console.WriteLine("===================================================================================");

                    Console.WriteLine(string.Format("{0,5} | {1,-15 } | {2,-12} | {3,-15} | {4,-21} |", "Id", "CarModel", "Date", "UserName", "UserComment"));
                    Console.WriteLine("===================================================================================");
                    var reviews = RentalManager.ShowReviews();

                    if (reviews.Count() == 0)
                    {
                        Console.WriteLine("                          No Result                            ");
                    }
                    foreach (var commnet in reviews)
                    {

                        Console.WriteLine(string.Format("{0,5} | {1,-15 } | {2,-12} | {3,-15} | {4,-21} ", commnet.OrderId, commnet.Model, commnet.Date.ToString("MM/dd/yyyy"), commnet.UserName, commnet.UserComment));
                    }
                    Console.WriteLine("===================================================================================");
                    Console.Write("Press any key to menu...");
                    Console.ReadLine();
                }
                else if (userKeyBoardInput == "0")
                {
                    rentalSystemLoop = true;
                }
            }
        }
    }
}
