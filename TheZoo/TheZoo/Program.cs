using System;
using System.Collections.Generic;
using System.Threading;
using TheZoo.Animals;
using TheZoo.FoodService;

namespace TheZoo
{
    public class Program
    {

        public static void Main()
        {
            // In the class Animal, the timer is set for one day after the animal is fed enough for the day.
            // When the Timer event is activated, The Zoo is handling each hungy animal in separate Thread.

            int timerInMiliseconds = 5000;      // <--------- Change if Desired: Lenght of the day 
            int maxNumOfEmployees = 10;         // <--------- Change if Desired: Max number of Employees(min is hardcoded 3)

            List<Animal> animals = CreateSomeAnimals(timerInMiliseconds);
            List<Employee> employees = CreateSomeEmployees(maxNumOfEmployees); 

            int meatInKg = 30;                   // <--------- Change if Desired: MEAT KG
            Meat meat = new (meatInKg);
            int grassInKg = 170;                 // <--------- Change if Desired: GRASS KG
            Grass grass = new (grassInKg);

            FoodAmountChecker(meat, grass, animals);

            FoodStorage foodStorage = new ();
            foodStorage.AddFood(meat);
            foodStorage.AddFood(grass);

            TicketOffice ticketOffice = new TicketOffice();

            TheZoo zoo = new (animals,employees,foodStorage, ticketOffice);

            GenerateSomeVisitors(zoo);

            Console.ReadKey();
        }

        private static void GenerateSomeVisitors(TheZoo zoo)
        {
            int numOfVisitors = 5;
            for (int i = 0; i<= numOfVisitors-1; i++)
            {
                Visitor visitor = new (800.0,2010,i);
                zoo.NewVisitorArrived(visitor);
            }

            Thread.Sleep(1000);
            int poorVisitors = 5;
            for (int i = 0; i <= poorVisitors - 1; i++)
            {
                Visitor visitor = new (500.0, 20, i);
                zoo.NewVisitorArrived(visitor);
            }

            Thread.Sleep(1000);
            int under7YoVisitors = 5;
            for (int i = 0; i <= under7YoVisitors - 1; i++)
            {
                Visitor visitor = new (400.0, 6, i);
                zoo.NewVisitorArrived(visitor);
            }
        }

        private static void FoodAmountChecker(Meat meat, Grass grass, List<Animal> animals)
        {
            double meatNeedCounter = 0;
            double grassNeedCounter = 0;

            animals.ForEach(animal => {

                if (animal.FoodPreference.Equals(typeof(Meat)))
                {
                    meatNeedCounter += animal.FoodNeedInKg;
                }
                else
                {
                    grassNeedCounter += animal.FoodNeedInKg;
                }
            });

            double meatEatersdaysSurvive = meat.Quantity / meatNeedCounter;
            double grassEatersdaysSurvive = grass.Quantity / grassNeedCounter;

            Console.WriteLine("\tWelcome to the Zoo!\n");
            Console.WriteLine($"\t\t\tMeat-eating animals will have food for {meatEatersdaysSurvive} days, *" +
                $"\n\t\t\tGrass-eating animals will have food for {grassEatersdaysSurvive} days. *\n");

            if (meatEatersdaysSurvive < 1 || grassEatersdaysSurvive < 1)
            {
                Console.WriteLine($"\n\n\t...Since there is no enough food for the animals, the Zoo will be closed.." +
                        $"\n\t\t.. Good Bye ..");
                Environment.Exit(0);
            }
        }

        private static List<Animal> CreateSomeAnimals(int lenghtOfTheDay)
        {// Creates  somewhere between 1 and 6(maxNum not included) Animal of each subclass

            List<Animal> retVal = new ();

            List<Type> typesOfAnimals = new ()
            {
                typeof(Elephant),
                typeof(Giraffe),
                typeof(Leopard),
                typeof(Lion),
                typeof(Tiger),
                typeof(Zebra),
            };
            int numOfinstances = 1; // <--------- Change here to get more instances of each Animal subclass
            int idGenerator = 0;

            for (int i = 0; i < typesOfAnimals.Count; i++)
            {
                for (int j = numOfinstances; j > 0; j--)
                {
                    Animal animal = (Animal)Activator.CreateInstance(typesOfAnimals[i]);
                    animal.Id = idGenerator++;
                    animal.LenghtOfTheDay = lenghtOfTheDay;
                    retVal.Add(animal);
                }
            }

            Console.WriteLine(retVal.Count);
            if (retVal.Count == 0) 
            {
                Console.WriteLine("We have a problem with creating animals"); 
                return retVal;
            }
            else
            {
                return retVal;
            }
        }

        private static List<Employee> CreateSomeEmployees(int maxEmployees)
        {// Creates  somewhere between 3 and maxEmployees(maxNum not included)of Employees
            List<Employee> retVal = new ();

            int numOfEmployees = new Random().Next(3, maxEmployees);
            for (int i = 0; i <= numOfEmployees; i++)
            {
                Employee employee = (Employee)Activator.CreateInstance(typeof(Employee));
                employee.Id = i;
                retVal.Add(employee);
            }

            if (retVal.Count == 0)
            {
                Console.WriteLine("We have a problem with creating employees"); 
                return retVal;
            }
            else
            {
                return retVal;
            }
        }
    }
}
