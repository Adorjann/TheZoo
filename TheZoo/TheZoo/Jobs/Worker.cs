using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.Animals;
using TheZoo.FoodService;

namespace TheZoo.Jobs
{
    public class Worker : Job
    {
        private FoodStorage _foodStorage;

        public FoodStorage FoodStorage { get => _foodStorage; set => _foodStorage = value; }

        public Worker() : base(typeof(Worker))
        {

        }


        public override bool DoTheWork(object theObject,Employee employee)
        {
            Animal animal = (Animal)theObject;
            double foodToGiveInPercentage = employee.PercentageOfWork;
            double animalsDailyFoodNeed = animal.FoodNeedInKg;

            double amountOfFoodToFeed = (foodToGiveInPercentage / 100) * animalsDailyFoodNeed;

            Food foodToFeedAnimal = this.FoodStorage.RemoveFood(animal.FoodPreference, amountOfFoodToFeed);

            if(foodToFeedAnimal == null)
            {
                return false;
            }
            
                if (animal.Eat(foodToFeedAnimal))
                {
                    Console.WriteLine($"|{animal.Id}|{animal.GetType().Name} was fed {amountOfFoodToFeed}Kg of {animal.FoodPreference.Name}" +
                        $"\n\tby|{employee.Role.ToString().ToLower()}|{employee.Id}|\n");
                    return true;
                }
                Console.WriteLine($"Something went wrong, {animal.GetType().Name} didn't get his food");
                return false;
            
            
        }
    }
}
