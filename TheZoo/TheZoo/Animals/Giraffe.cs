using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.FoodService;

namespace TheZoo.Animals
{
    public class Giraffe : Animal
    {
        private const double foodNeedInKg = 40;
        private static readonly  Type foodPreference = typeof(Grass);


        public Giraffe(): base(foodNeedInKg, foodPreference)
        {

        }

    }
}
