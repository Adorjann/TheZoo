using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.FoodService;

namespace TheZoo.Animals
{
    public class Zebra : Animal
    {
        private const double foodNeedInKg = 10;
        private static readonly Type foodPreference = typeof(Grass);


        public Zebra() : base(foodNeedInKg, foodPreference)
        {

        }

    }
}
