# Console_Car_Rental
- Practise designing and implementing a simple inventory control system for a small car rental store using the OOP techniques.
- Console program practise using Linq, List base on OOP.

* When renting a car:
- One customer can rent only one car at a time.
- The customer should select the period that he/she wants to keep the car which cannot exceed 30 days.
- The system should display the total amount that the customer will be paying when returning the car based on the selected period. Each car can have different rate.

* When returning a car:
- The system should display the total amount that the customer needs to pay. It should be the same amount that was presented before the customer rented the car.
- If the customer is returning the car after the period they selected, the system should apply a penalty of $10 per day. If the period exceeds 30 days a penalty of $200 should be applied to the final balance.
- Adding comments when returning a car is not required.


# Code
- Dist directory :  setup.exe  ( You can install and run this rental system) 

- Car.cs :  car class 
- order.cs :  place car rental order class
- Revies.cs : user review class 
 
- Program.cs : main menu for rental car 
- RentalManager.cs : all method for rental 


# Requirement 
- Show all the cars in the system with an indication if they are available for rent or not.
- The customers should be able to select any car to see the full details including all customer's comments.
- Allow customers to rent a car. 
- Allow customers to return a car. 
- Allow customers to add comments about their experience when returning a car.
