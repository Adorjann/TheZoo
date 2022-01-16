using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.FoodService;

namespace TheZoo.Animals
{
    public class Elephant : Animal
    {

        private const double foodNeedInKg = 120;
        private static readonly Type foodPreference = typeof(Grass);


        public Elephant() : base(foodNeedInKg, foodPreference)
        {

        }
    }
}
