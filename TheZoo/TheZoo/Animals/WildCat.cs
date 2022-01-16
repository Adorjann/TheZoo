using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.FoodService;

namespace TheZoo.Animals
{
    public abstract class WildCat : Animal
    {
        private static readonly Type foodPreference = typeof(Meat);

        public WildCat(double dailyFoodNeedInKg) : base(dailyFoodNeedInKg, foodPreference)
        {
        }
    }
}
