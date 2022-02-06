using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheZoo.FoodService
{
    public class FoodStorage
    {
        private List<Food> _allFood = new List<Food>();

        public FoodStorage()
        {
            Food meat = new Meat();
            Food grass = new Grass();
            this._allFood.Add(meat);
            this._allFood.Add(grass);
        }


        public bool AddFood (Food newFood)
        {
            Food food =  _allFood.Find(f => f.GetType() == newFood.GetType());
            if (food != null)
            {
                return food.addQuantity(newFood.Quantity);
            }

            return false;
        }

        public Food RemoveFood(Type foodType, double amount)
        {
            Food food = _allFood.Find(f => f.GetType() == foodType);
            if (food != null)
            {
                Food removedFood = (Food)Activator.CreateInstance(foodType);

                double foodAmount = food.RemoveAmount(amount);
                if (removedFood.addQuantity(foodAmount))
                {
                    return removedFood;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"You can't remove more {foodType.Name} than we have. Try again with the proper amount");
                }
            }

            return null;
        }
    }
}
